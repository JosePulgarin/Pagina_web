using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Recepcionistas
{
    public class EditModel : PageModel
    {
        private readonly RecepcionistasService _RecepcionistasService;

        [BindProperty]
        public RecepcionistasClase Recepcionista { get; set; } = new();

        public EditModel(RecepcionistasService RecepcionistasService)
        {
            _RecepcionistasService = RecepcionistasService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var RecepcionistaEncontrado = await _RecepcionistasService.ConsultarPorIdAsync(id);
            if (RecepcionistaEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            Recepcionista = RecepcionistaEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _RecepcionistasService.ModificarAsync(Recepcionista);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el Recepcionista.");
            return Page();
        }
    }
}