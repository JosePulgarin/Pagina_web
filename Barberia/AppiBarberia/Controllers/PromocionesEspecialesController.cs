using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/PromocionesEspeciales
    [Route("[controller]")]
    public class PromocionesEspecialesController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IPromocionesEspecialesNegocio? iPromocionesEspecialesNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public PromocionesEspecialesController()
        {
            this.iPromocionesEspecialesNegocio = new PromocionesEspecialesNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public PromocionesEspeciales Guardar(PromocionesEspeciales entidad)
        {
            if (this.iPromocionesEspecialesNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iPromocionesEspecialesNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<PromocionesEspeciales> Consultar()
        {

            if (this.iPromocionesEspecialesNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iPromocionesEspecialesNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public PromocionesEspeciales ConsultarPorId(int id)
        {
            if (this.iPromocionesEspecialesNegocio == null)
                throw new Exception("No implementado");

            return this.iPromocionesEspecialesNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public PromocionesEspeciales Actualizar(PromocionesEspeciales entidad)
        {
            if (this.iPromocionesEspecialesNegocio == null)
                throw new Exception("No implementado");

            return this.iPromocionesEspecialesNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iPromocionesEspecialesNegocio == null)
                throw new Exception("No implementado");

            return this.iPromocionesEspecialesNegocio!.Eliminar(id);
        }
    }
}