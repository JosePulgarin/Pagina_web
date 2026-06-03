<<<<<<< HEAD
using ClosedXML.Excel;
=======
>>>>>>> 112295c9af2e932238842f9f0beda5235c10b042
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly ProductosService _ProductosService;
        private readonly HistoricosService _historicosService; // INYECCIÓN DE HISTORICOS

        // Usamos nuestra nueva "maleta" de ProductoModel
        public List<ProductosClase> ListaProductos { get; set; } = new List<ProductosClase>();        
        // Inyectamos el servicio moderno
        public IndexModel(ProductosService ProductosService, HistoricosService historicosService)
        {
            _ProductosService = ProductosService;
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
            ListaProductos = await _ProductosService.ConsultarAsync();


            var registro = new HistoricosClase
            {
                Usuario = "Admin", // Aquí podrías poner el usuario real si tienes autenticación
<<<<<<< HEAD
                Entidad = "Productos",
=======
<<<<<<< HEAD
                Entidad = "Productos",
=======
>>>>>>> 7a211c30954c5185a1af436a7a13b3f477101c47
>>>>>>> 112295c9af2e932238842f9f0beda5235c10b042
                Accion = "Consultó la lista de Productos",
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
            // Llama a tu servicio para que borre el Producto con ese ID
            // NOTA: Asegúrate de tener este método EliminarAsync creado en tu ProductosService
            var exito = await _ProductosService.EliminarAsync(id);

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
            var listaDatos = await _ProductosService.ConsultarAsync();

            // 2. Creamos el archivo de Excel virtual
            using (var workbook = new XLWorkbook())
            {
                // Nombre de la pestaña abajo en el Excel
                var hoja = workbook.Worksheets.Add("Reporte de Productos");

                // 3. Pintamos los títulos de las columnas (Fila 1)
                // (Ajuste "ColumnaX" por los nombres reales de los atributos de su clase Productos)
                hoja.Cell(1, 1).Value = "ID Producto";
                hoja.Cell(1, 2).Value = "Marca producto";
                hoja.Cell(1, 3).Value = "Nombre articulo";
                hoja.Cell(1, 4).Value = "Precio";
                hoja.Cell(1, 5).Value = "Stock actual";
                hoja.Cell(1, 6).Value = "Id Inventario";
                hoja.Cell(1, 7).Value = "ID proveedor";
                hoja.Cell(1, 8).Value = "ID categoria";


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
                    hoja.Cell(fila, 2).Value = item.MarcaProducto;
                    hoja.Cell(fila, 3).Value = item.NombreArticulo;
                    hoja.Cell(fila, 4).Value = item.Precio;
                    hoja.Cell(fila, 5).Value = item.StockActual;
                    hoja.Cell(fila, 6).Value = item.IdInventario;
                    hoja.Cell(fila, 7).Value = item.IdProveedor;
                    hoja.Cell(fila, 8).Value = item.IdCategoria;


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
                    return File(contenido, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte_Productos.xlsx");
                }
            }
        }
=======
>>>>>>> 112295c9af2e932238842f9f0beda5235c10b042
    }
}
