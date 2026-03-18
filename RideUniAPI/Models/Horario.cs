using System.ComponentModel.DataAnnotations;

namespace RideUniAPI.Models
{
    public class Horario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La hora de salida es obligatoria")]
        public TimeSpan HoraSalida { get; set; }

        [Required(ErrorMessage = "La hora de llegada es obligatoria")]
        public TimeSpan HoraLlegada { get; set; }

        [Required(ErrorMessage = "Debe asignar un camión")]
        public int IdCamion { get; set; }
    }
}
