using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;
using System.IO;

namespace PresentacionesAspnetcoreBarberia.Pages.Comisiones
{
    public class IndexModel : PageModel
    {
        private readonly ComisionesService _ComisionesService;
        private readonly HistoricosService _historicosService; // INYECCIÓN DE HISTORICOS

        // Usamos nuestra nueva "maleta" de ComisionModel
        public List<ComisionesClase> ListaComisiones { get; set; } = new List<ComisionesClase>();        
        // Inyectamos el servicio moderno
        public IndexModel(ComisionesService ComisionesService, HistoricosService historicosService)
        {
            _ComisionesService = ComisionesService;
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
            ListaComisiones = await _ComisionesService.ConsultarAsync();


            var registro = new HistoricosClase
            {
                Usuario = "Admin", // Aquí podrías poner el usuario real si tienes autenticación
                Entidad = "Comisiones",
                Accion = "Consultó la lista de Comisiones",
                Fecha = DateTime.Now
            };

            await _historicosService.GuardarAsync(registro);
            return Page();
        }

        // Este método se activa cuando presionan el botón rojo de Borrar
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // Llama a tu servicio para que borre el Comision con ese ID
            // NOTA: Asegúrate de tener este método EliminarAsync creado en tu ComisionesService
            var exito = await _ComisionesService.EliminarAsync(id);

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
            var listaDatos = await _ComisionesService.ConsultarAsync();

            // 2. Creamos el archivo de Excel virtual
            using (var workbook = new XLWorkbook())
            {
                // Nombre de la pestaña abajo en el Excel
                var hoja = workbook.Worksheets.Add("Reporte de Comisiones");

                // 3. Pintamos los títulos de las columnas (Fila 1)
                // (Ajuste "ColumnaX" por los nombres reales de los atributos de su clase Comisiones)
                hoja.Cell(1, 1).Value = "ID Comision";
                hoja.Cell(1, 2).Value = "Porcentaje aplicado";
                hoja.Cell(1, 3).Value = "Monto";
                hoja.Cell(1, 4).Value = "Fecha";
                hoja.Cell(1, 5).Value = "Estado";
                hoja.Cell(1, 7).Value = "ID factura";
                hoja.Cell(1, 8).Value = "ID barbero";


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
                    hoja.Cell(fila, 2).Value = item.PorcentajeAplicado;
                    hoja.Cell(fila, 3).Value = item.Monto;
                    hoja.Cell(fila, 4).Value = item.Fecha.ToDateTime(TimeOnly.MinValue);
                    hoja.Cell(fila, 5).Value = item.EstadoLiquidacion;
                    hoja.Cell(fila, 6).Value = item.IdFactura;
                    hoja.Cell(fila, 7).Value = item.IdBarbero;
    


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
                    return File(contenido, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte_Comisiones.xlsx");
                }
            }
        }
    }
}
