using System.ComponentModel.DataAnnotations;

namespace EventosApi.DTO
{
    public class ParticipanteDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del participante es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "El email no tiene un formato valido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El evento asociado es requerido")]
        public int IdEvento { get; set; }
    }
}
