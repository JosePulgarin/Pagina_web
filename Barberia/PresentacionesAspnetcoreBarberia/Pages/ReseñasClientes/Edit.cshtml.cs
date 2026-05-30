using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.ReseñasClientes
{
    public class EditModel : PageModel
    {
        private readonly ReseñasClientesService _ReseñasClientesService;

        [BindProperty]
        public ReseñasClientesClase ReseñaCliente { get; set; } = new();

        public EditModel(ReseñasClientesService ReseñasClientesService)
        {
            _ReseñasClientesService = ReseñasClientesService;
        }

        // Se ejecuta cuando entras a la página. Recibe el ID de la URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var ReseñaClienteEncontrado = await _ReseñasClientesService.ConsultarPorIdAsync(id);
            if (ReseñaClienteEncontrado == null)
            {
                return RedirectToPage("Index"); // Si no existe, lo devuelve a la tabla
            }

            ReseñaCliente = ReseñaClienteEncontrado; // Llenamos el modelo para que el HTML lo pinte
            return Page();
        }

        // Se ejecuta cuando le das al botón de "Actualizar"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exito = await _ReseñasClientesService.ModificarAsync(ReseñaCliente);

            if (exito)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar el ReseñaCliente.");
            return Page();
        }
    }
}