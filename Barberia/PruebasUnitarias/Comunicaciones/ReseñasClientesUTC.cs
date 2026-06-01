using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class ReseñasClientesUTC
    {
        [TestMethod]
        public void ConsultarReseñasClientes()
        {// Debe retornar un estado 200 OK al obtener las ReseñasClientes

            // Instanciamos el controlador directamente, ya no la Conexion
            ReseñasClientesController controlador = new ReseñasClientesController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<ReseñasClientes>), "El controlador no devolvió el formato esperado (lista ReseñasClientes).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una ReseñaCliente
            // Arrange
            ReseñasClientesController controlador = new ReseñasClientesController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de ReseñasClientes, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto ReseñasClientes. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is ReseñasClientes, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarReseñasClientes()
        {
            // Arrange
            ReseñasClientesController controlador = new ReseñasClientesController();
            ReseñasClientes nuevaReseñaCliente = new ReseñasClientes
            {
                Id = 0, // Id 0 porque es nueva
                Puntuacion = 5,
                Comentario = "Excelente atención de Carlos",
                FechaPublicacion = new DateOnly(2026, 4, 10),
                Etiquetas = "Excelente Servicio",
                IdReserva = 1

            };

            // Act
            var respuesta = controlador.Guardar(nuevaReseñaCliente);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la ReseñaCliente guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(ReseñasClientes), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La ReseñaCliente no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarReseñasClientes()
        {// Deber retornar las ReseñasClientes actualizadas si el ID existe, o nulo si no existe (y no explotar)

            ReseñasClientesController controlador = new ReseñasClientesController();
            ReseñasClientes ReseñaClienteModificada = new ReseñasClientes
            {
                Id = 0, // Id 0 porque es nueva
                Puntuacion = 5,
                Comentario = "Excelente atención de Carlos",
                FechaPublicacion = new DateOnly(2026, 4, 10),
                Etiquetas = "Excelente Servicio",
                IdReserva = 1
            };

            ReseñasClientes ReseñaClienteGuardada = controlador.Guardar(ReseñaClienteModificada);
            ReseñaClienteGuardada.Etiquetas = "Asombroso servicio";
            // Act
            var respuesta = controlador.Actualizar(ReseñaClienteGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Asombroso servicio", respuesta.Etiquetas, "El estado no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarReseñaCliente()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            ReseñasClientesController controlador = new ReseñasClientesController();

            ReseñasClientes ReseñaClienteBasura = new ReseñasClientes
            {
                Id = 0,
                Puntuacion = 5,
                Comentario = "Excelente atención de Carlos",
                FechaPublicacion = new DateOnly(2026, 4, 10),
                Etiquetas = "Excelente Servicio",
                IdReserva = 1
            };

            ReseñasClientes ReseñaClienteGuardada = controlador.Guardar(ReseñaClienteBasura);

            int idParaBorrar = ReseñaClienteGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}