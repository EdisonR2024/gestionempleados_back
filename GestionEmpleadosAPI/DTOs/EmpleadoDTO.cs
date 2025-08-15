using System.ComponentModel.DataAnnotations;

namespace GestionEmpleadosAPI.DTOs
{
    public class EmpleadoDTO
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Apellido { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }
    }
}
