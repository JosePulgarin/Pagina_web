using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Proveedores
{
    public class EditModel : PageModel
    {
        private readonly ProveedoresService _ProveedoresService;

        [BindProperty]
        public ProveedoresClase Proveedor { get; set; } = new();

        public EditModel(ProveedoresService ProveedoresService)
        {
            _ProveedoresService = ProveedoresService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var ProveedorEncontrado = await _ProveedoresService.ConsultarPorIdAsync(id);
            if (ProveedorEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            Proveedor = ProveedorEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _ProveedoresService.ModificarAsync(Proveedor);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el Proveedor.");
            return Page();
        }
    }
}