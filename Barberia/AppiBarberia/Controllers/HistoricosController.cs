using LibreriaBarberia.Nucleo;
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Appi_Bailes_JP.Controllers // Asegúrate de que coincida con el nombre de tu proyecto API
{
    [ApiController]
    // Usamos el ruteo estándar de REST, la URL será: http://localhost:puerto/Academias
    [Route("[controller]")]
    public class HistoricosController : ControllerBase
    {
        // 1. El contrato con el Chef
        private IHistoricosNegocio? iHistoricosNegocio;

        // 2. El Constructor: Aquí contratamos al Chef
        public HistoricosController()
        {
            this.iHistoricosNegocio = new HistoricosNegocio();
        }

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        [HttpPost]
        public bool Guardar(Historicos entidad)
        {
            if (this.iHistoricosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero le pasa el pedido al Chef
            return this.iHistoricosNegocio!.Guardar(entidad);
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        [HttpGet]
        public List<Historicos> Consultar()
        {

            if (this.iHistoricosNegocio == null)
                throw new Exception("No implementado");

            // El Mesero trae la lista que hizo el Chef
            return this.iHistoricosNegocio!.Consultar();
        }



    }
}