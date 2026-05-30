using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Portafolios
{
    public class EditModel : PageModel
    {
        private readonly PortafoliosService _PortafoliosService;

        [BindProperty]
        public PortafoliosClase Portafolio { get; set; } = new();

        public EditModel(PortafoliosService PortafoliosService)
        {
            _PortafoliosService = PortafoliosService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var PortafolioEncontrado = await _PortafoliosService.ConsultarPorIdAsync(id);
            if (PortafolioEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            Portafolio = PortafolioEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _PortafoliosService.ModificarAsync(Portafolio);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el Portafolio.");
            return Page();
        }
    }
}