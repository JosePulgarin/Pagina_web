using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Agendas
{
    public class EditModel : PageModel
    {
        private readonly AgendasService _AgendasService;

        [BindProperty]
        public AgendasClase Agenda { get; set; } = new();

        public EditModel(AgendasService AgendasService)
        {
            _AgendasService = AgendasService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var AgendaEncontrado = await _AgendasService.ConsultarPorIdAsync(id);
            if (AgendaEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            Agenda = AgendaEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _AgendasService.ModificarAsync(Agenda);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el Agenda.");
            return Page();
        }
    }
}