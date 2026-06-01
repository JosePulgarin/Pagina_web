using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PresentacionesLibreriaBarberia.Interfaces;


namespace PresentacionesLibreriaBarberia.Implementaciones
{
    public class Comunicaciones : IComunicaciones
    {
        public async Task<Dictionary<string, object>> Ejecutar(Dictionary<string, object> datos)
        {
            var url = datos["Url"].ToString();
            datos.Remove("Url");

            // 2. Sacamos el Tipo de Petición (POST, GET, PUT, DELETE). Si no hay, asumimos GET.
            var tipoPeticion = datos.ContainsKey("Tipo") ? datos["Tipo"].ToString()!.ToUpper() : "GET";
            if (datos.ContainsKey("Tipo")) datos.Remove("Tipo");


            var stringData = datos.ContainsKey("Entidad") ?
                JsonConvert.SerializeObject(datos["Entidad"]) : "{}";
            var body = new StringContent(stringData, Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(0, 4, 0);

            HttpResponseMessage message;

            switch (tipoPeticion)
            {
                case "POST":
                    message = await httpClient.PostAsync(url, body);
                    break;
                case "PUT":
                    message = await httpClient.PutAsync(url, body);
                    break;
                case "DELETE":
                    // El Delete solo necesita la URL (que ya debería incluir el ID)
                    message = await httpClient.DeleteAsync(url!);
                    break;
                case "GET":
                default:
                    // El Get solo necesita la URL para consultar
                    message = await httpClient.GetAsync(url!);
                    break;
            }


            if (!message.IsSuccessStatusCode)
                throw new Exception($"Error Comunicacion: El servidor respondió con código {(int)message.StatusCode} ");

            var resp = await message.Content.ReadAsStringAsync();
            

            resp = Replace(resp);

            return new Dictionary<string, object>() {
                { "Valor", resp }
            };
        }

        private string Replace(string resp)
        {
            return resp.Replace("\\\\r\\\\n", "")
                .Replace("\\r\\n", "")
                .Replace("\\", "")
                .Replace("\\\"", "\"")
                .Replace("\"", "'")
                .Replace("'[", "[")
                .Replace("]'", "]")
                .Replace("'{'", "{'")
                .Replace("\\\\", "\\")
                .Replace("'}'", "'}")
                .Replace("}'", "}")
                .Replace("\\n", "")
                .Replace("\\r", "")
                .Replace("    ", "")
                .Replace("'{", "{")
                .Replace("\"", "")
                .Replace("  ", "")
                .Replace("null", "''");
        }
    }
}