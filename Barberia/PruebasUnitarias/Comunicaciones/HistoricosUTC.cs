using Appi_Bailes_JP.Controllers;
using AppiBarberia.Controllers;
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class HistoricosUTC
    {
        [TestMethod]
        public void ConsultarHistoricos()
        {// Debe retornar un estado 200 OK al obtener las Historicos

            // Instanciamos el controlador directamente, ya no la Conexion
            HistoricosController controlador = new HistoricosController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<Historicos>), "El controlador no devolvió el formato esperado (lista Historicos).");
        }

        [TestMethod]
        public void GuardarHistoricos()
        {
            // Arrange
 
            HistoricosController controlador = new HistoricosController();

            Historicos nuevoHistorico = new Historicos
            {
                Id = 0,
                Usuario = "Admin_Juan",
                Entidad = "Roles",
                Accion = "Creó los 3 roles iniciales del sistema",
                Fecha = DateTime.Now

            };

            // Act
            var respuesta = controlador.Guardar(nuevoHistorico);

            // Assert
            // Ahora evaluamos que la respuesta sea un booleano y que sea Verdadero
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El histórico no se guardó correctamente (devolvió False).");
        }

    }
}