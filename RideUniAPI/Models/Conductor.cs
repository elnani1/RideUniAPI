using System.ComponentModel.DataAnnotations;

namespace RideUniAPI.Models
{
    public class Conductor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El número celular es obligatorio")]
        [StringLength(15)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Solo se permiten números")]
        public string NumeroCelular { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(150)]
        public string Direccion { get; set; }
    }
}
