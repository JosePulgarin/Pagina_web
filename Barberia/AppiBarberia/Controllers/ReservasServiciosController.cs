using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/ReservasServicios
    [Route("[controller]")]
    public class ReservasServiciosController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IReservasServiciosNegocio? iReservasServiciosNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public ReservasServiciosController()
        {
            this.iReservasServiciosNegocio = new ReservasServiciosNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public ReservasServicios Guardar(ReservasServicios entidad)
        {
            if (this.iReservasServiciosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iReservasServiciosNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<ReservasServicios> Consultar()
        {

            if (this.iReservasServiciosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iReservasServiciosNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public ReservasServicios ConsultarPorId(int id)
        {
            if (this.iReservasServiciosNegocio == null)
                throw new Exception("No implementado");

            return this.iReservasServiciosNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public ReservasServicios Actualizar(ReservasServicios entidad)
        {
            if (this.iReservasServiciosNegocio == null)
                throw new Exception("No implementado");

            return this.iReservasServiciosNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iReservasServiciosNegocio == null)
                throw new Exception("No implementado");

            return this.iReservasServiciosNegocio!.Eliminar(id);
        }
    }
}