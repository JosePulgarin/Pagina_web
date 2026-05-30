using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.PerfilUsuarios
{
    public class IndexModel : PageModel
    {
        private readonly PerfilUsuariosService _PerfilUsuariosService;
        private readonly HistoricosService _historicosService; // INYECCIÓN DE HISTORICOS

        // Usamos nuestra nueva "maleta" de PerfilUsuarioModel
        public List<PerfilUsuariosClase> ListaPerfilUsuarios { get; set; } = new List<PerfilUsuariosClase>();        
        // Inyectamos el servicio moderno
        public IndexModel(PerfilUsuariosService PerfilUsuariosService, HistoricosService historicosService)
        {
            _PerfilUsuariosService = PerfilUsuariosService;
            _historicosService = historicosService; // INYECCIÓN DE HISTORICOS
        }

        public async Task OnGetAsync()
        {
            // Llamada asíncrona limpia a la API
            ListaPerfilUsuarios = await _PerfilUsuariosService.ConsultarAsync();


            var registro = new HistoricosClase
            {
                Usuario = "Admin", // Aquí podrías poner el usuario real si tienes autenticación
                Accion = "Consultó la lista de PerfilUsuarios",
                Fecha = DateTime.Now
            };

            await _historicosService.GuardarAsync(registro);
        }

        // Este método se activa cuando presionan el botón rojo de Borrar
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // Llama a tu servicio para que borre el PerfilUsuario con ese ID
            // NOTA: Asegúrate de tener este método EliminarAsync creado en tu PerfilUsuariosService
            var exito = await _PerfilUsuariosService.EliminarAsync(id);

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
