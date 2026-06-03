<<<<<<< HEAD
using ClosedXML.Excel;
=======
>>>>>>> 112295c9af2e932238842f9f0beda5235c10b042
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Portafolios
{
    public class IndexModel : PageModel
    {
        private readonly PortafoliosService _PortafoliosService;
        private readonly HistoricosService _historicosService; // INYECCIÓN DE HISTORICOS

        // Usamos nuestra nueva "maleta" de PortafolioModel
        public List<PortafoliosClase> ListaPortafolios { get; set; } = new List<PortafoliosClase>();        
        // Inyectamos el servicio moderno
        public IndexModel(PortafoliosService PortafoliosService, HistoricosService historicosService)
        {
            _PortafoliosService = PortafoliosService;
            _historicosService = historicosService; // INYECCIÓN DE HISTORICOS
        }

<<<<<<< HEAD
        public async Task<IActionResult> OnGetAsync()
        {

            var paseVip = HttpContext.Session.GetString("UsuarioLogueado");
            if (paseVip == null)
            {
                return RedirectToPage("/Login"); // Si no tiene el sello, pa' fuera
            }

=======
        public async Task OnGetAsync()
        {
>>>>>>> 112295c9af2e932238842f9f0beda5235c10b042
            // Llamada asíncrona limpia a la API
            ListaPortafolios = await _PortafoliosService.ConsultarAsync();


            var registro = new HistoricosClase
            {
                Usuario = "Admin", // Aquí podrías poner el usuario real si tienes autenticación
<<<<<<< HEAD
                Entidad = "Portafolios",
=======
<<<<<<< HEAD
                Entidad = "Portafolios",
=======
>>>>>>> 7a211c30954c5185a1af436a7a13b3f477101c47
>>>>>>> 112295c9af2e932238842f9f0beda5235c10b042
                Accion = "Consultó la lista de Portafolios",
                Fecha = DateTime.Now
            };

            await _historicosService.GuardarAsync(registro);
<<<<<<< HEAD
            return Page();
=======
>>>>>>> 112295c9af2e932238842f9f0beda5235c10b042
        }

        // Este método se activa cuando presionan el botón rojo de Borrar
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // Llama a tu servicio para que borre el Portafolio con ese ID
            // NOTA: Asegúrate de tener este método EliminarAsync creado en tu PortafoliosService
            var exito = await _PortafoliosService.EliminarAsync(id);

            if (exito)
            {
                // Si lo borró, recarga la página para que desaparezca de la tabla
                return RedirectToPage();
            }

            // Si falló, también recarga la página (luego podemos ponerle un mensaje de error)
            return RedirectToPage();
        }
<<<<<<< HEAD

        public async Task<IActionResult> OnGetExportarExcelAsync()
        {
            // 1. Traemos los datos frescos usando su servicio
            var listaDatos = await _PortafoliosService.ConsultarAsync();

            // 2. Creamos el archivo de Excel virtual
            using (var workbook = new XLWorkbook())
            {
                // Nombre de la pestaña abajo en el Excel
                var hoja = workbook.Worksheets.Add("Reporte de Portafolios");

                // 3. Pintamos los títulos de las columnas (Fila 1)
                // (Ajuste "ColumnaX" por los nombres reales de los atributos de su clase Portafolios)
                hoja.Cell(1, 1).Value = "ID Portafolio";
                hoja.Cell(1, 2).Value = "Ruta del portafolio";
                hoja.Cell(1, 3).Value = "Titulo corte";
                hoja.Cell(1, 4).Value = "Descripción";
                hoja.Cell(1, 5).Value = "ID barbero";


                // Estilo para que los títulos se vean nivel 5.0 (Opcional pero recomendado)
                var rangoTitulos = hoja.Range("A1:F1"); // Si agrega más columnas, cambie la 'D' por la letra que corresponda
                rangoTitulos.Style.Font.Bold = true;
                rangoTitulos.Style.Fill.BackgroundColor = XLColor.DarkSlateGray;
                rangoTitulos.Style.Font.FontColor = XLColor.White;

                // 4. Llenamos los datos reales (Comenzando en la Fila 2)
                int fila = 2;
                foreach (var item in listaDatos)
                {
                    // Reemplace item.Id, item.Fecha, etc., por las propiedades reales de su clase
                    hoja.Cell(fila, 1).Value = item.Id;
                    hoja.Cell(fila, 2).Value = item.Ruta;
                    hoja.Cell(fila, 3).Value = item.TituloCorte;
                    hoja.Cell(fila, 5).Value = item.Descripcion;
                    hoja.Cell(fila, 8).Value = item.IdBarbero;


                    fila++;
                }

                // Ajusta el ancho de las columnas automáticamente para que no se vea cortado
                hoja.Columns().AdjustToContents();

                // 5. Preparamos el archivo y se lo enviamos al navegador para que lo descargue
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var contenido = stream.ToArray();

                    // Retornamos el archivo. El último parámetro es el nombre con el que se descargará.
                    return File(contenido, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte_Portafolios.xlsx");
                }
            }
        }
=======
>>>>>>> 112295c9af2e932238842f9f0beda5235c10b042
    }
}
