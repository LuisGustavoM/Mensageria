using System.Text;
using System.Text.Json;

namespace Domain
{
    public class Mensagem
    {
        public int Id { get; set; }
        public string Conteudo { get; set; }

        public byte[] ConverterMensagem(Mensagem mensagem)
        {
            // Serializando a mensagem para Json 
            var message = JsonSerializer.Serialize(mensagem);
            // Transformando Json em byte 
            var body = Encoding.UTF8.GetBytes(message);
            return body;
        }
    }
}