using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Membresias
{
    public class EditModel : PageModel
    {
        private readonly MembresiasService _MembresiasService;

        [BindProperty]
        public MembresiasClase Membresia { get; set; } = new();

        public EditModel(MembresiasService MembresiasService)
        {
            _MembresiasService = MembresiasService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var MembresiaEncontrado = await _MembresiasService.ConsultarPorIdAsync(id);
            if (MembresiaEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            Membresia = MembresiaEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _MembresiasService.ModificarAsync(Membresia);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el Membresia.");
            return Page();
        }
    }
}