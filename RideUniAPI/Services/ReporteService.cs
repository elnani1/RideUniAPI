using System.Collections;
using System.Reflection;
using Microsoft.Data.SqlClient;
using RideUniAPI.Models;
using RideUniAPI.Services;
using RideUniAPI.Services.Interfaces;

namespace RideUniAPI.Services
{
    public class ReporteService : IReporteService
    {
        private readonly string _connectionString;

        public ReporteService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RideUniDB");
        }

        public async Task<List<Reporte>> ListarAsync()
        {
            var lista = new List<Reporte>();
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ListarReportes", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                lista.Add(MapearReporte(reader));

            return lista;
        }

        public async Task<Reporte?> BuscarAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_BuscarReporte", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return MapearReporte(reader);

            return null;
        }

        // 🔥 FIX AQUÍ
        public async Task InsertarAsync(Reporte rep)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_InsertarReporte", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // ❌ NO mandar ID
            // cmd.Parameters.AddWithValue("@id", rep.Id);

            cmd.Parameters.AddWithValue("@fecha", rep.Fecha);
            cmd.Parameters.AddWithValue("@descripcion", rep.Descripcion);
            cmd.Parameters.AddWithValue("@id_camion", rep.IdCamion);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task ActualizarAsync(Reporte rep)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ActualizarReporte", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", rep.Id);
            cmd.Parameters.AddWithValue("@fecha", rep.Fecha);
            cmd.Parameters.AddWithValue("@descripcion", rep.Descripcion);
            cmd.Parameters.AddWithValue("@id_camion", rep.IdCamion);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task EliminarAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_EliminarReporte", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        private static Reporte MapearReporte(SqlDataReader r) => new()
        {
            Id = Convert.ToInt32(r["id"]),
            Fecha = (DateTime)r["fecha"],
            Descripcion = r["descripcion"].ToString(),
            IdCamion = (int)r["id_camion"]
        };
    }
}