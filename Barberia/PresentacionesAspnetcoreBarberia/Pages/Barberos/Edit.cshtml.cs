using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Barberos
{
    public class EditModel : PageModel
    {
        private readonly BarberosService _BarberosService;

        [BindProperty]
        public BarberosClase Barbero { get; set; } = new();

        public EditModel(BarberosService BarberosService)
        {
            _BarberosService = BarberosService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var BarberoEncontrado = await _BarberosService.ConsultarPorIdAsync(id);
            if (BarberoEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            Barbero = BarberoEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _BarberosService.ModificarAsync(Barbero);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el Barbero.");
            return Page();
        }
    }
}