using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.CategoriasProductos
{
    public class EditModel : PageModel
    {
        private readonly CategoriasProductosService _CategoriasProductosService;

        [BindProperty]
        public CategoriasProductosClase CategoriaProducto { get; set; } = new();

        public EditModel(CategoriasProductosService CategoriasProductosService)
        {
            _CategoriasProductosService = CategoriasProductosService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var CategoriaProductoEncontrado = await _CategoriasProductosService.ConsultarPorIdAsync(id);
            if (CategoriaProductoEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            CategoriaProducto = CategoriaProductoEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _CategoriasProductosService.ModificarAsync(CategoriaProducto);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el CategoriaProducto.");
            return Page();
        }
    }
}