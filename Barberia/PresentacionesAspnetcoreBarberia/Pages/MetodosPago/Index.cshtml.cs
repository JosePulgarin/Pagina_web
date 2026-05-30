using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.MetodosPago
{
    public class IndexModel : PageModel
    {
        private readonly MetodosPagoService _MetodosPagoService;
        private readonly HistoricosService _historicosService; // INYECCIÓN DE HISTORICOS

        // Usamos nuestra nueva "maleta" de MetodoPagoModel
        public List<MetodosPagoClase> ListaMetodosPago { get; set; } = new List<MetodosPagoClase>();        
        // Inyectamos el servicio moderno
        public IndexModel(MetodosPagoService MetodosPagoService, HistoricosService historicosService)
        {
            _MetodosPagoService = MetodosPagoService;
            _historicosService = historicosService; // INYECCIÓN DE HISTORICOS
        }

        public async Task OnGetAsync()
        {
            // Llamada asíncrona limpia a la API
            ListaMetodosPago = await _MetodosPagoService.ConsultarAsync();


            var registro = new HistoricosClase
            {
                Usuario = "Admin", // Aquí podrías poner el usuario real si tienes autenticación
                Accion = "Consultó la lista de MetodosPago",
                Fecha = DateTime.Now
            };

            await _historicosService.GuardarAsync(registro);
        }

        // Este método se activa cuando presionan el botón rojo de Borrar
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // Llama a tu servicio para que borre el MetodoPago con ese ID
            // NOTA: Asegúrate de tener este método EliminarAsync creado en tu MetodosPagoService
            var exito = await _MetodosPagoService.EliminarAsync(id);

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
