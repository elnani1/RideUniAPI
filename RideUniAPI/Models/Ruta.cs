using System.ComponentModel.DataAnnotations;

namespace RideUniAPI.Models
{
    public class Ruta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El origen es obligatorio")]
        [StringLength(100)]
        public string Origen { get; set; }

        [Required(ErrorMessage = "El destino es obligatorio")]
        [StringLength(100)]
        public string Destino { get; set; }

        [Required(ErrorMessage = "La distancia es obligatoria")]
        [Range(0.1, 9999, ErrorMessage = "La distancia debe ser mayor a 0")]
        public double Distancia { get; set; }
    }
}
