using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class RolesUTC
    {
        [TestMethod]
        public void ConsultarRoles()
        {// Debe retornar un estado 200 OK al obtener las Roles

            // Instanciamos el controlador directamente, ya no la Conexion
            RolesController controlador = new RolesController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<Roles>), "El controlador no devolvió el formato esperado (lista Roles).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una Rol
            // Arrange
            RolesController controlador = new RolesController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de Roles, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto Roles. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is Roles, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarRoles()
        {
            // Arrange
            RolesController controlador = new RolesController();
            Roles nuevaRol = new Roles
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Administrador",
                Descripcion = "Acceso total al sistema, reportes y configuraciones",
                Estado = true,
                FechaCreacion = DateTime.Now

            };

            // Act
            var respuesta = controlador.Guardar(nuevaRol);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la Rol guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(Roles), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La Rol no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarRoles()
        {// Deber retornar las Roles actualizadas si el ID existe, o nulo si no existe (y no explotar)

            RolesController controlador = new RolesController();
            Roles RolModificada = new Roles
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Administrador",
                Descripcion = "Acceso total al sistema, reportes y configuraciones",
                Estado = true,
                FechaCreacion = DateTime.Now
            };

            Roles RolGuardada = controlador.Guardar(RolModificada);
            RolGuardada.Estado = false;
            // Act
            var respuesta = controlador.Actualizar(RolGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual(false, respuesta.Estado, "El estado no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarRol()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            RolesController controlador = new RolesController();

            Roles RolBasura = new Roles
            {
                Id = 0,
                Nombre = "Administrador",
                Descripcion = "Acceso total al sistema, reportes y configuraciones",
                Estado = true,
                FechaCreacion = DateTime.Now
            };

            Roles RolGuardada = controlador.Guardar(RolBasura);

            int idParaBorrar = RolGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}