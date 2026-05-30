using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/Productos
    [Route("[controller]")]
    public class ProductosController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IProductosNegocio? iProductosNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public ProductosController()
        {
            this.iProductosNegocio = new ProductosNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public Productos Guardar(Productos entidad)
        {
            if (this.iProductosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iProductosNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<Productos> Consultar()
        {

            if (this.iProductosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iProductosNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public Productos ConsultarPorId(int id)
        {
            if (this.iProductosNegocio == null)
                throw new Exception("No implementado");

            return this.iProductosNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public Productos Actualizar(Productos entidad)
        {
            if (this.iProductosNegocio == null)
                throw new Exception("No implementado");

            return this.iProductosNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iProductosNegocio == null)
                throw new Exception("No implementado");

            return this.iProductosNegocio!.Eliminar(id);
        }
    }
}