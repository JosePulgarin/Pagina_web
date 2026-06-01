using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Servicios
{
    public class CreateModel : PageModel
    {
        private readonly ServiciosService _ServiciosService;

        [BindProperty]
        public ServiciosClase Servicio { get; set; } = new();

        public CreateModel(ServiciosService ServiciosService)
        {
            _ServiciosService = ServiciosService;
        }

        public void OnGet()
        {
            // Se ejecuta al abrir la página. Para Servicios suele ir vacío.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Si falta algún dato, recarga la página mostrando errores
            }

            // Llamamos a tu servicio para que mande el POST a tu API
            var exito = await _ServiciosService.GuardarAsync(Servicio);

            if (exito) // Corregido: GuardarAsync devuelve un bool (true o false)
            {
                // Si funcionó, lo devolvemos a la tabla (Index)
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al guardar la Servicio.");
            return Page();
        }
    }
}