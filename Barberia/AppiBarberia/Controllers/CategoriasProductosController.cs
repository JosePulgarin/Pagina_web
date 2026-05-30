using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/CategoriasProductos
    [Route("[controller]")]
    public class CategoriasProductosController : ControllerBase
    {
        // 1. El contrato con el Chef
        private ICategoriasProductosNegocio? iCategoriasProductosNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public CategoriasProductosController()
        {
            this.iCategoriasProductosNegocio = new CategoriasProductosNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public CategoriasProductos Guardar(CategoriasProductos entidad)
        {
            if (this.iCategoriasProductosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iCategoriasProductosNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<CategoriasProductos> Consultar()
        {

            if (this.iCategoriasProductosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iCategoriasProductosNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public CategoriasProductos ConsultarPorId(int id)
        {
            if (this.iCategoriasProductosNegocio == null)
                throw new Exception("No implementado");

            return this.iCategoriasProductosNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public CategoriasProductos Actualizar(CategoriasProductos entidad)
        {
            if (this.iCategoriasProductosNegocio == null)
                throw new Exception("No implementado");

            return this.iCategoriasProductosNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iCategoriasProductosNegocio == null)
                throw new Exception("No implementado");

            return this.iCategoriasProductosNegocio!.Eliminar(id);
        }
    }
}