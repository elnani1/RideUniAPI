using System.ComponentModel.DataAnnotations;

namespace RideUniAPI.Models
{
    public class Reporte
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe asignar un camión")]
        public int IdCamion { get; set; }
    }
}
