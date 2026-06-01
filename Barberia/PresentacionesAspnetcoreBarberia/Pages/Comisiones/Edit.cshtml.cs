using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Comisiones
{
    public class EditModel : PageModel
    {
        private readonly ComisionesService _ComisionesService;

        [BindProperty]
        public ComisionesClase Comision { get; set; } = new();

        public EditModel(ComisionesService ComisionesService)
        {
            _ComisionesService = ComisionesService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var ComisionEncontrado = await _ComisionesService.ConsultarPorIdAsync(id);
            if (ComisionEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            Comision = ComisionEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _ComisionesService.ModificarAsync(Comision);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el Comision.");
            return Page();
        }
    }
}