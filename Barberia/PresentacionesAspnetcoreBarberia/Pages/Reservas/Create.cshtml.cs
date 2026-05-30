using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Reservas
{
    public class CreateModel : PageModel
    {
        private readonly ReservasService _ReservasService;

        [BindProperty]
        public ReservasClase Reserva { get; set; } = new();

        public CreateModel(ReservasService ReservasService)
        {
            _ReservasService = ReservasService;
        }

        public void OnGet()
        {
            // Se ejecuta al abrir la página. Para Reservas suele ir vacío.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Si falta algún dato, recarga la página mostrando errores
            }

            // Llamamos a tu servicio para que mande el POST a tu API
            var exito = await _ReservasService.GuardarAsync(Reserva);

            if (exito) // Corregido: GuardarAsync devuelve un bool (true o false)
            {
                // Si funcionó, lo devolvemos a la tabla (Index)
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al guardar la Reserva.");
            return Page();
        }
    }
}