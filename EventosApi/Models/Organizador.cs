namespace EventosApi.Models
{
    public class Organizador
    {
        public int IdOrganizador { get; set; }
        public required string Nombre { get; set; }
        public required string Cargo { get; set; }
        public int IdEvento { get; set; }
    }
}
