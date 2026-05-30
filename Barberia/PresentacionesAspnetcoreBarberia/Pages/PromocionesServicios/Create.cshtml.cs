using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.PromocionesServicios
{
    public class CreateModel : PageModel
    {
        private readonly PromocionesServiciosService _PromocionesServiciosService;

        [BindProperty]
        public PromocionesServiciosClase PromocionServicio { get; set; } = new();

        public CreateModel(PromocionesServiciosService PromocionesServiciosService)
        {
            _PromocionesServiciosService = PromocionesServiciosService;
        }

        public void OnGet()
        {
            // Se ejecuta al abrir la página. Para PromocionesServicios suele ir vacío.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Si falta algún dato, recarga la página mostrando errores
            }

            // Llamamos a tu servicio para que mande el POST a tu API
            var exito = await _PromocionesServiciosService.GuardarAsync(PromocionServicio);

            if (exito) // Corregido: GuardarAsync devuelve un bool (true o false)
            {
                // Si funcionó, lo devolvemos a la tabla (Index)
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al guardar la PromocionServicio.");
            return Page();
        }
    }
}