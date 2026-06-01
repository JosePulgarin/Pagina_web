using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Facturas
{
    public class EditModel : PageModel
    {
        private readonly FacturasService _FacturasService;

        [BindProperty]
        public FacturasClase Factura { get; set; } = new();

        public EditModel(FacturasService FacturasService)
        {
            _FacturasService = FacturasService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var FacturaEncontrado = await _FacturasService.ConsultarPorIdAsync(id);
            if (FacturaEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            Factura = FacturaEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _FacturasService.ModificarAsync(Factura);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el Factura.");
            return Page();
        }
    }
}