using Newtonsoft.Json;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesLibreriaBarberia.Interfaces; // <-- Importamos nuestro puente

namespace PresentacionesAspnetcoreBarberia.Services
{
    public class AgendasService
    {
        private readonly IComunicaciones _puente;

        // La URL base de tu API (Asegúrate de poner el puerto correcto que te da Swagger)
        private readonly string _urlApi = "http://localhost:5247/Agendas";

        // El constructor recibe el puente mágicamente
        public AgendasService(IComunicaciones puente)
        {
            _puente = puente;
        }

        // 1. CONSULTAR
        public async Task<List<AgendasClase>> ConsultarAsync()
        {
            // Solo le decimos al puente qué queremos hacer
            var peticion = new Dictionary<string, object>
            {
                { "Url", _urlApi },
                { "Tipo", "GET" }
            };

            // El puente hace el trabajo sucio y nos devuelve el valor
            var respuesta = await _puente.Ejecutar(peticion);
            var json = respuesta["Valor"].ToString();

            return JsonConvert.DeserializeObject<List<AgendasClase>>(json!) ?? new List<AgendasClase>();
        }

        // 2. CONSULTAR POR ID (Para llenar los datos en la pantalla Edit)
        public async Task<AgendasClase?> ConsultarPorIdAsync(int id)
        {
            var peticion = new Dictionary<string, object>
            {
                { "Url", $"{_urlApi}/{id}" }, // Le pegamos el ID a la URL (Ej: /Agendas/5)
                { "Tipo", "GET" }
            };

            var respuesta = await _puente.Ejecutar(peticion);
            var json = respuesta["Valor"].ToString();

            // Si la API no encontró nada, devolvemos null
            if (string.IsNullOrEmpty(json) || json == "''") return null;

            return JsonConvert.DeserializeObject<AgendasClase>(json);
        }


        // 3. GUARDAR
        public async Task<bool> GuardarAsync(AgendasClase agenda)
        {
            var peticion = new Dictionary<string, object>
            {
                { "Url", _urlApi },
                { "Tipo", "POST" },
                { "Entidad", agenda } // Le pasamos los datos para que el puente arme el paquete
            };

            var respuesta = await _puente.Ejecutar(peticion);
            return respuesta["Valor"] != null;
        }

        // 4. MODIFICAR EXISTENTE (Para la pantalla Edit)
        public async Task<bool> ModificarAsync(AgendasClase agenda)
        {
            var peticion = new Dictionary<string, object>
            {
                { "Url", _urlApi },
                { "Tipo", "PUT" },
                { "Entidad", agenda } // Mandamos el objeto entero (que ya trae su ID adentro)
            };

            var respuesta = await _puente.Ejecutar(peticion);
            return respuesta["Valor"] != null;
        }

        // 5. ELIMINAR (Para el botón de borrar en la tabla Index)
        public async Task<bool> EliminarAsync(int id)
        {
            var peticion = new Dictionary<string, object>
            {
                { "Url", $"{_urlApi}/{id}" }, // Le pegamos el ID a la URL para que el puente lo borre
                { "Tipo", "DELETE" }
            };

            var respuesta = await _puente.Ejecutar(peticion);
            return respuesta["Valor"] != null;
        }
    }
}