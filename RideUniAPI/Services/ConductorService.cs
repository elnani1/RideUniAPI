using Microsoft.Data.SqlClient;
using RideUniAPI.Models;
using RideUniAPI.Services.Interfaces;

namespace RideUniAPI.Services
{
    public class ConductorService : IConductorService
    {
        private readonly string _connectionString;

        public ConductorService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RideUniDB")!;
        }

        public async Task<List<Conductor>> ListarAsync()
        {
            var lista = new List<Conductor>();
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ListarConductores", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                lista.Add(MapearConductor(reader));

            return lista;
        }

        public async Task<Conductor?> BuscarAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_BuscarConductor", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return MapearConductor(reader);

            return null;
        }

        public async Task InsertarAsync(Conductor c)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_InsertarConductor", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("id", c.Id);
            cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
            cmd.Parameters.AddWithValue("@numeroCelular", c.NumeroCelular);
            cmd.Parameters.AddWithValue("@Direccion", c.Direccion);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task ActualizarAsync(Conductor c)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ActualizarConductor", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", c.Id);
            cmd.Parameters.AddWithValue("@nombre", c.Nombre);
            cmd.Parameters.AddWithValue("@numeroCelular", c.NumeroCelular);
            cmd.Parameters.AddWithValue("@direccion", c.Direccion);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task EliminarAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_EliminarConductor", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        private static Conductor MapearConductor(SqlDataReader r) => new()
        {
            Id = Convert.ToInt32(r["id"]),
            Nombre = r["nombre"].ToString(),
            NumeroCelular = r["numeroCelular"].ToString(),
            Direccion = r["direccion"].ToString()
        };
    }
}