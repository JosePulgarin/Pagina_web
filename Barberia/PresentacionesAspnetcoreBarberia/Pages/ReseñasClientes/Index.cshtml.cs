using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.ReseñasClientes
{
    public class IndexModel : PageModel
    {
        private readonly ReseñasClientesService _ReseñasClientesService;
        private readonly HistoricosService _historicosService; // INYECCIÓN DE HISTORICOS

        // Usamos nuestra nueva "maleta" de ReseñaClienteModel
        public List<ReseñasClientesClase> ListaReseñasClientes { get; set; } = new List<ReseñasClientesClase>();        
        // Inyectamos el servicio moderno
        public IndexModel(ReseñasClientesService ReseñasClientesService, HistoricosService historicosService)
        {
            _ReseñasClientesService = ReseñasClientesService;
            _historicosService = historicosService; // INYECCIÓN DE HISTORICOS
        }

        public async Task OnGetAsync()
        {
            // Llamada asíncrona limpia a la API
            ListaReseñasClientes = await _ReseñasClientesService.ConsultarAsync();


            var registro = new HistoricosClase
            {
                Usuario = "Admin", // Aquí podrías poner el usuario real si tienes autenticación
                Accion = "Consultó la lista de ReseñasClientes",
                Fecha = DateTime.Now
            };

            await _historicosService.GuardarAsync(registro);
        }

        // Este método se activa cuando presionan el botón rojo de Borrar
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // Llama a tu servicio para que borre el ReseñaCliente con ese ID
            // NOTA: Asegúrate de tener este método EliminarAsync creado en tu ReseñasClientesService
            var exito = await _ReseñasClientesService.EliminarAsync(id);

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
