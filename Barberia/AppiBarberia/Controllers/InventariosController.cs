using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/Inventarios
    [Route("[controller]")]
    public class InventariosController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IInventariosNegocio? iInventariosNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public InventariosController()
        {
            this.iInventariosNegocio = new InventariosNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public Inventarios Guardar(Inventarios entidad)
        {
            if (this.iInventariosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iInventariosNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<Inventarios> Consultar()
        {

            if (this.iInventariosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iInventariosNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public Inventarios ConsultarPorId(int id)
        {
            if (this.iInventariosNegocio == null)
                throw new Exception("No implementado");

            return this.iInventariosNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public Inventarios Actualizar(Inventarios entidad)
        {
            if (this.iInventariosNegocio == null)
                throw new Exception("No implementado");

            return this.iInventariosNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iInventariosNegocio == null)
                throw new Exception("No implementado");

            return this.iInventariosNegocio!.Eliminar(id);
        }
    }
}