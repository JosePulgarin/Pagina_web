using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.HorariosLaborales
{
    public class IndexModel : PageModel
    {
        private readonly HorariosLaboralesService _HorariosLaboralesService;
        private readonly HistoricosService _historicosService; // INYECCIÓN DE HISTORICOS

        // Usamos nuestra nueva "maleta" de HorarioLaboralModel
        public List<HorariosLaboralesClase> ListaHorariosLaborales { get; set; } = new List<HorariosLaboralesClase>();        
        // Inyectamos el servicio moderno
        public IndexModel(HorariosLaboralesService HorariosLaboralesService, HistoricosService historicosService)
        {
            _HorariosLaboralesService = HorariosLaboralesService;
            _historicosService = historicosService; // INYECCIÓN DE HISTORICOS
        }

        public async Task<IActionResult> OnGetAsync()
        {

            var paseVip = HttpContext.Session.GetString("UsuarioLogueado");
            if (paseVip == null)
            {
                return RedirectToPage("/Login"); // Si no tiene el sello, pa' fuera
            }

            // Llamada asíncrona limpia a la API
            ListaHorariosLaborales = await _HorariosLaboralesService.ConsultarAsync();


            var registro = new HistoricosClase
            {
                Usuario = "Admin", // Aquí podrías poner el usuario real si tienes autenticación
                Entidad = "HorariosLaborales",
                Accion = "Consultó la lista de HorariosLaborales",
                Fecha = DateTime.Now
            };

            await _historicosService.GuardarAsync(registro);
            return Page();
        }

        // Este método se activa cuando presionan el botón rojo de Borrar
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // Llama a tu servicio para que borre el HorarioLaboral con ese ID
            // NOTA: Asegúrate de tener este método EliminarAsync creado en tu HorariosLaboralesService
            var exito = await _HorariosLaboralesService.EliminarAsync(id);

            if (exito)
            {
                // Si lo borró, recarga la página para que desaparezca de la tabla
                return RedirectToPage();
            }

            // Si falló, también recarga la página (luego podemos ponerle un mensaje de error)
            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetExportarExcelAsync()
        {
            // 1. Traemos los datos frescos usando su servicio
            var listaDatos = await _HorariosLaboralesService.ConsultarAsync();

            // 2. Creamos el archivo de Excel virtual
            using (var workbook = new XLWorkbook())
            {
                // Nombre de la pestaña abajo en el Excel
                var hoja = workbook.Worksheets.Add("HorariosLaborales");

                // 3. Pintamos los títulos de las columnas (Fila 1)
                // (Ajuste "ColumnaX" por los nombres reales de los atributos de su clase HorariosLaborales)
                hoja.Cell(1, 1).Value = "ID HorarioLaboral";
                hoja.Cell(1, 2).Value = "Dia";
                hoja.Cell(1, 3).Value = "Hora apertura";
                hoja.Cell(1, 4).Value = "Hora cierre";
                hoja.Cell(1, 5).Value = "¿Dia festivo?";
                hoja.Cell(1, 8).Value = "ID sede";


                // Estilo para que los títulos se vean nivel 5.0 (Opcional pero recomendado)
                var rangoTitulos = hoja.Range("A1:H1"); // Si agrega más columnas, cambie la 'D' por la letra que corresponda
                rangoTitulos.Style.Font.Bold = true;
                rangoTitulos.Style.Fill.BackgroundColor = XLColor.DarkSlateGray;
                rangoTitulos.Style.Font.FontColor = XLColor.White;

                // 4. Llenamos los datos reales (Comenzando en la Fila 2)
                int fila = 2;
                foreach (var item in listaDatos)
                {
                    // Reemplace item.Id, item.Fecha, etc., por las propiedades reales de su clase
                    hoja.Cell(fila, 1).Value = item.Id;
                    hoja.Cell(fila, 2).Value = item.Dia;
                    hoja.Cell(fila, 3).Value = item.HoraApertura.ToTimeSpan();
                    hoja.Cell(fila, 4).Value = item.HoraCierre.ToTimeSpan();
                    hoja.Cell(fila, 5).Value = item.DiaFestivo;
                    hoja.Cell(fila, 6).Value = item.IdSede;


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
                    return File(contenido, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte_HorariosLaborales.xlsx");
                }
            }
        }
    }
}
