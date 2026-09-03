using System.ComponentModel.DataAnnotations;

namespace EventosApi.DTO
{
    public class OrganizadorDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del organizador es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El cargo del organizador es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El cargo debe tener entre 3 y 50 caracteres")]
        public string Cargo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El evento asociado es requerido")]
        public int IdEvento { get; set; }
    }
}
