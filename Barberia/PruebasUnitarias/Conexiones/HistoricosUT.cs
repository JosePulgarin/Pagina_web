using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class HistoricosUT
    {
        // ==========================================
        // 1. CONSULTAR (Policía Bueno)
        // ==========================================
        [TestMethod]
        public void ConsultarHistoricos() //Debe Retornar Lista
        {
            // Arrange
            HistoricosNegocio negocio = new HistoricosNegocio();

            // Act
            var respuesta = negocio.Consultar();

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<Historicos>));
        }

        // ==========================================
        // 2. GUARDAR - POLICÍA MALO (Probar validación)
        // ==========================================

        [TestMethod]
        public void GuardarHistoricosNulo() //Entidad Nula Debe Retornar False
        {
            // Arrange
            HistoricosNegocio negocio = new HistoricosNegocio();

            // Act: Le enviamos nulo a propósito para ver si tu "if" se defiende
            var respuesta = negocio.Guardar(null!);

            // Assert: Como le mandamos basura, esperamos que la respuesta sea Falsa
            Assert.IsInstanceOfType(respuesta, typeof(bool));
            Assert.IsFalse(respuesta, "El sistema falló: dejó pasar un objeto nulo.");
        }

        // ==========================================
        // 3. GUARDAR - POLICÍA BUENO (Guardado exitoso)
        // ==========================================
        [TestMethod]
        public void GuardarHistoricosValido() // Debe Retornar True
        {
            // Arrange
            HistoricosNegocio negocio = new HistoricosNegocio();
            Historicos nuevoHistorico = new Historicos
            {
                Id = 0,
                Usuario = "UsuarioDePrueba",
                Entidad = "EntidadDePrueba",
                Accion = "AccionDePrueba"
              
            };

            // Act
            var respuesta = negocio.Guardar(nuevoHistorico);

            // Assert: Como mandamos todo bien, esperamos que devuelva Verdadero
            Assert.IsInstanceOfType(respuesta, typeof(bool));
            Assert.IsTrue(respuesta, "El histórico no se pudo guardar en la base de datos.");
        }
    }
}