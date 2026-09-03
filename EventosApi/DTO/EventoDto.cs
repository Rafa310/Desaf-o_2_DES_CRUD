using System.ComponentModel.DataAnnotations;

namespace EventosApi.DTO
{
    public class EventoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del evento es requerido")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "El nombre debe tener entre 5 y 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha del evento es requerida")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El lugar del evento es requerido")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "El lugar debe tener entre 5 y 100 caracteres")]
        public string Lugar { get; set; } = string.Empty;
    }
}
