using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using IntegrationService.Models; 
using IntegrationService.Data;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationService.Service
{
    public class ConsumeRabbitMQHostedService: BackgroundService
    {
        private readonly ILogger _logger;  
        private IConnection _connection;  
        private IModel _channel;

        //private readonly ISContext _context;
        private readonly IServiceScopeFactory _scopeFactory;

        public ConsumeRabbitMQHostedService(ILoggerFactory loggerFactory, IServiceScopeFactory scopeFactory)  
        {
            this._scopeFactory = scopeFactory;  
            this._logger = loggerFactory.CreateLogger<ConsumeRabbitMQHostedService>();  
            InitRabbitMQ();  
        } 

        private void InitRabbitMQ()  
        {  
            var factory = new ConnectionFactory   {
                    HostName = "192.168.0.12",
                    UserName = "guest",
                    Password = "guest",
                    Port = 5672,
                    VirtualHost = "/"
                };  

            // create connection  
            _connection = factory.CreateConnection();  

            // create channel  
            _channel = _connection.CreateModel();  

            var ExchangeName = "1c.IS"; 
            var QueueName = "1c.IS.Response";
            var RoutingKey = "1c.IS.Response.*";

            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, true);  
            _channel.QueueDeclare(QueueName, false, false, false, null);  
            _channel.QueueBind(QueueName, ExchangeName, RoutingKey, null);  


            _channel.BasicQos(0, 1, false);  

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;  
        }  

        protected override Task ExecuteAsync(CancellationToken stoppingToken)  
        {  
            stoppingToken.ThrowIfCancellationRequested();  
    
            var consumer = new EventingBasicConsumer(_channel);  
            consumer.Received += (ch, ea) =>  
            {  
                // received message  
                var content = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());
                //var content = ea.ToString();  
    
                // handle the received message  
                HandleMessage(content);  
                _channel.BasicAck(ea.DeliveryTag, false);  
            };  
    
            consumer.Shutdown += OnConsumerShutdown;  
            consumer.Registered += OnConsumerRegistered;  
            consumer.Unregistered += OnConsumerUnregistered;  
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;  
    
            _channel.BasicConsume("1c.IS.Response", false, consumer);  
            return Task.CompletedTask;  
        }  


        private void HandleMessage(string content)  
        {  
            
            // {"Date": "2021-08-01T00:00:00-07:00", "StatusId": 2, "UpackageId": 20, "Message": "Пакет успешно обработан"}
            InputStatus inputStatus = JsonSerializer.Deserialize<InputStatus>(content);
            
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ISContext>();
                
                Status status = dbContext.Statuses
                        .Where(b => b.Id == inputStatus.StatusId)
                        .FirstOrDefault();

                Upackage upackage = dbContext.Upackages
                                        .Where(b => b.Id == inputStatus.UpackageId)
                                        .FirstOrDefault();
                
                var upackageStatus      = new UpackageStatus();
                upackageStatus.Date     = inputStatus.Date;
                upackageStatus.Status   = status;
                upackageStatus.Upackage = upackage;
                upackageStatus.Message  = inputStatus.Message;        

                dbContext.UpackageStatuses.Add(upackageStatus);
                dbContext.SaveChanges();
            }

            _logger.LogInformation($"consumer received {inputStatus.Message}");
        }  
        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)  {  }  
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) {  }  
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) {  }  
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) {  }  
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)  {  }  
    
        public override void Dispose()  
        {  
            _channel.Close();  
            _connection.Close();  
            base.Dispose();  
        }  

    }
}