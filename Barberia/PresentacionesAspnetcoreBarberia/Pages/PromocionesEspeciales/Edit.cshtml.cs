using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.PromocionesEspeciales
{
    public class EditModel : PageModel
    {
        private readonly PromocionesEspecialesService _PromocionesEspecialesService;

        [BindProperty]
        public PromocionesEspecialesClase PromocionEspecial { get; set; } = new();

        public EditModel(PromocionesEspecialesService PromocionesEspecialesService)
        {
            _PromocionesEspecialesService = PromocionesEspecialesService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var PromocionEspecialEncontrado = await _PromocionesEspecialesService.ConsultarPorIdAsync(id);
            if (PromocionEspecialEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            PromocionEspecial = PromocionEspecialEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _PromocionesEspecialesService.ModificarAsync(PromocionEspecial);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el PromocionEspecial.");
            return Page();
        }
    }
}