using System.ComponentModel.DataAnnotations;

namespace GestionEmpleadosAPI.Models
{
    public class Empleado
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;
        
        [Required]
        public string Apellido { get; set; } = string.Empty;

        public string? Email { get; set; }   
    }
}
