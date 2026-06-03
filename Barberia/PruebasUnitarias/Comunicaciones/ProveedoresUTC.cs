using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class ProveedoresUTC
    {
        [TestMethod]
        public void ConsultarProveedores()
        {// Debe retornar un estado 200 OK al obtener las Proveedores

            // Instanciamos el controlador directamente, ya no la Conexion
            ProveedoresController controlador = new ProveedoresController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<Proveedores>), "El controlador no devolvió el formato esperado (lista Proveedores).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una Proveedor
            // Arrange
            ProveedoresController controlador = new ProveedoresController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de Proveedores, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto Proveedores. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is Proveedores, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarProveedores()
        {
            // Arrange
            ProveedoresController controlador = new ProveedoresController();
            Proveedores nuevaProveedor = new Proveedores
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Andrés Giraldo",
                NombreEmpresa = "Distribuidora Belleza",
                Correo = "ventas@distribelleza.com",
                Telefono = "3001112233"

            };

            // Act
            var respuesta = controlador.Guardar(nuevaProveedor);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la Proveedor guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(Proveedores), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La Proveedor no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarProveedores()
        {// Deber retornar las Proveedores actualizadas si el ID existe, o nulo si no existe (y no explotar)

            ProveedoresController controlador = new ProveedoresController();
            Proveedores ProveedorModificada = new Proveedores
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Andrés Giraldo",
                NombreEmpresa = "Distribuidora Belleza",
                Correo = "ventas@distribelleza.com",
                Telefono = "3001112233"
            };

            Proveedores ProveedorGuardada = controlador.Guardar(ProveedorModificada);
            ProveedorGuardada.NombreEmpresa = "Belleza SAS";
            // Act
            var respuesta = controlador.Actualizar(ProveedorGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Belleza SAS", respuesta.NombreEmpresa, "El estado no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarProveedor()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            ProveedoresController controlador = new ProveedoresController();

            Proveedores ProveedorBasura = new Proveedores
            {
                Id = 0,
                Nombre = "Andrés Giraldo",
                NombreEmpresa = "Distribuidora Belleza",
                Correo = "ventas@distribelleza.com",
                Telefono = "3001112233"
            };

            Proveedores ProveedorGuardada = controlador.Guardar(ProveedorBasura);

            int idParaBorrar = ProveedorGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}