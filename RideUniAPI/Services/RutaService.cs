using Microsoft.Data.SqlClient;
using RideUniAPI.Models;
using RideUniAPI.Services.Interfaces;

namespace RideUniAPI.Services
{
    public class RutaService : IRutaService
    {
        private readonly string _connectionString;

        public RutaService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RideUniDB");
        }

        public async Task<List<Ruta>> ListarAsync()
        {
            var lista = new List<Ruta>();
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ListarRutas", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                lista.Add(MapearRuta(reader));

            return lista;
        }

        public async Task<Ruta?> BuscarAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_BuscarRuta", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return MapearRuta(reader);

            return null;
        }

        public async Task InsertarAsync(Ruta r)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_InsertarRuta", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", r.Id);
            cmd.Parameters.AddWithValue("@origen", r.Origen);
            cmd.Parameters.AddWithValue("@destino", r.Destino);
            cmd.Parameters.AddWithValue("@distancia", r.Distancia);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task ActualizarAsync(Ruta r)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ActualizarRuta", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", r.Id);
            cmd.Parameters.AddWithValue("@origen", r.Origen);
            cmd.Parameters.AddWithValue("@destino", r.Destino);
            cmd.Parameters.AddWithValue("@distancia", r.Distancia);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task EliminarAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_EliminarRuta", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        private static Ruta MapearRuta(SqlDataReader r) => new()
        {
            Id = Convert.ToInt32(r["id"]),
            Origen = r["origen"].ToString(),
            Destino = r["destino"].ToString(),
            Distancia = (double)r["distancia"]
        };
    }
}