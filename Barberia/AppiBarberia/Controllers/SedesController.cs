using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/Sedes
    [Route("[controller]")]
    public class SedesController : ControllerBase
    {
        // 1. El contrato con el Chef
        private ISedesNegocio? iSedesNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public SedesController()
        {
            this.iSedesNegocio = new SedesNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public Sedes Guardar(Sedes entidad)
        {
            if (this.iSedesNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iSedesNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<Sedes> Consultar()
        {

            if (this.iSedesNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iSedesNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public Sedes ConsultarPorId(int id)
        {
            if (this.iSedesNegocio == null)
                throw new Exception("No implementado");

            return this.iSedesNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public Sedes Actualizar(Sedes entidad)
        {
            if (this.iSedesNegocio == null)
                throw new Exception("No implementado");

            return this.iSedesNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iSedesNegocio == null)
                throw new Exception("No implementado");

            return this.iSedesNegocio!.Eliminar(id);
        }
    }
}