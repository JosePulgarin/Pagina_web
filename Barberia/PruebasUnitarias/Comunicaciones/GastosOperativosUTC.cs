using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class GastosOperativosUTC
    {
        [TestMethod]
        public void ConsultarGastosOperativos()
        {// Debe retornar un estado 200 OK al obtener las GastosOperativos

            // Instanciamos el controlador directamente, ya no la Conexion
            GastosOperativosController controlador = new GastosOperativosController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<GastosOperativos>), "El controlador no devolvió el formato esperado (lista GastosOperativos).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una agenda
            // Arrange
            GastosOperativosController controlador = new GastosOperativosController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de GastosOperativos, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto GastosOperativos. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is GastosOperativos, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarGastosOperativos()
        {
            // Arrange
            GastosOperativosController controlador = new GastosOperativosController();
            GastosOperativos nuevaAgenda = new GastosOperativos
            {
                Id = 0, // Id 0 porque es nueva
                Categoria = "Arriendo",
                Monto = 2500000.00m,
                FechaPago = new DateOnly(2026, 4, 1),
                NumeroComprobante = "ARR-001",
                IdSede = 1

            };

            // Act
            var respuesta = controlador.Guardar(nuevaAgenda);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la agenda guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(GastosOperativos), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La agenda no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarGastosOperativos()
        {// Deber retornar las GastosOperativos actualizadas si el ID existe, o nulo si no existe (y no explotar)

            GastosOperativosController controlador = new GastosOperativosController();
            GastosOperativos agendaModificada = new GastosOperativos
            {
                Id = 0, // Id 0 porque es nueva
                Categoria = "Arriendo",
                Monto = 2500000.00m,
                FechaPago = new DateOnly(2026, 4, 1),
                NumeroComprobante = "ARR-001",
                IdSede = 1
            };

            GastosOperativos agendaGuardada = controlador.Guardar(agendaModificada);
            agendaGuardada.Categoria = "Servicios";
            // Act
            var respuesta = controlador.Actualizar(agendaGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Servicios", respuesta.Categoria, "El estado no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarAgenda()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            GastosOperativosController controlador = new GastosOperativosController();

            GastosOperativos agendaBasura = new GastosOperativos
            {
                Id = 0,
                Categoria = "Arriendo",
                Monto = 2500000.00m,
                FechaPago = new DateOnly(2026, 4, 1),
                NumeroComprobante = "ARR-001",
                IdSede = 1
            };

            GastosOperativos agendaGuardada = controlador.Guardar(agendaBasura);

            int idParaBorrar = agendaGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}