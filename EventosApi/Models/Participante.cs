namespace EventosApi.Models
{
    public class Participante
    {
        public int IdParticipante { get; set; }
        public required string Nombre { get; set; }
        public required string Email { get; set; }
        public int IdEvento { get; set; }
    }
}
