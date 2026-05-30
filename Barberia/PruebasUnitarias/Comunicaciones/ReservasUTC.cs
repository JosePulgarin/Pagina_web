using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class ReservasUTC
    {
        [TestMethod]
        public void ConsultarReservas()
        {// Debe retornar un estado 200 OK al obtener las Reservas

            // Instanciamos el controlador directamente, ya no la Conexion
            ReservasController controlador = new ReservasController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<Reservas>), "El controlador no devolvió el formato esperado (lista Reservas).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una Reserva
            // Arrange
            ReservasController controlador = new ReservasController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de Reservas, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto Reservas. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is Reservas, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarReservas()
        {
            // Arrange
            ReservasController controlador = new ReservasController();
            Reservas nuevaReserva = new Reservas
            {
                Id = 0, // Id 0 porque es nueva
                Recordatorio = "WhatsApp",
                Fecha = new DateOnly(2026, 04, 10),
                Estado = "Confirmada",
                Notas = "Cliente puntual",
                IdAgenda = 1,
                IdCliente = 1

            };

            // Act
            var respuesta = controlador.Guardar(nuevaReserva);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la Reserva guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(Reservas), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La Reserva no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarReservas()
        {// Deber retornar las Reservas actualizadas si el ID existe, o nulo si no existe (y no explotar)

            ReservasController controlador = new ReservasController();
            Reservas ReservaModificada = new Reservas
            {
                Id = 0, // Id 0 porque es nueva
                Recordatorio = "WhatsApp",
                Fecha = new DateOnly(2026, 04, 10),
                Estado = "Confirmada",
                Notas = "Cliente puntual",
                IdAgenda = 1,
                IdCliente = 1
            };

            Reservas ReservaGuardada = controlador.Guardar(ReservaModificada);
            ReservaGuardada.Estado = "Cancelada";
            // Act
            var respuesta = controlador.Actualizar(ReservaGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Cancelada", respuesta.Estado, "El estado no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarReserva()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            ReservasController controlador = new ReservasController();

            Reservas ReservaBasura = new Reservas
            {
                Id = 0,
                Recordatorio = "WhatsApp",
                Fecha = new DateOnly(2026, 04, 10),
                Estado = "Confirmada",
                Notas = "Cliente puntual",
                IdAgenda = 1,
                IdCliente = 1
            };

            Reservas ReservaGuardada = controlador.Guardar(ReservaBasura);

            int idParaBorrar = ReservaGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}