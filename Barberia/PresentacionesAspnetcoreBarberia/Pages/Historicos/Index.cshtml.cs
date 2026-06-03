<<<<<<< HEAD
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
=======
>>>>>>> 112295c9af2e932238842f9f0beda5235c10b042
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Historicos
{
    public class IndexModel : PageModel
    {
        private readonly HistoricosService _historicosService;

        public List<HistoricosClase> ListaHistoricos { get; set; } = new();

        public IndexModel(HistoricosService historicosService)
        {
            _historicosService = historicosService;
        }

<<<<<<< HEAD
        public async Task<IActionResult> OnGetAsync()
        {
            // 1. EL CADENERO: Revisamos si el usuario tiene la sesión iniciada
            var paseVip = HttpContext.Session.GetString("UsuarioLogueado");
            if (paseVip == null)
            {
                // Si no tiene el sello, lo mandamos directo a la puerta del Login
                return RedirectToPage("/Login");
            }

            // 2. Si pasó el filtro (sí está logueado), traemos los registros comunes y corrientes
            ListaHistoricos = await _historicosService.ConsultarAsync();

            // 3. Le decimos que dibuje la página con los datos
            return Page();
        }

        public async Task<IActionResult> OnGetExportarExcelAsync()
        {
            // 1. Traemos los datos frescos usando su servicio
            var listaDatos = await _historicosService.ConsultarAsync();

            // 2. Creamos el archivo de Excel virtual
            using (var workbook = new XLWorkbook())
            {
                // Nombre de la pestaña abajo en el Excel
                var hoja = workbook.Worksheets.Add("Reporte de Historicos");

                // 3. Pintamos los títulos de las columnas (Fila 1)
                // (Ajuste "ColumnaX" por los nombres reales de los atributos de su clase Historicos)
                hoja.Cell(1, 1).Value = "ID Historico";
                hoja.Cell(1, 2).Value = "Usuario";
                hoja.Cell(1, 3).Value = "Entidad";
                hoja.Cell(1, 4).Value = "Acción";
                hoja.Cell(1, 5).Value = "Fecha";


                // Estilo para que los títulos se vean nivel 5.0 (Opcional pero recomendado)
                var rangoTitulos = hoja.Range("A1:E1"); // Si agrega más columnas, cambie la 'D' por la letra que corresponda
                rangoTitulos.Style.Font.Bold = true;
                rangoTitulos.Style.Fill.BackgroundColor = XLColor.DarkSlateGray;
                rangoTitulos.Style.Font.FontColor = XLColor.White;

                // 4. Llenamos los datos reales (Comenzando en la Fila 2)
                int fila = 2;
                foreach (var item in listaDatos)
                {
                    // Reemplace item.Id, item.Fecha, etc., por las propiedades reales de su clase
                    hoja.Cell(fila, 1).Value = item.Id;
                    hoja.Cell(fila, 2).Value = item.Usuario;
                    hoja.Cell(fila, 3).Value = item.Entidad;
                    hoja.Cell(fila, 4).Value = item.Accion;
                    hoja.Cell(fila, 5).Value = item.Fecha;


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
                    return File(contenido, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte_Historicos.xlsx");
                }
            }
=======
        public async Task OnGetAsync()
        {
            // Traemos los registros de la bitácora
            ListaHistoricos = await _historicosService.ConsultarAsync();
>>>>>>> 112295c9af2e932238842f9f0beda5235c10b042
        }
    }
}