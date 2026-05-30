using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly ProductosService _ProductosService;
        private readonly HistoricosService _historicosService; // INYECCIÓN DE HISTORICOS

        // Usamos nuestra nueva "maleta" de ProductoModel
        public List<ProductosClase> ListaProductos { get; set; } = new List<ProductosClase>();        
        // Inyectamos el servicio moderno
        public IndexModel(ProductosService ProductosService, HistoricosService historicosService)
        {
            _ProductosService = ProductosService;
            _historicosService = historicosService; // INYECCIÓN DE HISTORICOS
        }

        public async Task OnGetAsync()
        {
            // Llamada asíncrona limpia a la API
            ListaProductos = await _ProductosService.ConsultarAsync();


            var registro = new HistoricosClase
            {
                Usuario = "Admin", // Aquí podrías poner el usuario real si tienes autenticación
                Accion = "Consultó la lista de Productos",
                Fecha = DateTime.Now
            };

            await _historicosService.GuardarAsync(registro);
        }

        // Este método se activa cuando presionan el botón rojo de Borrar
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // Llama a tu servicio para que borre el Producto con ese ID
            // NOTA: Asegúrate de tener este método EliminarAsync creado en tu ProductosService
            var exito = await _ProductosService.EliminarAsync(id);

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
