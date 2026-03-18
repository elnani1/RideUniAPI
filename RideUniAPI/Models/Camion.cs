using System.ComponentModel.DataAnnotations;

namespace RideUniAPI.Models
{
    public class Camion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El modelo es obligatorio")]
        [StringLength(100)]
        public string Modelo { get; set; }

        [Required]
        [Range(1, 200, ErrorMessage = "La capacidad debe estar entre 1 y 200")]
        public int Capacidad { get; set; }

        public bool Disponibilidad { get; set; }

        [Required(ErrorMessage = "Debe asignar un conductor")]
        public int IdConductor { get; set; }

        public Conductor? Conductor { get; set; }
    }
}
