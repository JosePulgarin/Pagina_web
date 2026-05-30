using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/Agendas
    [Route("[controller]")]
    public class AgendasController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IAgendasNegocio? iAgendasNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public AgendasController()
        {
            this.iAgendasNegocio = new AgendasNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public Agendas Guardar(Agendas entidad)
        {
            if (this.iAgendasNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iAgendasNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<Agendas> Consultar()
        {

            if (this.iAgendasNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iAgendasNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public Agendas ConsultarPorId(int id)
        {
            if (this.iAgendasNegocio == null)
                throw new Exception("No implementado");

            return this.iAgendasNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public Agendas Actualizar(Agendas entidad)
        {
            if (this.iAgendasNegocio == null)
                throw new Exception("No implementado");

            return this.iAgendasNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iAgendasNegocio == null)
                throw new Exception("No implementado");

            return this.iAgendasNegocio!.Eliminar(id);
        }
    }
}