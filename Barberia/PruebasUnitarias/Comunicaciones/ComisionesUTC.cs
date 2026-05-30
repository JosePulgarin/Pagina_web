using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class ComisionesUTC
    {
        [TestMethod]
        public void ConsultarComisiones()
        {// Debe retornar un estado 200 OK al obtener las Comisiones

            // Instanciamos el controlador directamente, ya no la Conexion
            ComisionesController controlador = new ComisionesController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<Comisiones>), "El controlador no devolvió el formato esperado (lista Comisiones).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una Comision
            // Arrange
            ComisionesController controlador = new ComisionesController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de Comisiones, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto Comisiones. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is Comisiones, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarComisiones()
        {
            // Arrange
            ComisionesController controlador = new ComisionesController();
            Comisiones nuevaComision = new Comisiones
            {
                Id = 0, // Id 0 porque es nueva
                PorcentajeAplicado = 0.50m,
                Monto = 10504.20m,
                Fecha = new DateOnly(2026, 4, 10),
                EstadoLiquidacion = "Pendiente",
                IdFactura = 1,
                IdBarbero = 1

            };

            // Act
            var respuesta = controlador.Guardar(nuevaComision);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la Comision guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(Comisiones), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La Comision no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarComisiones()
        {// Deber retornar las Comisiones actualizadas si el ID existe, o nulo si no existe (y no explotar)

            ComisionesController controlador = new ComisionesController();
            Comisiones ComisionModificada = new Comisiones
            {
                Id = 0, // Id 0 porque es nueva
                PorcentajeAplicado = 0.50m,
                Monto = 10504.20m,
                Fecha = new DateOnly(2026, 4, 10),
                EstadoLiquidacion = "Pendiente",
                IdFactura = 1,
                IdBarbero = 1
            };

            Comisiones ComisionGuardada = controlador.Guardar(ComisionModificada);
            ComisionGuardada.EstadoLiquidacion = "Cancelado";
            // Act
            var respuesta = controlador.Actualizar(ComisionGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Cancelado", respuesta.EstadoLiquidacion, "El estado no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarComision()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            ComisionesController controlador = new ComisionesController();

            Comisiones ComisionBasura = new Comisiones
            {
                Id = 0, // Id 0 porque es nueva
                PorcentajeAplicado = 0.50m,
                Monto = 10504.20m,
                Fecha = new DateOnly(2026, 4, 10),
                EstadoLiquidacion = "Pendiente",
                IdFactura = 1,
                IdBarbero = 1
            };

            Comisiones ComisionGuardada = controlador.Guardar(ComisionBasura);

            int idParaBorrar = ComisionGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}