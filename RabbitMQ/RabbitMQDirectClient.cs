using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;

namespace IntegrationService.RabbitMQ
{

    public class RabbitMQDirectClient
    {

        public RabbitMQDirectClient()
        {

        }

        public void Send(string queueName, string msg)
        {
            try
            {
                var connectionFactory = new ConnectionFactory()
                {
                    HostName = "192.168.0.9",
                    UserName = "guest",
                    Password = "guest",
                    Port = 5672,
                    VirtualHost = "/"
                };

                using (var rabbitConnection = connectionFactory.CreateConnection())
                {
                    using (var channel = rabbitConnection.CreateModel())
                    {
                        channel.QueueDeclare(
                            queue: queueName,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);


                        channel.BasicPublish(
                            exchange: string.Empty,
                            routingKey: queueName,
                            basicProperties: null,
                            body: Encoding.UTF8.GetBytes(msg));

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void Receive()
        {
            try
            {
                var connectionFactory = new ConnectionFactory()
                {
                    HostName = "192.168.0.9",
                    UserName = "guest",
                    Password = "guest",
                    Port = 5672,
                    VirtualHost = "/"
                };

                using (var rabbitConnection = connectionFactory.CreateConnection())
                {
                    using (var channel = rabbitConnection.CreateModel())
                    {
                        channel.ExchangeDeclare("1c.IS.Response", "topic");
                        channel.QueueDeclare(
                            queue: "1c.IS.Response",
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                        // channel.QueueBind(CardPaymentQueueName, ExchangeName, 
                        // "payment.cardpayment");

                        // channel.BasicQos(0, 10, false);

                        Subscription subscription = new Subscription(channel, 
                        "1c.IS.Response", false);


                        // channel.BasicPublish(
                        //     exchange: string.Empty,
                        //     routingKey: queueName,
                        //     basicProperties: null,
                        //     body: Encoding.UTF8.GetBytes(msg));

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}