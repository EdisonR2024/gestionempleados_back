using GestionEmpleadosAPI.Data;
using GestionEmpleadosAPI.DTOs;
using GestionEmpleadosAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpleadosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public EmpleadosController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult ObtenerEmpleados()
        {
            var empleados = _dbContext.Empleados.ToList();
            return Ok(empleados);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerEmpleado(int id)
        {
            var empleado = _dbContext.Empleados.Find(id);

            //También se puede usar FirstOrDefault pero Find es más eficiente para buscar
            //por clave primaria.
            //var empleado = _dbContext.Empleados.FirstOrDefault(e => e.Id == id);

            if (empleado == null)
            {
                return NotFound("Empleado no existe");
            }

            return Ok(empleado);
        }

        [HttpPost]
        public IActionResult CrearEmpleado(EmpleadoDTO empleadoDTO)
        {
            var empleadoNuevo = new Empleado()
            {
                Nombre = empleadoDTO.Nombre,
                Apellido = empleadoDTO.Apellido,
                Email = empleadoDTO.Email
            };

            _dbContext.Add(empleadoNuevo);//Esto es obligatorio para que el nuevo empleado se agregue al contexto de la base de datos, si se omite no se guardará en la base de datos.
            _dbContext.SaveChanges();

            //return Ok("Se creo el nuevo empleado");//Se debe retornar CreatedAtAction en lugar de Ok para seguir las buenas prácticas de REST.
            return CreatedAtAction(nameof(ObtenerEmpleado), new { id = empleadoNuevo.Id }, empleadoNuevo);

        }

        [HttpPut("{id}")]
        public IActionResult ActualizarEmpleado(int id, EmpleadoDTO empleadoDTO)
        {

            var empleado = _dbContext.Empleados.Find(id);

            if (empleado is null)
            {
                return NotFound("Empleado no existe");
            }

            empleado.Nombre = empleadoDTO.Nombre;
            empleado.Apellido = empleadoDTO.Apellido;
            empleado.Email = empleadoDTO.Email;

            //_dbContext.Update(empleado);

            _dbContext.SaveChanges();

            //return Ok(empleado);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult EliminarEmpleado(int id)
        {
            var empleado = _dbContext.Empleados.Find(id);

            if (empleado == null)
            {
                return NotFound("Empleado no existe");
            }

            _dbContext.Empleados.Remove(empleado);
            _dbContext.SaveChanges();

            //return Ok("Empleado eliminado correctamente");
            return NoContent();
        }
    }
}
