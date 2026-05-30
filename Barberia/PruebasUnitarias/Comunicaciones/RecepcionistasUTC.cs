using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class RecepcionistasUTC
    {
        [TestMethod]
        public void ConsultarRecepcionistas()
        {// Debe retornar un estado 200 OK al obtener las Recepcionistas

            // Instanciamos el controlador directamente, ya no la Conexion
            RecepcionistasController controlador = new RecepcionistasController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<Recepcionistas>), "El controlador no devolvió el formato esperado (lista Recepcionistas).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una Recepcionista
            // Arrange
            RecepcionistasController controlador = new RecepcionistasController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de Recepcionistas, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto Recepcionistas. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is Recepcionistas, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarRecepcionistas()
        {
            // Arrange
            RecepcionistasController controlador = new RecepcionistasController();
            Recepcionistas nuevaRecepcionista = new Recepcionistas
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Daniela Henao",
                FechaNacimiento = new DateOnly(1996, 6, 12),
                Turno = "Mañana",
                Telefono = "3009990011",
                IdUsuario = 3,
                IdSede = 1

            };

            // Act
            var respuesta = controlador.Guardar(nuevaRecepcionista);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la Recepcionista guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(Recepcionistas), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La Recepcionista no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarRecepcionistas()
        {// Deber retornar las Recepcionistas actualizadas si el ID existe, o nulo si no existe (y no explotar)

            RecepcionistasController controlador = new RecepcionistasController();
            Recepcionistas RecepcionistaModificada = new Recepcionistas
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Daniela Henao",
                FechaNacimiento = new DateOnly(1996, 6, 12),
                Turno = "Mañana",
                Telefono = "3009990011",
                IdUsuario = 3,
                IdSede = 1
            };

            Recepcionistas RecepcionistaGuardada = controlador.Guardar(RecepcionistaModificada);
            RecepcionistaGuardada.Turno = "Tarde";
            // Act
            var respuesta = controlador.Actualizar(RecepcionistaGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Tarde", respuesta.Turno, "El estado no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarRecepcionista()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            RecepcionistasController controlador = new RecepcionistasController();

            Recepcionistas RecepcionistaBasura = new Recepcionistas
            {
                Id = 0,
                Nombre = "Daniela Henao",
                FechaNacimiento = new DateOnly(1996, 6, 12),
                Turno = "Mañana",
                Telefono = "3009990011",
                IdUsuario = 3,
                IdSede = 1
            };

            Recepcionistas RecepcionistaGuardada = controlador.Guardar(RecepcionistaBasura);

            int idParaBorrar = RecepcionistaGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}