using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/PerfilUsuarios
    [Route("[controller]")]
    public class PerfilUsuariosController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IPerfilUsuariosNegocio? iPerfilUsuariosNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public PerfilUsuariosController()
        {
            this.iPerfilUsuariosNegocio = new PerfilUsuariosNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public PerfilUsuarios Guardar(PerfilUsuarios entidad)
        {
            if (this.iPerfilUsuariosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iPerfilUsuariosNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<PerfilUsuarios> Consultar()
        {

            if (this.iPerfilUsuariosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iPerfilUsuariosNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public PerfilUsuarios ConsultarPorId(int id)
        {
            if (this.iPerfilUsuariosNegocio == null)
                throw new Exception("No implementado");

            return this.iPerfilUsuariosNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public PerfilUsuarios Actualizar(PerfilUsuarios entidad)
        {
            if (this.iPerfilUsuariosNegocio == null)
                throw new Exception("No implementado");

            return this.iPerfilUsuariosNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iPerfilUsuariosNegocio == null)
                throw new Exception("No implementado");

            return this.iPerfilUsuariosNegocio!.Eliminar(id);
        }
    }
}