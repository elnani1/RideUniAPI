using RideUniAPI.Models;

namespace RideUniAPI.Services.Interfaces
{
    public interface IRutaService
    {
            Task<List<Ruta>> ListarAsync();
            Task<Ruta?> BuscarAsync(int id);
            Task InsertarAsync(Ruta ruta);
            Task ActualizarAsync(Ruta ruta);
            Task EliminarAsync(int id);
    }
}
