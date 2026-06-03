using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.ReservasServicios
{
    public class EditModel : PageModel
    {
        private readonly ReservasServiciosService _ReservasServiciosService;

        [BindProperty]
        public ReservasServiciosClase ReservaServicio { get; set; } = new();

        public EditModel(ReservasServiciosService ReservasServiciosService)
        {
            _ReservasServiciosService = ReservasServiciosService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var ReservaServicioEncontrado = await _ReservasServiciosService.ConsultarPorIdAsync(id);
            if (ReservaServicioEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            ReservaServicio = ReservaServicioEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _ReservasServiciosService.ModificarAsync(ReservaServicio);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el ReservaServicio.");
            return Page();
        }
    }
}