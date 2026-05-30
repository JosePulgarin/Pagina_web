using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.PromocionesEspeciales
{
    public class IndexModel : PageModel
    {
        private readonly PromocionesEspecialesService _PromocionesEspecialesService;
        private readonly HistoricosService _historicosService; // INYECCIÓN DE HISTORICOS

        // Usamos nuestra nueva "maleta" de PromocionEspecialModel
        public List<PromocionesEspecialesClase> ListaPromocionesEspeciales { get; set; } = new List<PromocionesEspecialesClase>();        
        // Inyectamos el servicio moderno
        public IndexModel(PromocionesEspecialesService PromocionesEspecialesService, HistoricosService historicosService)
        {
            _PromocionesEspecialesService = PromocionesEspecialesService;
            _historicosService = historicosService; // INYECCIÓN DE HISTORICOS
        }

        public async Task OnGetAsync()
        {
            // Llamada asíncrona limpia a la API
            ListaPromocionesEspeciales = await _PromocionesEspecialesService.ConsultarAsync();


            var registro = new HistoricosClase
            {
                Usuario = "Admin", // Aquí podrías poner el usuario real si tienes autenticación
                Accion = "Consultó la lista de PromocionesEspeciales",
                Fecha = DateTime.Now
            };

            await _historicosService.GuardarAsync(registro);
        }

        // Este método se activa cuando presionan el botón rojo de Borrar
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // Llama a tu servicio para que borre el PromocionEspecial con ese ID
            // NOTA: Asegúrate de tener este método EliminarAsync creado en tu PromocionesEspecialesService
            var exito = await _PromocionesEspecialesService.EliminarAsync(id);

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
