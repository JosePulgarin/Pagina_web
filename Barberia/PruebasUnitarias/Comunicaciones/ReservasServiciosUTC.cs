using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class ReservasServiciosUTC
    {
        [TestMethod]
        public void ConsultarReservasServicios()
        {// Debe retornar un estado 200 OK al obtener las ReservasServicios

            // Instanciamos el controlador directamente, ya no la Conexion
            ReservasServiciosController controlador = new ReservasServiciosController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<ReservasServicios>), "El controlador no devolvió el formato esperado (lista ReservasServicios).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una ReservaServicio
            // Arrange
            ReservasServiciosController controlador = new ReservasServiciosController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de ReservasServicios, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto ReservasServicios. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is ReservasServicios, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarReservasServicios()
        {
            // Arrange
            ReservasServiciosController controlador = new ReservasServiciosController();
            ReservasServicios nuevaReservaServicio = new ReservasServicios
            {
                Id = 0, // Id 0 porque es nueva
                Precio = 25000.00m,
                Observacion = "Sin shampoo",
                IdServicio = 1,
                IdReserva = 1

            };

            // Act
            var respuesta = controlador.Guardar(nuevaReservaServicio);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la ReservaServicio guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(ReservasServicios), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La ReservaServicio no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarReservasServicios()
        {// Deber retornar las ReservasServicios actualizadas si el ID existe, o nulo si no existe (y no explotar)

            ReservasServiciosController controlador = new ReservasServiciosController();
            ReservasServicios ReservaServicioModificada = new ReservasServicios
            {
                Id = 0, // Id 0 porque es nueva
                Precio = 25000.00m,
                Observacion = "Sin shampoo",
                IdServicio = 1,
                IdReserva = 1
            };

            ReservasServicios ReservaServicioGuardada = controlador.Guardar(ReservaServicioModificada);
            ReservaServicioGuardada.Observacion = "Solo barba";
            // Act
            var respuesta = controlador.Actualizar(ReservaServicioGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Solo barba", respuesta.Observacion, "El estado no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarReservaServicio()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            ReservasServiciosController controlador = new ReservasServiciosController();

            ReservasServicios ReservaServicioBasura = new ReservasServicios
            {
                Id = 0,
                Precio = 25000.00m,
                Observacion = "Sin shampoo",
                IdServicio = 1,
                IdReserva = 1
            };

            ReservasServicios ReservaServicioGuardada = controlador.Guardar(ReservaServicioBasura);

            int idParaBorrar = ReservaServicioGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}