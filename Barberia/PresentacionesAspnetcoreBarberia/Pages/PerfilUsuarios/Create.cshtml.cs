using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.PerfilUsuarios
{
    public class CreateModel : PageModel
    {
        private readonly PerfilUsuariosService _PerfilUsuariosService;

        [BindProperty]
        public PerfilUsuariosClase PerfilUsuario { get; set; } = new();

        public CreateModel(PerfilUsuariosService PerfilUsuariosService)
        {
            _PerfilUsuariosService = PerfilUsuariosService;
        }

        public void OnGet()
        {
            // Se ejecuta al abrir la página. Para PerfilUsuarios suele ir vacío.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Si falta algún dato, recarga la página mostrando errores
            }

            // Llamamos a tu servicio para que mande el POST a tu API
            var exito = await _PerfilUsuariosService.GuardarAsync(PerfilUsuario);

            if (exito) // Corregido: GuardarAsync devuelve un bool (true o false)
            {
                // Si funcionó, lo devolvemos a la tabla (Index)
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al guardar la PerfilUsuario.");
            return Page();
        }
    }
}