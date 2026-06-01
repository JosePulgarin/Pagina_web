using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.PromocionesEspeciales
{
    public class CreateModel : PageModel
    {
        private readonly PromocionesEspecialesService _PromocionesEspecialesService;

        [BindProperty]
        public PromocionesEspecialesClase PromocionEspecial { get; set; } = new();

        public CreateModel(PromocionesEspecialesService PromocionesEspecialesService)
        {
            _PromocionesEspecialesService = PromocionesEspecialesService;
        }

        public void OnGet()
        {
            // Se ejecuta al abrir la página. Para PromocionesEspeciales suele ir vacío.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Si falta algún dato, recarga la página mostrando errores
            }

            // Llamamos a tu servicio para que mande el POST a tu API
            var exito = await _PromocionesEspecialesService.GuardarAsync(PromocionEspecial);

            if (exito) // Corregido: GuardarAsync devuelve un bool (true o false)
            {
                // Si funcionó, lo devolvemos a la tabla (Index)
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al guardar la PromocionEspecial.");
            return Page();
        }
    }
}