using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TareaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Crear tarea
        [HttpPost]
        public async Task<ActionResult<Tarea>> CrearTarea(Tarea tarea)
        {
            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObtenerTarea), new { id = tarea.Id }, tarea);
        }

        // Modificar tarea
        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarTarea(int id, Tarea tarea)
        {
            if (id != tarea.Id) return BadRequest();
            
            _context.Entry(tarea).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Obtener tarea por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> ObtenerTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null) return NotFound();
            return tarea;
        }

        // Listar todas las tareas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> ListarTareas()
        {
            return await _context.Tareas.ToListAsync();
        }

        // Listar tareas por estado
        [HttpGet("estado/{estado}")]
        public async Task<ActionResult<IEnumerable<Tarea>>> ListarTareasPorEstado(string estado)
        {
            return await _context.Tareas
                .Where(t => t.Estado == estado)
                .ToListAsync();
        }
    }
} 