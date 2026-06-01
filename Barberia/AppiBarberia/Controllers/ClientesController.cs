using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/Clientes
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IClientesNegocio? iClientesNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public ClientesController()
        {
            this.iClientesNegocio = new ClientesNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public Clientes Guardar(Clientes entidad)
        {
            if (this.iClientesNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iClientesNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<Clientes> Consultar()
        {

            if (this.iClientesNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iClientesNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public Clientes ConsultarPorId(int id)
        {
            if (this.iClientesNegocio == null)
                throw new Exception("No implementado");

            return this.iClientesNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public Clientes Actualizar(Clientes entidad)
        {
            if (this.iClientesNegocio == null)
                throw new Exception("No implementado");

            return this.iClientesNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iClientesNegocio == null)
                throw new Exception("No implementado");

            return this.iClientesNegocio!.Eliminar(id);
        }
    }
}