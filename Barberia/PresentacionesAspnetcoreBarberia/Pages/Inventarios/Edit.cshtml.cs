using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Inventarios
{
    public class EditModel : PageModel
    {
        private readonly InventariosService _InventariosService;

        [BindProperty]
        public InventariosClase Inventario { get; set; } = new();

        public EditModel(InventariosService InventariosService)
        {
            _InventariosService = InventariosService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var InventarioEncontrado = await _InventariosService.ConsultarPorIdAsync(id);
            if (InventarioEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            Inventario = InventarioEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _InventariosService.ModificarAsync(Inventario);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el Inventario.");
            return Page();
        }
    }
}