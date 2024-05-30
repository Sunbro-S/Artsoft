using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace ProfileConnectionLib.ConnectionServices.RabbitMQConnectionServer;

public class ConnectionPool : IPooledObjectPolicy<IConnection>
{
    private ConnectionFactory _connectionFactory;

    public ConnectionPool(string host)
    {
        var factory = new ConnectionFactory 
        {        
            HostName = host,
            UserName = "Admin",
            Password = "12345"
        };
        _connectionFactory = factory;
    }
    
    public IConnection Create()
    {
        return _connectionFactory.CreateConnection();
    }

    public bool Return(IConnection obj)
    {
        if (obj.IsOpen)
        {
            return true;
        }
        
        obj.Dispose();
        return false;
    }
}