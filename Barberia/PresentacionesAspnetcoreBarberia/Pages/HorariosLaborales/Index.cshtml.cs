using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.HorariosLaborales
{
    public class IndexModel : PageModel
    {
        private readonly HorariosLaboralesService _HorariosLaboralesService;
        private readonly HistoricosService _historicosService; // INYECCIÓN DE HISTORICOS

        // Usamos nuestra nueva "maleta" de HorarioLaboralModel
        public List<HorariosLaboralesClase> ListaHorariosLaborales { get; set; } = new List<HorariosLaboralesClase>();        
        // Inyectamos el servicio moderno
        public IndexModel(HorariosLaboralesService HorariosLaboralesService, HistoricosService historicosService)
        {
            _HorariosLaboralesService = HorariosLaboralesService;
            _historicosService = historicosService; // INYECCIÓN DE HISTORICOS
        }

        public async Task OnGetAsync()
        {
            // Llamada asíncrona limpia a la API
            ListaHorariosLaborales = await _HorariosLaboralesService.ConsultarAsync();


            var registro = new HistoricosClase
            {
                Usuario = "Admin", // Aquí podrías poner el usuario real si tienes autenticación
                Accion = "Consultó la lista de HorariosLaborales",
                Fecha = DateTime.Now
            };

            await _historicosService.GuardarAsync(registro);
        }

        // Este método se activa cuando presionan el botón rojo de Borrar
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // Llama a tu servicio para que borre el HorarioLaboral con ese ID
            // NOTA: Asegúrate de tener este método EliminarAsync creado en tu HorariosLaboralesService
            var exito = await _HorariosLaboralesService.EliminarAsync(id);

            if (exito)
            {
                // Si lo borró, recarga la página para que desaparezca de la tabla
                return RedirectToPage();
            }

            // Si falló, también recarga la página (luego podemos ponerle un mensaje de error)
            return RedirectToPage();
        }
    }
}
