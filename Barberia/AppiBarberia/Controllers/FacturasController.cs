using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/Facturas
    [Route("[controller]")]
    public class FacturasController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IFacturasNegocio? iFacturasNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public FacturasController()
        {
            this.iFacturasNegocio = new FacturasNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public Facturas Guardar(Facturas entidad)
        {
            if (this.iFacturasNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iFacturasNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<Facturas> Consultar()
        {

            if (this.iFacturasNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iFacturasNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public Facturas ConsultarPorId(int id)
        {
            if (this.iFacturasNegocio == null)
                throw new Exception("No implementado");

            return this.iFacturasNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public Facturas Actualizar(Facturas entidad)
        {
            if (this.iFacturasNegocio == null)
                throw new Exception("No implementado");

            return this.iFacturasNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iFacturasNegocio == null)
                throw new Exception("No implementado");

            return this.iFacturasNegocio!.Eliminar(id);
        }
    }
}