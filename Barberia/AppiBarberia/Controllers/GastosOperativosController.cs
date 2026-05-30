using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/GastosOperativos
    [Route("[controller]")]
    public class GastosOperativosController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IGastosOperativosNegocio? iGastosOperativosNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public GastosOperativosController()
        {
            this.iGastosOperativosNegocio = new GastosOperativosNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public GastosOperativos Guardar(GastosOperativos entidad)
        {
            if (this.iGastosOperativosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iGastosOperativosNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<GastosOperativos> Consultar()
        {

            if (this.iGastosOperativosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iGastosOperativosNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public GastosOperativos ConsultarPorId(int id)
        {
            if (this.iGastosOperativosNegocio == null)
                throw new Exception("No implementado");

            return this.iGastosOperativosNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public GastosOperativos Actualizar(GastosOperativos entidad)
        {
            if (this.iGastosOperativosNegocio == null)
                throw new Exception("No implementado");

            return this.iGastosOperativosNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iGastosOperativosNegocio == null)
                throw new Exception("No implementado");

            return this.iGastosOperativosNegocio!.Eliminar(id);
        }
    }
}