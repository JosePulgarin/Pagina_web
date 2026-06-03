using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.PromocionesServicios
{
    public class EditModel : PageModel
    {
        private readonly PromocionesServiciosService _PromocionesServiciosService;

        [BindProperty]
        public PromocionesServiciosClase PromocionServicio { get; set; } = new();

        public EditModel(PromocionesServiciosService PromocionesServiciosService)
        {
            _PromocionesServiciosService = PromocionesServiciosService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var PromocionServicioEncontrado = await _PromocionesServiciosService.ConsultarPorIdAsync(id);
            if (PromocionServicioEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            PromocionServicio = PromocionServicioEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _PromocionesServiciosService.ModificarAsync(PromocionServicio);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el PromocionServicio.");
            return Page();
        }
    }
}