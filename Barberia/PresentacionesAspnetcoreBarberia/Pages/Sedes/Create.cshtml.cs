using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Sedes
{
    public class CreateModel : PageModel
    {
        private readonly SedesService _SedesService;

        [BindProperty]
        public SedesClase Sede { get; set; } = new();

        public CreateModel(SedesService SedesService)
        {
            _SedesService = SedesService;
        }

        public void OnGet()
        {
            // Se ejecuta al abrir la página. Para Sedes suele ir vacío.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Si falta algún dato, recarga la página mostrando errores
            }

            // Llamamos a tu servicio para que mande el POST a tu API
            var exito = await _SedesService.GuardarAsync(Sede);

            if (exito) // Corregido: GuardarAsync devuelve un bool (true o false)
            {
                // Si funcionó, lo devolvemos a la tabla (Index)
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al guardar la Sede.");
            return Page();
        }
    }
}