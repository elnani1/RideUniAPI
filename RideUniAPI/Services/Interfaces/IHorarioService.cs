using RideUniAPI.Models;

namespace RideUniAPI.Services.Interfaces
{
    public interface IHorarioService
    {
            Task<List<Horario>> ListarAsync();
            Task<Horario?> BuscarAsync(int id);
            Task InsertarAsync(Horario horario);
            Task ActualizarAsync(Horario horario);
            Task EliminarAsync(int id);
    }
}
