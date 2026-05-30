using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class SedesUTC
    {
        [TestMethod]
        public void ConsultarSedes()
        {// Debe retornar un estado 200 OK al obtener las Sedes

            // Instanciamos el controlador directamente, ya no la Conexion
            SedesController controlador = new SedesController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<Sedes>), "El controlador no devolvió el formato esperado (lista Sedes).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una Sede
            // Arrange
            SedesController controlador = new SedesController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de Sedes, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto Sedes. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is Sedes, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarSedes()
        {
            // Arrange
            SedesController controlador = new SedesController();
            Sedes nuevaSede = new Sedes
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Elite Central",
                Direccion = "Cl. 10 #43-20",
                Ciudad = "Medellín",
                Correo = "central@barberia.com"

            };

            // Act
            var respuesta = controlador.Guardar(nuevaSede);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la Sede guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(Sedes), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La Sede no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarSedes()
        {// Deber retornar las Sedes actualizadas si el ID existe, o nulo si no existe (y no explotar)

            SedesController controlador = new SedesController();
            Sedes SedeModificada = new Sedes
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Elite Central",
                Direccion = "Cl. 10 #43-20",
                Ciudad = "Medellín",
                Correo = "central@barberia.com"
            };

            Sedes SedeGuardada = controlador.Guardar(SedeModificada);
            SedeGuardada.Direccion = "Carrera 80 #39-12";
            // Act
            var respuesta = controlador.Actualizar(SedeGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Carrera 80 #39-12", respuesta.Direccion, "El estado no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarSede()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            SedesController controlador = new SedesController();

            Sedes SedeBasura = new Sedes
            {
                Id = 0,
                Nombre = "Elite Central",
                Direccion = "Cl. 10 #43-20",
                Ciudad = "Medellín",
                Correo = "central@barberia.com"
            };

            Sedes SedeGuardada = controlador.Guardar(SedeBasura);

            int idParaBorrar = SedeGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}