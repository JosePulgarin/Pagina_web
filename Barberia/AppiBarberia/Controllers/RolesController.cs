using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/Roles
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IRolesNegocio? iRolesNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public RolesController()
        {
            this.iRolesNegocio = new RolesNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public Roles Guardar(Roles entidad)
        {
            if (this.iRolesNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iRolesNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<Roles> Consultar()
        {

            if (this.iRolesNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iRolesNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public Roles ConsultarPorId(int id)
        {
            if (this.iRolesNegocio == null)
                throw new Exception("No implementado");

            return this.iRolesNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public Roles Actualizar(Roles entidad)
        {
            if (this.iRolesNegocio == null)
                throw new Exception("No implementado");

            return this.iRolesNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iRolesNegocio == null)
                throw new Exception("No implementado");

            return this.iRolesNegocio!.Eliminar(id);
        }
    }
}