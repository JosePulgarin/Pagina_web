using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/Membresias
    [Route("[controller]")]
    public class MembresiasController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IMembresiasNegocio? iMembresiasNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public MembresiasController()
        {
            this.iMembresiasNegocio = new MembresiasNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public Membresias Guardar(Membresias entidad)
        {
            if (this.iMembresiasNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iMembresiasNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<Membresias> Consultar()
        {

            if (this.iMembresiasNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iMembresiasNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public Membresias ConsultarPorId(int id)
        {
            if (this.iMembresiasNegocio == null)
                throw new Exception("No implementado");

            return this.iMembresiasNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public Membresias Actualizar(Membresias entidad)
        {
            if (this.iMembresiasNegocio == null)
                throw new Exception("No implementado");

            return this.iMembresiasNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iMembresiasNegocio == null)
                throw new Exception("No implementado");

            return this.iMembresiasNegocio!.Eliminar(id);
        }
    }
}