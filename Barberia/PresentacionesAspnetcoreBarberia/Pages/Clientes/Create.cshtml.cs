using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Clientes
{
    public class CreateModel : PageModel
    {
        private readonly ClientesService _ClientesService;

        [BindProperty]
        public ClientesClase Cliente { get; set; } = new();

        public CreateModel(ClientesService ClientesService)
        {
            _ClientesService = ClientesService;
        }

        public void OnGet()
        {
            // Se ejecuta al abrir la página. Para Clientes suele ir vacío.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Si falta algún dato, recarga la página mostrando errores
            }

            // Llamamos a tu servicio para que mande el POST a tu API
            var exito = await _ClientesService.GuardarAsync(Cliente);

            if (exito) // Corregido: GuardarAsync devuelve un bool (true o false)
            {
                // Si funcionó, lo devolvemos a la tabla (Index)
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al guardar la Cliente.");
            return Page();
        }
    }
}