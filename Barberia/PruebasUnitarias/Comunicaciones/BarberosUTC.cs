using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class BarberosUTC
    {
        [TestMethod]
        public void ConsultarBarberos()
        {// Debe retornar un estado 200 OK al obtener las Barberos

            // Instanciamos el controlador directamente, ya no la Conexion
            BarberosController controlador = new BarberosController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<Barberos>), "El controlador no devolvió el formato esperado (lista Barberos).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una Barbero
            // Arrange
            BarberosController controlador = new BarberosController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de Barberos, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto Barberos. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is Barberos, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarBarberos()
        {
            // Arrange
            BarberosController controlador = new BarberosController();
            Barberos nuevaBarbero = new Barberos
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Carlos Pérez", //2026-04-10
                Correo = "carlos@mail.com", // Cita para las 08:00 AM
                FechaNacimiento = new DateOnly(1990, 5, 15),
                Especialidad = "Degradados",
                Biografia = "Experto en faders urbanos",
                IdUsuario = 2,
                IdSede = 1

            };

            // Act
            var respuesta = controlador.Guardar(nuevaBarbero);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la Barbero guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(Barberos), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La Barbero no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarBarberos()
        {// Deber retornar las Barberos actualizadas si el ID existe, o nulo si no existe (y no explotar)

            BarberosController controlador = new BarberosController();
            Barberos BarberoModificada = new Barberos
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Carlos Pérez", //2026-04-10
                Correo = "carlos@mail.com", // Cita para las 08:00 AM
                FechaNacimiento = new DateOnly(1990, 5, 15),
                Especialidad = "Degradados",
                Biografia = "Experto en faders urbanos",
                IdUsuario = 2,
                IdSede = 1
            };

            Barberos BarberoGuardada = controlador.Guardar(BarberoModificada);
            BarberoGuardada.Biografia = "Experto en low fades";
            // Act
            var respuesta = controlador.Actualizar(BarberoGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Experto en low fades", respuesta.Biografia, "La biografía no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarBarbero()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            BarberosController controlador = new BarberosController();

            Barberos BarberoBasura = new Barberos
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Carlos Pérez", //2026-04-10
                Correo = "carlos@mail.com", // Cita para las 08:00 AM
                FechaNacimiento = new DateOnly(1990, 5, 15),
                Especialidad = "Degradados",
                Biografia = "Experto en faders urbanos",
                IdUsuario = 2,
                IdSede = 1
            };

            Barberos BarberoGuardada = controlador.Guardar(BarberoBasura);

            int idParaBorrar = BarberoGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}