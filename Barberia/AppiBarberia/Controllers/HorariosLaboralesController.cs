using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/HorariosLaborales
    [Route("[controller]")]
    public class HorariosLaboralesController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IHorariosLaboralesNegocio? iHorariosLaboralesNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public HorariosLaboralesController()
        {
            this.iHorariosLaboralesNegocio = new HorariosLaboralesNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public HorariosLaborales Guardar(HorariosLaborales entidad)
        {
            if (this.iHorariosLaboralesNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iHorariosLaboralesNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<HorariosLaborales> Consultar()
        {

            if (this.iHorariosLaboralesNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iHorariosLaboralesNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public HorariosLaborales ConsultarPorId(int id)
        {
            if (this.iHorariosLaboralesNegocio == null)
                throw new Exception("No implementado");

            return this.iHorariosLaboralesNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public HorariosLaborales Actualizar(HorariosLaborales entidad)
        {
            if (this.iHorariosLaboralesNegocio == null)
                throw new Exception("No implementado");

            return this.iHorariosLaboralesNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iHorariosLaboralesNegocio == null)
                throw new Exception("No implementado");

            return this.iHorariosLaboralesNegocio!.Eliminar(id);
        }
    }
}