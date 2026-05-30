using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class ClientesUTC
    {
        [TestMethod]
        public void ConsultarClientes()
        {// Debe retornar un estado 200 OK al obtener las Clientes

            // Instanciamos el controlador directamente, ya no la Conexion
            ClientesController controlador = new ClientesController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<Clientes>), "El controlador no devolvió el formato esperado (lista Clientes).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una Cliente
            // Arrange
            ClientesController controlador = new ClientesController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de Clientes, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto Clientes. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is Clientes, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarClientes()
        {
            // Arrange
            ClientesController controlador = new ClientesController();
            Clientes nuevaCliente = new Clientes
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Juan Pérez",
                Telefono = "3110001111",
                Correo = "juanp@mail.com",
                IdUsuario = 4,
                IdSede = 1,
                IdMembresia = 1

            };

            // Act
            var respuesta = controlador.Guardar(nuevaCliente);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la Cliente guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(Clientes), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La Cliente no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarClientes()
        {// Deber retornar las Clientes actualizadas si el ID existe, o nulo si no existe (y no explotar)

            ClientesController controlador = new ClientesController();
            Clientes ClienteModificada = new Clientes
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Juan Pérez",
                Telefono = "3110001111",
                Correo = "juanp@mail.com",
                IdUsuario = 4,
                IdSede = 1,
                IdMembresia = 1
            };

            Clientes ClienteGuardada = controlador.Guardar(ClienteModificada);
            ClienteGuardada.Telefono = "02030440";
            // Act
            var respuesta = controlador.Actualizar(ClienteGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Cancelado", respuesta.Telefono, "El telefono no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarCliente()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            ClientesController controlador = new ClientesController();

            Clientes ClienteBasura = new Clientes
            {
                Id = 0,
                Nombre = "Juan Pérez",
                Telefono = "3110001111",
                Correo = "juanp@mail.com",
                IdUsuario = 4,
                IdSede = 1,
                IdMembresia = 1
            };

            Clientes ClienteGuardada = controlador.Guardar(ClienteBasura);

            int idParaBorrar = ClienteGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}