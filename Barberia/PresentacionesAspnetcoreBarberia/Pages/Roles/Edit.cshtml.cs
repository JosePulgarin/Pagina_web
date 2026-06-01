using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Roles
{
    public class EditModel : PageModel
    {
        private readonly RolesService _RolesService;

        [BindProperty]
        public RolesClase Rol { get; set; } = new();

        public EditModel(RolesService RolesService)
        {
            _RolesService = RolesService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var RolEncontrado = await _RolesService.ConsultarPorIdAsync(id);
            if (RolEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            Rol = RolEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _RolesService.ModificarAsync(Rol);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el Rol.");
            return Page();
        }
    }
}