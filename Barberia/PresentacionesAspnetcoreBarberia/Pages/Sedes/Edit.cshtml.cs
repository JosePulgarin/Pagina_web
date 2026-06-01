using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Sedes
{
    public class EditModel : PageModel
    {
        private readonly SedesService _SedesService;

        [BindProperty]
        public SedesClase Sede { get; set; } = new();

        public EditModel(SedesService SedesService)
        {
            _SedesService = SedesService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var SedeEncontrado = await _SedesService.ConsultarPorIdAsync(id);
            if (SedeEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            Sede = SedeEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _SedesService.ModificarAsync(Sede);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el Sede.");
            return Page();
        }
    }
}