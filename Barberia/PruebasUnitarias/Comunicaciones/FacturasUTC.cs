using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class FacturasUTC
    {
        [TestMethod]
        public void ConsultarFacturas()
        {// Debe retornar un estado 200 OK al obtener las Facturas

            // Instanciamos el controlador directamente, ya no la Conexion
            FacturasController controlador = new FacturasController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<Facturas>), "El controlador no devolvió el formato esperado (lista Facturas).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una Factura
            // Arrange
            FacturasController controlador = new FacturasController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de Facturas, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto Facturas. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is Facturas, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarFacturas()
        {
            // Arrange
            FacturasController controlador = new FacturasController();
            Facturas nuevaFactura = new Facturas
            {
                Id = 0, // Id 0 porque es nueva
                NumeroFactura = "FAC-1001",
                MontoSubTotal = 21008.40m,
                IVA = 0.19m,
                Total = 25000.00m,
                IdReserva = 1,
                IdMetodo = 1

            };

            // Act
            var respuesta = controlador.Guardar(nuevaFactura);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la Factura guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(Facturas), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La Factura no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarFacturas()
        {// Deber retornar las Facturas actualizadas si el ID existe, o nulo si no existe (y no explotar)

            FacturasController controlador = new FacturasController();
            Facturas FacturaModificada = new Facturas
            {
                Id = 0, // Id 0 porque es nueva
                NumeroFactura = "FAC-1001",
                MontoSubTotal = 21008.40m,
                IVA = 0.19m,
                Total = 25000.00m,
                IdReserva = 1,
                IdMetodo = 1
            };

            Facturas FacturaGuardada = controlador.Guardar(FacturaModificada);
            FacturaGuardada.IVA = 0.20m;
            // Act
            var respuesta = controlador.Actualizar(FacturaGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual(0.20m, respuesta.IVA, "El IVA no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarFactura()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            FacturasController controlador = new FacturasController();

            Facturas FacturaBasura = new Facturas
            {
                Id = 0,
                NumeroFactura = "FAC-1001",
                MontoSubTotal = 21008.40m,
                IVA = 0.19m,
                Total = 25000.00m,
                IdReserva = 1,
                IdMetodo = 1
            };

            Facturas FacturaGuardada = controlador.Guardar(FacturaBasura);

            int idParaBorrar = FacturaGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}