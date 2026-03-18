using Microsoft.AspNetCore.Mvc;
using RideUniAPI.Models;
using RideUniAPI.Services.Interfaces;

namespace RideUniAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConductorController : Controller
    {
        private readonly IConductorService _service;

        public ConductorController(IConductorService service)
        {
            _service = service;
        }

        // GET: api/conductor
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var lista = await _service.ListarAsync();
            return Ok(lista);
        }

        // GET: api/conductor/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var conductor = await _service.BuscarAsync(id);
            if (conductor == null)
                return NotFound(new { mensaje = "Conductor no encontrado" });

            return Ok(conductor);
        }

        // POST: api/conductor
        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] Conductor conductor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.InsertarAsync(conductor);
            return Ok(new { mensaje = "Conductor registrado correctamente" });
        }

        // PUT: api/conductor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Conductor conductor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            conductor.Id = id;
            await _service.ActualizarAsync(conductor);
            return Ok(new { mensaje = "Conductor actualizado correctamente" });
        }

        // DELETE: api/conductor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existe = await _service.BuscarAsync(id);
            if (existe == null)
                return NotFound(new { mensaje = "Conductor no encontrado" });

            await _service.EliminarAsync(id);
            return Ok(new { mensaje = "Conductor eliminado correctamente" });
        }
    }
}