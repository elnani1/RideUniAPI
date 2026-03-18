using Microsoft.AspNetCore.Mvc;
using RideUniAPI.Models;
using RideUniAPI.Services.Interfaces;

namespace RideUniAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RutaController : Controller
    {
        private readonly IRutaService _service;

        public RutaController(IRutaService service)
        {
            _service = service;
        }

        // GET: api/ruta
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var lista = await _service.ListarAsync();
            return Ok(lista);
        }

        // GET: api/ruta/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var ruta = await _service.BuscarAsync(id);
            if (ruta == null)
                return NotFound(new { mensaje = "Ruta no encontrada" });

            return Ok(ruta);
        }

        // POST: api/ruta
        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] Ruta ruta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.InsertarAsync(ruta);
            return Ok(new { mensaje = "Ruta registrada correctamente" });
        }

        // PUT: api/ruta/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Ruta ruta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ruta.Id = id;
            await _service.ActualizarAsync(ruta);
            return Ok(new { mensaje = "Ruta actualizada correctamente" });
        }

        // DELETE: api/ruta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existe = await _service.BuscarAsync(id);
            if (existe == null)
                return NotFound(new { mensaje = "Ruta no encontrada" });

            await _service.EliminarAsync(id);
            return Ok(new { mensaje = "Ruta eliminada correctamente" });
        }
    }
}