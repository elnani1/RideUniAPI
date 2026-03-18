using RideUniAPI.Models;

namespace RideUniAPI.Services.Interfaces
{
    public interface ICamionService
    {
        Task<List<Camion>> ListarAsync();
        Task<Camion?> BuscarAsync(int id);
        Task InsertarAsync(Camion camion);
        Task ActualizarAsync(Camion camion);
        Task EliminarAsync(int id);
    }
}
