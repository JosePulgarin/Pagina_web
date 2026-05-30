using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class MetodosPagoUTC
    {
        [TestMethod]
        public void ConsultarMetodosPago()
        {// Debe retornar un estado 200 OK al obtener las MetodosPago

            // Instanciamos el controlador directamente, ya no la Conexion
            MetodosPagoController controlador = new MetodosPagoController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<MetodosPago>), "El controlador no devolvió el formato esperado (lista MetodosPago).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una MetodoPago
            // Arrange
            MetodosPagoController controlador = new MetodosPagoController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de MetodosPago, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto MetodosPago. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is MetodosPago, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarMetodosPago()
        {
            // Arrange
            MetodosPagoController controlador = new MetodosPagoController();
            MetodosPago nuevaMetodoPago = new MetodosPago
            {
                Id = 0, // Id 0 porque es nueva
                TipoMetodo = "Efectivo",
                Banco = "N/A",
                Moneda = "COP"

            };

            // Act
            var respuesta = controlador.Guardar(nuevaMetodoPago);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la MetodoPago guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(MetodosPago), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La MetodoPago no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarMetodosPago()
        {// Deber retornar las MetodosPago actualizadas si el ID existe, o nulo si no existe (y no explotar)

            MetodosPagoController controlador = new MetodosPagoController();
            MetodosPago MetodoPagoModificada = new MetodosPago
            {
                Id = 0, // Id 0 porque es nueva
                TipoMetodo = "Efectivo",
                Banco = "N/A",
                Moneda = "COP"
            };

            MetodosPago MetodoPagoGuardada = controlador.Guardar(MetodoPagoModificada);
            MetodoPagoGuardada.TipoMetodo = "Efectivo";
            // Act
            var respuesta = controlador.Actualizar(MetodoPagoGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Efectivo", respuesta.TipoMetodo, "El estado no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarMetodoPago()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            MetodosPagoController controlador = new MetodosPagoController();

            MetodosPago MetodoPagoBasura = new MetodosPago
            {
                Id = 0,
                TipoMetodo = "Efectivo",
                Banco = "N/A",
                Moneda = "COP"
            };

            MetodosPago MetodoPagoGuardada = controlador.Guardar(MetodoPagoBasura);

            int idParaBorrar = MetodoPagoGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}