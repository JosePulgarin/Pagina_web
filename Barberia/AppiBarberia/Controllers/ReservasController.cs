using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/Reservas
    [Route("[controller]")]
    public class ReservasController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IReservasNegocio? iReservasNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public ReservasController()
        {
            this.iReservasNegocio = new ReservasNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public Reservas Guardar(Reservas entidad)
        {
            if (this.iReservasNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iReservasNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<Reservas> Consultar()
        {

            if (this.iReservasNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iReservasNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public Reservas ConsultarPorId(int id)
        {
            if (this.iReservasNegocio == null)
                throw new Exception("No implementado");

            return this.iReservasNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public Reservas Actualizar(Reservas entidad)
        {
            if (this.iReservasNegocio == null)
                throw new Exception("No implementado");

            return this.iReservasNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iReservasNegocio == null)
                throw new Exception("No implementado");

            return this.iReservasNegocio!.Eliminar(id);
        }
    }
}