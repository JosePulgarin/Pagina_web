using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Reservas
{
    public class EditModel : PageModel
    {
        private readonly ReservasService _ReservasService;

        [BindProperty]
        public ReservasClase Reserva { get; set; } = new();

        public EditModel(ReservasService ReservasService)
        {
            _ReservasService = ReservasService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var ReservaEncontrado = await _ReservasService.ConsultarPorIdAsync(id);
            if (ReservaEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            Reserva = ReservaEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _ReservasService.ModificarAsync(Reserva);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el Reserva.");
            return Page();
        }
    }
}