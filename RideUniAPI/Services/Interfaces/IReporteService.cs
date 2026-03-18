using RideUniAPI.Models;

namespace RideUniAPI.Services.Interfaces
{
    public interface IReporteService
    {
            Task<List<Reporte>> ListarAsync();
            Task<Reporte?> BuscarAsync(int id);
            Task InsertarAsync(Reporte reporte);
            Task ActualizarAsync(Reporte reporte);
            Task EliminarAsync(int id);
    }
}
