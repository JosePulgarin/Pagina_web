using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.HorariosLaborales
{
    public class EditModel : PageModel
    {
        private readonly HorariosLaboralesService _HorariosLaboralesService;

        [BindProperty]
        public HorariosLaboralesClase HorarioLaboral { get; set; } = new();

        public EditModel(HorariosLaboralesService HorariosLaboralesService)
        {
            _HorariosLaboralesService = HorariosLaboralesService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var HorarioLaboralEncontrado = await _HorariosLaboralesService.ConsultarPorIdAsync(id);
            if (HorarioLaboralEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            HorarioLaboral = HorarioLaboralEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _HorariosLaboralesService.ModificarAsync(HorarioLaboral);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el HorarioLaboral.");
            return Page();
        }
    }
}