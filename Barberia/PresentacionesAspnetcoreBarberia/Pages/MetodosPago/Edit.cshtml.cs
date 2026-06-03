using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.MetodosPago
{
    public class EditModel : PageModel
    {
        private readonly MetodosPagoService _MetodosPagoService;

        [BindProperty]
        public MetodosPagoClase MetodoPago { get; set; } = new();

        public EditModel(MetodosPagoService MetodosPagoService)
        {
            _MetodosPagoService = MetodosPagoService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var MetodoPagoEncontrado = await _MetodosPagoService.ConsultarPorIdAsync(id);
            if (MetodoPagoEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            MetodoPago = MetodoPagoEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _MetodosPagoService.ModificarAsync(MetodoPago);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el MetodoPago.");
            return Page();
        }
    }
}