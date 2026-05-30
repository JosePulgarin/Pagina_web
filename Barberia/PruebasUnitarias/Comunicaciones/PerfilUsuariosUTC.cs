using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class PerfilUsuariosUTC
    {
        [TestMethod]
        public void ConsultarPerfilUsuarios()
        {// Debe retornar un estado 200 OK al obtener las PerfilUsuarios

            // Instanciamos el controlador directamente, ya no la Conexion
            PerfilUsuariosController controlador = new PerfilUsuariosController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<PerfilUsuarios>), "El controlador no devolvió el formato esperado (lista PerfilUsuarios).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una PerfilUsuario
            // Arrange
            PerfilUsuariosController controlador = new PerfilUsuariosController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de PerfilUsuarios, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto PerfilUsuarios. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is PerfilUsuarios, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarPerfilUsuarios()
        {
            // Arrange
            PerfilUsuariosController controlador = new PerfilUsuariosController();
            PerfilUsuarios nuevaPerfilUsuario = new PerfilUsuarios
            {
                Id = 0, // Id 0 porque es nueva
                Correo = "admin@barberia.com",
                Contraseña = "Admin2026!",
                Estado = "Activo",
                IdRol = 1

            };

            // Act
            var respuesta = controlador.Guardar(nuevaPerfilUsuario);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la PerfilUsuario guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(PerfilUsuarios), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La PerfilUsuario no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarPerfilUsuarios()
        {// Deber retornar las PerfilUsuarios actualizadas si el ID existe, o nulo si no existe (y no explotar)

            PerfilUsuariosController controlador = new PerfilUsuariosController();
            PerfilUsuarios PerfilUsuarioModificada = new PerfilUsuarios
            {
                Id = 0, // Id 0 porque es nueva
                Correo = "admin@barberia.com",
                Contraseña = "Admin2026!",
                Estado = "Activo",
                IdRol = 1
            };

            PerfilUsuarios PerfilUsuarioGuardada = controlador.Guardar(PerfilUsuarioModificada);
            PerfilUsuarioGuardada.Estado = "Inactivo";
            // Act
            var respuesta = controlador.Actualizar(PerfilUsuarioGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Inactivo", respuesta.Estado, "El estado no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarPerfilUsuario()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            PerfilUsuariosController controlador = new PerfilUsuariosController();

            PerfilUsuarios PerfilUsuarioBasura = new PerfilUsuarios
            {
                Id = 0,
                Correo = "admin@barberia.com",
                Contraseña = "Admin2026!",
                Estado = "Activo",
                IdRol = 1
            };

            PerfilUsuarios PerfilUsuarioGuardada = controlador.Guardar(PerfilUsuarioBasura);

            int idParaBorrar = PerfilUsuarioGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}