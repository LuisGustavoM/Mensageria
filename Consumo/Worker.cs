using Domain;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Consumo
{
    public class Worker : BackgroundService
    {
        private readonly Config config = new("filaDeMensagens");
        private readonly IConnection _connection;
        private IModel _channel;

        public Worker()
        {
            var factory = new ConnectionFactory
            {
                HostName = config.HostName,
                Port = config.Porta,
                UserName = config.UserName,
                Password = config.Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                    queue: config.NomeFila,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);
                var message = JsonConvert.DeserializeObject<Mensagem>(contentString);

                Console.WriteLine(string.Format("Id da Mensagem: {0} , Conteudo: {1}", message.Id, message.Conteudo));
                _channel.BasicAck(eventArgs.DeliveryTag, false);

            };
            _channel.BasicConsume(config.NomeFila, false, consumer);

            return Task.CompletedTask;

        }
    }
}