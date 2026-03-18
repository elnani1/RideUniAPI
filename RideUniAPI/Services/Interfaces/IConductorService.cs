using RideUniAPI.Models;

namespace RideUniAPI.Services.Interfaces
{
    public interface IConductorService
    {
        Task<List<Conductor>> ListarAsync();
        Task<Conductor?> BuscarAsync(int id);
        Task InsertarAsync(Conductor conductor);
        Task ActualizarAsync(Conductor conductor);
        Task EliminarAsync(int id);
    }
}
