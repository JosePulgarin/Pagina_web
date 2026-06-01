using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/ReseñasClientes
    [Route("[controller]")]
    public class ReseñasClientesController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IReseñasClientesNegocio? iReseñasClientesNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public ReseñasClientesController()
        {
            this.iReseñasClientesNegocio = new ReseñasClientesNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public ReseñasClientes Guardar(ReseñasClientes entidad)
        {
            if (this.iReseñasClientesNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iReseñasClientesNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<ReseñasClientes> Consultar()
        {

            if (this.iReseñasClientesNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iReseñasClientesNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public ReseñasClientes ConsultarPorId(int id)
        {
            if (this.iReseñasClientesNegocio == null)
                throw new Exception("No implementado");

            return this.iReseñasClientesNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public ReseñasClientes Actualizar(ReseñasClientes entidad)
        {
            if (this.iReseñasClientesNegocio == null)
                throw new Exception("No implementado");

            return this.iReseñasClientesNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iReseñasClientesNegocio == null)
                throw new Exception("No implementado");

            return this.iReseñasClientesNegocio!.Eliminar(id);
        }
    }
}