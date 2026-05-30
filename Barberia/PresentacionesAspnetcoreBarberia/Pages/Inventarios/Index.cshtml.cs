using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Inventarios
{
    public class IndexModel : PageModel
    {
        private readonly InventariosService _InventariosService;
        private readonly HistoricosService _historicosService; // INYECCIÓN DE HISTORICOS

        // Usamos nuestra nueva "maleta" de InventarioModel
        public List<InventariosClase> ListaInventarios { get; set; } = new List<InventariosClase>();        
        // Inyectamos el servicio moderno
        public IndexModel(InventariosService InventariosService, HistoricosService historicosService)
        {
            _InventariosService = InventariosService;
            _historicosService = historicosService; // INYECCIÓN DE HISTORICOS
        }

        public async Task OnGetAsync()
        {
            // Llamada asíncrona limpia a la API
            ListaInventarios = await _InventariosService.ConsultarAsync();


            var registro = new HistoricosClase
            {
                Usuario = "Admin", // Aquí podrías poner el usuario real si tienes autenticación
                Accion = "Consultó la lista de Inventarios",
                Fecha = DateTime.Now
            };

            await _historicosService.GuardarAsync(registro);
        }

        // Este método se activa cuando presionan el botón rojo de Borrar
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // Llama a tu servicio para que borre el Inventario con ese ID
            // NOTA: Asegúrate de tener este método EliminarAsync creado en tu InventariosService
            var exito = await _InventariosService.EliminarAsync(id);

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
