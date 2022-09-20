namespace Domain
{
    public class Config
    {
        public string HostName { get; set; }
        public int Porta { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NomeFila { get; set; }

        public Config(string fila)
        {
            HostName = "127.0.0.1";
            Porta = 5672;
            UserName = "guest";
            Password = "guest";
            NomeFila = fila;
        }
    }
}
