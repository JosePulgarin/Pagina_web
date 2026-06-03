using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Clientes
{
    public class EditModel : PageModel
    {
        private readonly ClientesService _ClientesService;

        [BindProperty]
        public ClientesClase Cliente { get; set; } = new();

        public EditModel(ClientesService ClientesService)
        {
            _ClientesService = ClientesService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var ClienteEncontrado = await _ClientesService.ConsultarPorIdAsync(id);
            if (ClienteEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            Cliente = ClienteEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _ClientesService.ModificarAsync(Cliente);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el Cliente.");
            return Page();
        }
    }
}