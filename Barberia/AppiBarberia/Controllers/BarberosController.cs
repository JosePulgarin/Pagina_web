using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/Barberos
    [Route("[controller]")]
    public class BarberosController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IBarberosNegocio? iBarberosNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public BarberosController()
        {
            this.iBarberosNegocio = new BarberosNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public Barberos Guardar(Barberos entidad)
        {
            if (this.iBarberosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iBarberosNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<Barberos> Consultar()
        {

            if (this.iBarberosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iBarberosNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public Barberos ConsultarPorId(int id)
        {
            if (this.iBarberosNegocio == null)
                throw new Exception("No implementado");

            return this.iBarberosNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public Barberos Actualizar(Barberos entidad)
        {
            if (this.iBarberosNegocio == null)
                throw new Exception("No implementado");

            return this.iBarberosNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iBarberosNegocio == null)
                throw new Exception("No implementado");

            return this.iBarberosNegocio!.Eliminar(id);
        }
    }
}