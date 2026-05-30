using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/Comisiones
    [Route("[controller]")]
    public class ComisionesController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IComisionesNegocio? iComisionesNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public ComisionesController()
        {
            this.iComisionesNegocio = new ComisionesNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public Comisiones Guardar(Comisiones entidad)
        {
            if (this.iComisionesNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iComisionesNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<Comisiones> Consultar()
        {

            if (this.iComisionesNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iComisionesNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public Comisiones ConsultarPorId(int id)
        {
            if (this.iComisionesNegocio == null)
                throw new Exception("No implementado");

            return this.iComisionesNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public Comisiones Actualizar(Comisiones entidad)
        {
            if (this.iComisionesNegocio == null)
                throw new Exception("No implementado");

            return this.iComisionesNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iComisionesNegocio == null)
                throw new Exception("No implementado");

            return this.iComisionesNegocio!.Eliminar(id);
        }
    }
}