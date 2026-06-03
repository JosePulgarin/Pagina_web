using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Comisiones
{
    public class CreateModel : PageModel
    {
        private readonly ComisionesService _ComisionesService;

        [BindProperty]
        public ComisionesClase Comision { get; set; } = new();

        public CreateModel(ComisionesService ComisionesService)
        {
            _ComisionesService = ComisionesService;
        }

        public void OnGet()
        {
            // Se ejecuta al abrir la página. Para Comisiones suele ir vacío.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Si falta algún dato, recarga la página mostrando errores
            }

            // Llamamos a tu servicio para que mande el POST a tu API
            var exito = await _ComisionesService.GuardarAsync(Comision);

            if (exito) // Corregido: GuardarAsync devuelve un bool (true o false)
            {
                // Si funcionó, lo devolvemos a la tabla (Index)
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al guardar la Comision.");
            return Page();
        }
    }
}