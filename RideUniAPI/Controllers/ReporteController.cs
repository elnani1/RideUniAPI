using Microsoft.AspNetCore.Mvc;
using RideUniAPI.Models;
using RideUniAPI.Services.Interfaces;

namespace RideUniAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReporteController : Controller
    {
        private readonly IReporteService _service;

        public ReporteController(IReporteService service)
        {
            _service = service;
        }

        // GET: api/reporte
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var lista = await _service.ListarAsync();
            return Ok(lista);
        }

        // GET: api/reporte/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var reporte = await _service.BuscarAsync(id);
            if (reporte == null)
                return NotFound(new { mensaje = "Reporte no encontrado" });

            return Ok(reporte);
        }

        // POST: api/reporte
        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] Reporte reporte)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.InsertarAsync(reporte);
            return Ok(new { mensaje = "Reporte registrado correctamente" });
        }

        // PUT: api/reporte/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Reporte reporte)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            reporte.Id = id;
            await _service.ActualizarAsync(reporte);
            return Ok(new { mensaje = "Reporte actualizado correctamente" });
        }

        // DELETE: api/reporte/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existe = await _service.BuscarAsync(id);
            if (existe == null)
                return NotFound(new { mensaje = "Reporte no encontrado" });

            await _service.EliminarAsync(id);
            return Ok(new { mensaje = "Reporte eliminado correctamente" });
        }
    }
}