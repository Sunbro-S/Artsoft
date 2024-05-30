using System.Text;
using ExampleCore.RPC.Interface;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ExampleCore.RPC;

public class Publisher<T> : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;
    private readonly IConsumer<T> _consumer;
    
    public Publisher(string queueName,  IConsumer<T> consumer, string hostName = "localhost")
    {
        _queueName = queueName;
        _consumer = consumer;

        var factory = new ConnectionFactory { HostName = hostName, 
            DispatchConsumersAsync = true, UserName = "main", Password = "alex2004"};
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        
        _channel.QueueDeclare(queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var messageObject = JsonConvert.DeserializeObject<T>(message);
            await _consumer.Consume(model, ea, messageObject ?? throw new InvalidOperationException(), _channel);
        };

        _channel.BasicConsume(queue: _queueName,
            autoAck: false,
            consumer: consumer); 

        return Task.CompletedTask;
    }
}