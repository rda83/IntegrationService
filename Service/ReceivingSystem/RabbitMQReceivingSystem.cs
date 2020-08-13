using System;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace IntegrationService.Service.ReceivingSystem
{
    public class RabbitMQReceivingSystem: IReceivingSystem
    {

        private readonly IConfiguration _configuration;
        private ConnectionFactory connectionFactory;


        public RabbitMQReceivingSystem(IConfiguration configuration)
        {
            _configuration = configuration;

            connectionFactory = new ConnectionFactory()
            {
                HostName    = _configuration.GetValue<string>("RabbitMQReceivingSystem:HostName", ""),
                UserName    = _configuration.GetValue<string>("RabbitMQReceivingSystem:UserName", ""),
                Password    = _configuration.GetValue<string>("RabbitMQReceivingSystem:Password", ""),
                Port        = _configuration.GetValue<int>("RabbitMQReceivingSystem:Port", 0),
                VirtualHost = _configuration.GetValue<string>("RabbitMQReceivingSystem:VirtualHost", "")
            };
        }

        public void Send(string key, string message)
        {
            try
            {
                using (var rabbitConnection = connectionFactory.CreateConnection())
                {
                    using (var channel = rabbitConnection.CreateModel())
                    {
                        channel.QueueDeclare(
                            queue: key,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                        channel.BasicPublish(
                            exchange: string.Empty,
                            routingKey: key,
                            basicProperties: null,
                            body: Encoding.UTF8.GetBytes(message));
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}