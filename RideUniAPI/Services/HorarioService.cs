using Microsoft.Data.SqlClient;
using RideUniAPI.Models;
using RideUniAPI.Services.Interfaces;

namespace RideUniAPI.Services
{
    public class HorarioService : IHorarioService
    {
        private readonly string _connectionString;

        public HorarioService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RideUniDB");
        }

        public async Task<List<Horario>> ListarAsync()
        {
            var lista = new List<Horario>();
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ListarHorarios", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                lista.Add(MapearHorario(reader));

            return lista;
        }

        public async Task<Horario?> BuscarAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_BuscarHorario", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return MapearHorario(reader);

            return null;
        }

        public async Task InsertarAsync(Horario h)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_InsertarHorario", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", h.Id);
            cmd.Parameters.AddWithValue("@horaSalida", h.HoraSalida);
            cmd.Parameters.AddWithValue("@horaLlegada", h.HoraLlegada);
            cmd.Parameters.AddWithValue("@id_camion", h.IdCamion);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task ActualizarAsync(Horario h)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ActualizarHorario", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", h.Id);
            cmd.Parameters.AddWithValue("@horaSalida", h.HoraSalida);
            cmd.Parameters.AddWithValue("@horaLlegada", h.HoraLlegada);
            cmd.Parameters.AddWithValue("@id_camion", h.IdCamion);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task EliminarAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_EliminarHorario", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        private static Horario MapearHorario(SqlDataReader r) => new()
        {
            Id = Convert.ToInt32(r["id"]),
            HoraSalida = (TimeSpan)r["horaSalida"],
            HoraLlegada = (TimeSpan)r["horaLlegada"],
            IdCamion = (int)r["id_camion"]
        };
    }
}