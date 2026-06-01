using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppiBarberia.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/PromocionesServicios
    [Route("[controller]")]
    public class PromocionesServiciosController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IPromocionesServiciosNegocio? iPromocionesServiciosNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public PromocionesServiciosController()
        {
            this.iPromocionesServiciosNegocio = new PromocionesServiciosNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public PromocionesServicios Guardar(PromocionesServicios entidad)
        {
            if (this.iPromocionesServiciosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iPromocionesServiciosNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<PromocionesServicios> Consultar()
        {

            if (this.iPromocionesServiciosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iPromocionesServiciosNegocio!.Consultar();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        [HttpGet("{id}")] // Le decimos a Swagger que espere un número en la URL
        public PromocionesServicios ConsultarPorId(int id)
        {
            if (this.iPromocionesServiciosNegocio == null)
                throw new Exception("No implementado");

            return this.iPromocionesServiciosNegocio.ConsultarPorId(id);
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        [HttpPut]
        public PromocionesServicios Actualizar(PromocionesServicios entidad)
        {
            if (this.iPromocionesServiciosNegocio == null)
                throw new Exception("No implementado");

            return this.iPromocionesServiciosNegocio!.Actualizar(entidad);
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        [HttpDelete("{id}")] // Le decimos a Swagger qué número borrar
        public bool Eliminar(int id)
        {
            if (this.iPromocionesServiciosNegocio == null)
                throw new Exception("No implementado");

            return this.iPromocionesServiciosNegocio!.Eliminar(id);
        }
    }
}