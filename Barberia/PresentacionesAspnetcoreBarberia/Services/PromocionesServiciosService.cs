using Newtonsoft.Json;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesLibreriaBarberia.Interfaces; // <-- Importamos nuestro puente

namespace PresentacionesAspnetcoreBarberia.Services
{
    public class PromocionesServiciosService
    {
        private readonly IComunicaciones _puente;

        // La URL base de tu API (Asegúrate de poner el puerto correcto que te da Swagger)
        private readonly string _urlApi = "http://localhost:5247/PromocionesServicios";

        // El constructor recibe el puente mágicamente
        public PromocionesServiciosService(IComunicaciones puente)
        {
            _puente = puente;
        }

        // 1. CONSULTAR
        public async Task<List<PromocionesServiciosClase>> ConsultarAsync()
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

            return JsonConvert.DeserializeObject<List<PromocionesServiciosClase>>(json!) ?? new List<PromocionesServiciosClase>();
        }

        // 2. CONSULTAR POR ID (Para llenar los datos en la pantalla Edit)
        public async Task<PromocionesServiciosClase?> ConsultarPorIdAsync(int id)
        {
            var peticion = new Dictionary<string, object>
            {
                { "Url", $"{_urlApi}/{id}" }, // Le pegamos el ID a la URL (Ej: /PromocionesServicios/5)
                { "Tipo", "GET" }
            };

            var respuesta = await _puente.Ejecutar(peticion);
            var json = respuesta["Valor"].ToString();

            // Si la API no encontró nada, devolvemos null
            if (string.IsNullOrEmpty(json) || json == "''") return null;

            return JsonConvert.DeserializeObject<PromocionesServiciosClase>(json);
        }


        // 3. GUARDAR
        public async Task<bool> GuardarAsync(PromocionesServiciosClase PromocionServicio)
        {
            var peticion = new Dictionary<string, object>
            {
                { "Url", _urlApi },
                { "Tipo", "POST" },
                { "Entidad", PromocionServicio } // Le pasamos los datos para que el puente arme el paquete
            };

            var respuesta = await _puente.Ejecutar(peticion);
            return respuesta["Valor"] != null;
        }

        // 4. MODIFICAR EXISTENTE (Para la pantalla Edit)
        public async Task<bool> ModificarAsync(PromocionesServiciosClase PromocionServicio)
        {
            var peticion = new Dictionary<string, object>
            {
                { "Url", _urlApi },
                { "Tipo", "PUT" },
                { "Entidad", PromocionServicio } // Mandamos el objeto entero (que ya trae su ID adentro)
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