using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.CategoriasProductos
{
    public class IndexModel : PageModel
    {
        private readonly CategoriasProductosService _CategoriasProductosService;
        private readonly HistoricosService _historicosService; // INYECCIÓN DE HISTORICOS

        // Usamos nuestra nueva "maleta" de CategoriaProductoModel
        public List<CategoriasProductosClase> ListaCategoriasProductos { get; set; } = new List<CategoriasProductosClase>();        
        // Inyectamos el servicio moderno
        public IndexModel(CategoriasProductosService CategoriasProductosService, HistoricosService historicosService)
        {
            _CategoriasProductosService = CategoriasProductosService;
            _historicosService = historicosService; // INYECCIÓN DE HISTORICOS
        }

        public async Task OnGetAsync()
        {
            // Llamada asíncrona limpia a la API
            ListaCategoriasProductos = await _CategoriasProductosService.ConsultarAsync();


            var registro = new HistoricosClase
            {
                Usuario = "Admin", // Aquí podrías poner el usuario real si tienes autenticación
                Accion = "Consultó la lista de CategoriasProductos",
                Fecha = DateTime.Now
            };

            await _historicosService.GuardarAsync(registro);
        }

        // Este método se activa cuando presionan el botón rojo de Borrar
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // Llama a tu servicio para que borre el CategoriaProducto con ese ID
            // NOTA: Asegúrate de tener este método EliminarAsync creado en tu CategoriasProductosService
            var exito = await _CategoriasProductosService.EliminarAsync(id);

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
