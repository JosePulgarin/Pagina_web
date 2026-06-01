using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Servicios
{
    public class EditModel : PageModel
    {
        private readonly ServiciosService _ServiciosService;

        [BindProperty]
        public ServiciosClase Servicio { get; set; } = new();

        public EditModel(ServiciosService ServiciosService)
        {
            _ServiciosService = ServiciosService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var ServicioEncontrado = await _ServiciosService.ConsultarPorIdAsync(id);
            if (ServicioEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            Servicio = ServicioEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _ServiciosService.ModificarAsync(Servicio);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el Servicio.");
            return Page();
        }
    }
}