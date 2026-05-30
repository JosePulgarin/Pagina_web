using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/Portafolios
    [Route("[controller]")]
    public class PortafoliosController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IPortafoliosNegocio? iPortafoliosNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public PortafoliosController()
        {
            this.iPortafoliosNegocio = new PortafoliosNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public Portafolios Guardar(Portafolios entidad)
        {
            if (this.iPortafoliosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iPortafoliosNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<Portafolios> Consultar()
        {

            if (this.iPortafoliosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iPortafoliosNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public Portafolios ConsultarPorId(int id)
        {
            if (this.iPortafoliosNegocio == null)
                throw new Exception("No implementado");

            return this.iPortafoliosNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public Portafolios Actualizar(Portafolios entidad)
        {
            if (this.iPortafoliosNegocio == null)
                throw new Exception("No implementado");

            return this.iPortafoliosNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iPortafoliosNegocio == null)
                throw new Exception("No implementado");

            return this.iPortafoliosNegocio!.Eliminar(id);
        }
    }
}
