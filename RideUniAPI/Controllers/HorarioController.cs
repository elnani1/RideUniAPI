using Microsoft.AspNetCore.Mvc;
using RideUniAPI.Models;
using RideUniAPI.Services.Interfaces;

namespace RideUniAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorarioController : Controller
    {
        private readonly IHorarioService _service;

        public HorarioController(IHorarioService service)
        {
            _service = service;
        }

        // GET: api/horario
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var lista = await _service.ListarAsync();
            return Ok(lista);
        }

        // GET: api/horario/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var horario = await _service.BuscarAsync(id);
            if (horario == null)
                return NotFound(new { mensaje = "Horario no encontrado" });

            return Ok(horario);
        }

        // POST: api/horario
        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] Horario horario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.InsertarAsync(horario);
            return Ok(new { mensaje = "Horario registrado correctamente" });
        }

        // PUT: api/horario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Horario horario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            horario.Id = id;
            await _service.ActualizarAsync(horario);
            return Ok(new { mensaje = "Horario actualizado correctamente" });
        }

        // DELETE: api/horario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existe = await _service.BuscarAsync(id);
            if (existe == null)
                return NotFound(new { mensaje = "Horario no encontrado" });

            await _service.EliminarAsync(id);
            return Ok(new { mensaje = "Horario eliminado correctamente" });
        }
    }
}