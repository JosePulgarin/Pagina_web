using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/Recepcionistas
    [Route("[controller]")]
    public class RecepcionistasController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IRecepcionistasNegocio? iRecepcionistasNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public RecepcionistasController()
        {
            this.iRecepcionistasNegocio = new RecepcionistasNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public Recepcionistas Guardar(Recepcionistas entidad)
        {
            if (this.iRecepcionistasNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iRecepcionistasNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<Recepcionistas> Consultar()
        {

            if (this.iRecepcionistasNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iRecepcionistasNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public Recepcionistas ConsultarPorId(int id)
        {
            if (this.iRecepcionistasNegocio == null)
                throw new Exception("No implementado");

            return this.iRecepcionistasNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public Recepcionistas Actualizar(Recepcionistas entidad)
        {
            if (this.iRecepcionistasNegocio == null)
                throw new Exception("No implementado");

            return this.iRecepcionistasNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iRecepcionistasNegocio == null)
                throw new Exception("No implementado");

            return this.iRecepcionistasNegocio!.Eliminar(id);
        }
    }
}