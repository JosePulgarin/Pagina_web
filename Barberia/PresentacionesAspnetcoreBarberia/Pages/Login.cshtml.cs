using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Services;
// Nota: Si Visual Studio le subraya en rojo el nombre del servicio, dele Alt + Enter
// para importar el 'using' de la carpeta donde tenga sus servicios.

namespace PresentacionesAspnetcoreBarberia.Pages
{
    public class LoginModel : PageModel
    {
        // 1. Inyectamos el servicio que se comunica con su API para la tabla perfilUsuarios.
        // Si su servicio se llama diferente (ej: UsuariosService), cámbielo aquí:
        private readonly PerfilUsuariosService _usuariosService;

        // Constructor para recibir el servicio
        public LoginModel(PerfilUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        public string? MensajeError { get; set; }

        public void OnGet()
        {
            // Cuando la página carga por primera vez, no hacemos nada
        }

        // 2. Cambiamos a OnPostAsync porque vamos a ir a la base de datos de forma asincrónica
        public async Task<IActionResult> OnPostAsync(string Correo, string Clave)
        {
            try
            {
                // 3. Traemos los usuarios usando su servicio. 
                // (Si su método no se llama ConsultarAsync(), ponga el nombre correcto aquí)
                var listaUsuarios = await _usuariosService.ConsultarAsync();

                // 4. Buscamos en la lista si hay alguien que coincida con el correo y la clave EXACTOS
                // OJO: Asegúrese de que las propiedades se llamen 'Correo' y 'Clave' en su modelo PerfilUsuarios, 
                // si se llaman diferente (ej: Email y Password), cámbielo aquí abajo:
                var usuarioValido = listaUsuarios.FirstOrDefault(u => u.Correo == Correo && u.Contraseña == Clave);

                if (usuarioValido != null)
                {
                    // ¡Datos correctos! Le damos el pase VIP guardando su correo en la sesión
                    HttpContext.Session.SetString("UsuarioLogueado", usuarioValido.Correo!);

                    // Lo mandamos a la página principal de Agendas
                    return RedirectToPage("/Agendas/Index");
                }
                else
                {
                    // Datos incorrectos o no existe el usuario
                    MensajeError = "Correo o contraseña incorrectos, parce. Revise e intente de nuevo.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                // Si la API falla, estalla, o está apagada, capturamos el error para que no salga la pantalla negra
                MensajeError = "Error al conectar con la base de datos: " + ex.Message;
                return Page();
            }
        }
    }
}