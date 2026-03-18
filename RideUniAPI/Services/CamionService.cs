using Microsoft.Data.SqlClient;
using RideUniAPI.Models;
using RideUniAPI.Services.Interfaces;

namespace RideUniAPI.Services
{
    public class CamionService : ICamionService
    {
        private readonly string _connectionString;

        public CamionService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RideUniDB");
        }

        public async Task<List<Camion>> ListarAsync()
        {
            var lista = new List<Camion>();
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_listar_camiones", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                lista.Add(MapearCamion(reader));

            return lista;
        }

        public async Task<Camion?> BuscarAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_buscar_camion", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return MapearCamion(reader);

            return null;
        }

        public async Task InsertarAsync(Camion c)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_insertar_camion", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@modelo", c.Modelo);
            cmd.Parameters.AddWithValue("@capacidad", c.Capacidad);
            cmd.Parameters.AddWithValue("@disponibilidad", c.Disponibilidad);
            cmd.Parameters.AddWithValue("@id_conductor", c.IdConductor);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task ActualizarAsync(Camion c)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_actualizar_camion", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", c.Id);
            cmd.Parameters.AddWithValue("@modelo", c.Modelo);
            cmd.Parameters.AddWithValue("@capacidad", c.Capacidad);
            cmd.Parameters.AddWithValue("@disponibilidad", c.Disponibilidad);
            cmd.Parameters.AddWithValue("@id_conductor", c.IdConductor);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task EliminarAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_eliminar_camion", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        private static Camion MapearCamion(SqlDataReader r) => new()
        {
            Id = Convert.ToInt32(r["id"]),
            Modelo = r["modelo"].ToString(),
            Capacidad = (int)r["capacidad"],
            Disponibilidad = (bool)r["disponibilidad"],
            IdConductor = (int)r["id_conductor"]
        };
    }
}