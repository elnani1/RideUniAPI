using Microsoft.AspNetCore.Mvc;
using RideUniAPI.Models;
using RideUniAPI.Services.Interfaces;

namespace RideUniAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CamionController : Controller
    {
        private readonly ICamionService _service;

        public CamionController(ICamionService service)
        {
            _service = service;
        }

        // GET: api/camion
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var lista = await _service.ListarAsync();
            return Ok(lista);
        }

        // GET: api/camion/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var camion = await _service.BuscarAsync(id);
            if (camion == null)
                return NotFound(new { mensaje = "Camión no encontrado" });

            return Ok(camion);
        }

        // POST: api/camion
        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] Camion camion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.InsertarAsync(camion);
            return Ok(new { mensaje = "Camión registrado correctamente" });
        }

        // PUT: api/camion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Camion camion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            camion.Id = id;
            await _service.ActualizarAsync(camion);
            return Ok(new { mensaje = "Camión actualizado correctamente" });
        }

        // DELETE: api/camion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existe = await _service.BuscarAsync(id);
            if (existe == null)
                return NotFound(new { mensaje = "Camión no encontrado" });

            await _service.EliminarAsync(id);
            return Ok(new { mensaje = "Camión eliminado correctamente" });
        }
    }
}