using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionEmpleadosAPI.Data;
using GestionEmpleadosAPI.Models;

namespace GestionEmpleadosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosAUTController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmpleadosAUTController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/EmpleadosAUT
        //Consultar Todos los Empleados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
        {
            return await _context.Empleados.ToListAsync();
        }

        // GET: api/EmpleadosAUT/5
        //Consultar un empleado
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);

            if (empleado == null)
            {
                return NotFound();
            }

            return empleado;
        }

        // PUT: api/EmpleadosAUT/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //Actualizar un empleado
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleado(int id, Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return BadRequest();
            }

            _context.Entry(empleado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EmpleadosAUT
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //Crear un empleado
        [HttpPost]
        public async Task<ActionResult<Empleado>> PostEmpleado(Empleado empleado)
        {
            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpleado", new { id = empleado.Id }, empleado);
        }

        // DELETE: api/EmpleadosAUT/5
        //Eliminar un empleado
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }

            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();

            return NoContent();//El estandar de REST indica que al eliminar un recurso se debe retornar NoContent (204)
        }
        //Validar si existe un empleado
        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }
    }
}
