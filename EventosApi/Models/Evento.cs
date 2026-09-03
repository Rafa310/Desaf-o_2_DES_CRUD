namespace EventosApi.Models
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public required string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public required string Lugar { get; set; }
    }
}
