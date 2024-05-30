using System.Text;
using ExampleCore.RPC.Interface;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ExampleCore.RPC;

public abstract class Consumer<TRequest, TResponse> : IConsumer<TRequest>
    where TRequest : class
    where TResponse : class
{
    protected IServiceScopeFactory ScopeFactory { get; }

    protected Consumer(IServiceScopeFactory scopeFactory)
    {
        ScopeFactory = scopeFactory;
    }

    public async Task Consume(object sender, BasicDeliverEventArgs @event, TRequest message, IModel channel)
    {
        var response = string.Empty;
        var props = @event.BasicProperties;
        var replyProps = channel.CreateBasicProperties();
        replyProps.CorrelationId = props.CorrelationId;

        try
        {
            var responseMessage = await ProcessMessage(message);
            response = SerializeResponse(responseMessage);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            response = SerializeErrorResponse();
        }
        finally
        {
            SendResponse(channel, props, replyProps, response, @event);
        }
    }

    protected abstract Task<TResponse> ProcessMessage(TRequest message);
    protected abstract string SerializeResponse(TResponse response);
    protected abstract string SerializeErrorResponse();

    private void SendResponse(IModel channel, IBasicProperties props, IBasicProperties replyProps, string response,
        BasicDeliverEventArgs @event)
    {
        var responseBytes = Encoding.UTF8.GetBytes(response);
        channel.BasicPublish(exchange: string.Empty,
            routingKey: props.ReplyTo,
            basicProperties: replyProps,
            body: responseBytes);
        channel.BasicAck(deliveryTag: @event.DeliveryTag, multiple: false);
    }
}