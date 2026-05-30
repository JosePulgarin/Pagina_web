using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Productos
{
    public class EditModel : PageModel
    {
        private readonly ProductosService _ProductosService;

        [BindProperty]
        public ProductosClase Producto { get; set; } = new();

        public EditModel(ProductosService ProductosService)
        {
            _ProductosService = ProductosService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var ProductoEncontrado = await _ProductosService.ConsultarPorIdAsync(id);
            if (ProductoEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            Producto = ProductoEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _ProductosService.ModificarAsync(Producto);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el Producto.");
            return Page();
        }
    }
}