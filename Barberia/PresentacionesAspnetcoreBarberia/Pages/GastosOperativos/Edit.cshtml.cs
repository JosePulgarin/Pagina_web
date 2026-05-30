using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.GastosOperativos
{
    public class EditModel : PageModel
    {
        private readonly GastosOperativosService _GastosOperativosService;

        [BindProperty]
        public GastosOperativosClase GastoOperativo { get; set; } = new();

        public EditModel(GastosOperativosService GastosOperativosService)
        {
            _GastosOperativosService = GastosOperativosService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var GastoOperativoEncontrado = await _GastosOperativosService.ConsultarPorIdAsync(id);
            if (GastoOperativoEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            GastoOperativo = GastoOperativoEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _GastosOperativosService.ModificarAsync(GastoOperativo);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el GastoOperativo.");
            return Page();
        }
    }
}