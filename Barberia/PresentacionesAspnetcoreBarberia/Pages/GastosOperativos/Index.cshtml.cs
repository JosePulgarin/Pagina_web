using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.GastosOperativos
{
    public class IndexModel : PageModel
    {
        private readonly GastosOperativosService _GastosOperativosService;
        private readonly HistoricosService _historicosService; // INYECCIÓN DE HISTORICOS

        // Usamos nuestra nueva "maleta" de GastoOperativoModel
        public List<GastosOperativosClase> ListaGastosOperativos { get; set; } = new List<GastosOperativosClase>();        
        // Inyectamos el servicio moderno
        public IndexModel(GastosOperativosService GastosOperativosService, HistoricosService historicosService)
        {
            _GastosOperativosService = GastosOperativosService;
            _historicosService = historicosService; // INYECCIÓN DE HISTORICOS
        }

        public async Task OnGetAsync()
        {
            // Llamada asíncrona limpia a la API
            ListaGastosOperativos = await _GastosOperativosService.ConsultarAsync();


            var registro = new HistoricosClase
            {
                Usuario = "Admin", // Aquí podrías poner el usuario real si tienes autenticación
                Accion = "Consultó la lista de GastosOperativos",
                Fecha = DateTime.Now
            };

            await _historicosService.GuardarAsync(registro);
        }

        // Este método se activa cuando presionan el botón rojo de Borrar
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // Llama a tu servicio para que borre el GastoOperativo con ese ID
            // NOTA: Asegúrate de tener este método EliminarAsync creado en tu GastosOperativosService
            var exito = await _GastosOperativosService.EliminarAsync(id);

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
