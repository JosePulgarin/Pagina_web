using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/MetodosPago
    [Route("[controller]")]
    public class MetodosPagoController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IMetodosPagoNegocio? iMetodosPagoNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public MetodosPagoController()
        {
            this.iMetodosPagoNegocio = new MetodosPagoNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public MetodosPago Guardar(MetodosPago entidad)
        {
            if (this.iMetodosPagoNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iMetodosPagoNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<MetodosPago> Consultar()
        {

            if (this.iMetodosPagoNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iMetodosPagoNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public MetodosPago ConsultarPorId(int id)
        {
            if (this.iMetodosPagoNegocio == null)
                throw new Exception("No implementado");

            return this.iMetodosPagoNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public MetodosPago Actualizar(MetodosPago entidad)
        {
            if (this.iMetodosPagoNegocio == null)
                throw new Exception("No implementado");

            return this.iMetodosPagoNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iMetodosPagoNegocio == null)
                throw new Exception("No implementado");

            return this.iMetodosPagoNegocio!.Eliminar(id);
        }
    }
}