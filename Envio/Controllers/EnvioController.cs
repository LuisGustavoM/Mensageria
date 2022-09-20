using Domain;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace Envio.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    public class EnvioController : ControllerBase
    {
        private readonly Config config = new("filaDeMensagens");
        private readonly ILogger<EnvioController> _logger;
        public EnvioController(ILogger<EnvioController> logger)
        {
            _logger = logger;
        }

        [Route("EnviarMensagem")]
        [HttpPost]
        public IActionResult EnviarMensagem(Mensagem mensagem)
        {
            try
            {
                ConnectionFactory factory = new() {
                    HostName = config.HostName,
                    Port = config.Porta,
                    UserName = config.UserName,
                    Password = config.Password
                }; 

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                {
                    channel.QueueDeclare(queue: "",
                                             exclusive: false,
                                             autoDelete: false,
                                             durable: false,
                                             arguments: null);

                    var mensagemToByte = mensagem.ConverterMensagem(mensagem);

                    channel.BasicPublish(
                        exchange: "",
                        mandatory: false,
                        routingKey: config.NomeFila,
                        basicProperties: null,
                        body: mensagemToByte
                    );
                    return Accepted(mensagem);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao tentar criar um novo pedido", ex);
                return new StatusCodeResult(500);
            }
        }
    }
}
