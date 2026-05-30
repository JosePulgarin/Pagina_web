using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/Proveedores
    [Route("[controller]")]
    public class ProveedoresController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IProveedoresNegocio? iProveedoresNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public ProveedoresController()
        {
            this.iProveedoresNegocio = new ProveedoresNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public Proveedores Guardar(Proveedores entidad)
        {
            if (this.iProveedoresNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iProveedoresNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<Proveedores> Consultar()
        {

            if (this.iProveedoresNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iProveedoresNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public Proveedores ConsultarPorId(int id)
        {
            if (this.iProveedoresNegocio == null)
                throw new Exception("No implementado");

            return this.iProveedoresNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public Proveedores Actualizar(Proveedores entidad)
        {
            if (this.iProveedoresNegocio == null)
                throw new Exception("No implementado");

            return this.iProveedoresNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iProveedoresNegocio == null)
                throw new Exception("No implementado");

            return this.iProveedoresNegocio!.Eliminar(id);
        }
    }
}