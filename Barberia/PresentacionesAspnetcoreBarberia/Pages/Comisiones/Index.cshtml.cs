using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Comisiones
{
    public class IndexModel : PageModel
    {
        private readonly ComisionesService _ComisionesService;
        private readonly HistoricosService _historicosService; // INYECCIÓN DE HISTORICOS

        // Usamos nuestra nueva "maleta" de ComisionModel
        public List<ComisionesClase> ListaComisiones { get; set; } = new List<ComisionesClase>();        
        // Inyectamos el servicio moderno
        public IndexModel(ComisionesService ComisionesService, HistoricosService historicosService)
        {
            _ComisionesService = ComisionesService;
            _historicosService = historicosService; // INYECCIÓN DE HISTORICOS
        }

        public async Task OnGetAsync()
        {
            // Llamada asíncrona limpia a la API
            ListaComisiones = await _ComisionesService.ConsultarAsync();


            var registro = new HistoricosClase
            {
                Usuario = "Admin", // Aquí podrías poner el usuario real si tienes autenticación
                Accion = "Consultó la lista de Comisiones",
                Fecha = DateTime.Now
            };

            await _historicosService.GuardarAsync(registro);
        }

        // Este método se activa cuando presionan el botón rojo de Borrar
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // Llama a tu servicio para que borre el Comision con ese ID
            // NOTA: Asegúrate de tener este método EliminarAsync creado en tu ComisionesService
            var exito = await _ComisionesService.EliminarAsync(id);

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
