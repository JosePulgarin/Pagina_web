using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.PerfilUsuarios
{
    public class EditModel : PageModel
    {
        private readonly PerfilUsuariosService _PerfilUsuariosService;

        [BindProperty]
        public PerfilUsuariosClase PerfilUsuario { get; set; } = new();

        public EditModel(PerfilUsuariosService PerfilUsuariosService)
        {
            _PerfilUsuariosService = PerfilUsuariosService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var PerfilUsuarioEncontrado = await _PerfilUsuariosService.ConsultarPorIdAsync(id);
            if (PerfilUsuarioEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            PerfilUsuario = PerfilUsuarioEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _PerfilUsuariosService.ModificarAsync(PerfilUsuario);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el PerfilUsuario.");
            return Page();
        }
    }
}