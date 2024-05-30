using System.Collections.Concurrent;
using System.Net;
using System.Text;
using CoreLib.HttpServiceV2.Services.Interfaces;
using ExampleCore.HttpLogic.Services;
using ExampleCore.TraceLogic.Interfaces;
using Medo;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProfileConnectionLib.ConnectionServices.RabbitMQConnectionServer.Service;

public class RequestServer : IHttpRequestService
{
    private readonly string _replyQueueName;
    
    private readonly IEnumerable<ITraceWriter> _traceWriterList;
    private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> _callbackMapper = new();
    private readonly ObjectPool<IConnection> _connectionPool;
    public RequestServer( IEnumerable<ITraceWriter> traceWriterList, ObjectPool<IConnection> connectionPool)
    {
        _connectionPool = connectionPool;
        _traceWriterList = traceWriterList;
        
        var connection = _connectionPool.Get();
        var channel = connection.CreateModel();
        
        _replyQueueName = channel.QueueDeclare().QueueName;
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            if (!_callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var tcs))
                return;
            var body = ea.Body.ToArray();
            var response = Encoding.UTF8.GetString(body);
            tcs.TrySetResult(response);
        };
        
        channel.BasicConsume(consumer: consumer,
            queue: _replyQueueName,  
            autoAck: true);
        
        _connectionPool.Return(connection);
    }
    
    public async Task<HttpResponse<TResponse>> SendRequestAsync<TResponse>(HttpRequestData requestData, HttpConnectionData connectionData = default)
    {
        var connection = _connectionPool.Get();
        using var channel = connection.CreateModel();
        var props = channel.CreateBasicProperties();
        var correlationId = new Uuid7().ToString();     
        props.CorrelationId = correlationId; 
        props.ReplyTo = _replyQueueName;
        foreach (var traceWriter in _traceWriterList)
        {
            if (traceWriter != null)
            {
                var name = traceWriter.Name;
                var value = traceWriter.GetValue();
                if (!string.IsNullOrEmpty(name) && value != null)
                {
                    props.Headers.Add(name, value);
                }
            }
        }

        try
        {
            var res = await SendMessegeAsync<TResponse>(requestData, connectionData, correlationId, channel, props);
            return new HttpResponse<TResponse>()
            {
                Body = res,
                StatusCode = HttpStatusCode.OK
            };
        }
           
        finally
        {
            _connectionPool.Return(connection);
        }
    }
    
    private async Task<TResponse> SendMessegeAsync<TResponse>(HttpRequestData requestData, HttpConnectionData connectionData,
        string correlationId, IModel channel, IBasicProperties props)
    {
        var json = JsonConvert.SerializeObject(requestData.Body);
        var messageBytes = Encoding.UTF8.GetBytes(json);
        
        var tcs = new TaskCompletionSource<string>();
        _callbackMapper.TryAdd(correlationId, tcs);
       
        channel.BasicPublish(exchange: String.Empty,
            routingKey: "rpc/"+requestData.Uri,
            basicProperties: props,
            body: messageBytes);

        connectionData.CancellationToken.Register(() => _callbackMapper.TryRemove(correlationId, out _));

        var answer = await tcs.Task;
        var response = JsonConvert.DeserializeObject<TResponse>(answer);
        return response;
    }
}