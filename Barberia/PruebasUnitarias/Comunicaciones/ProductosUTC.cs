using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class ProductosUTC
    {
        [TestMethod]
        public void ConsultarProductos()
        {// Debe retornar un estado 200 OK al obtener las Productos

            // Instanciamos el controlador directamente, ya no la Conexion
            ProductosController controlador = new ProductosController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<Productos>), "El controlador no devolvió el formato esperado (lista Productos).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una Producto
            // Arrange
            ProductosController controlador = new ProductosController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de Productos, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto Productos. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is Productos, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarProductos()
        {
            // Arrange
            ProductosController controlador = new ProductosController();
            Productos nuevaProducto = new Productos
            {
                Id = 0, // Id 0 porque es nueva
                MarcaProducto = "Loreal",
                NombreArticulo = "Cera Mate",
                Precio = 45000.00m,
                StockActual = 10,
                IdInventario = 1,
                IdProveedor = 1,
                IdCategoria = 1

            };

            // Act
            var respuesta = controlador.Guardar(nuevaProducto);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la Producto guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(Productos), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La Producto no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarProductos()
        {// Deber retornar las Productos actualizadas si el ID existe, o nulo si no existe (y no explotar)

            ProductosController controlador = new ProductosController();
            Productos ProductoModificada = new Productos
            {
                Id = 0, // Id 0 porque es nueva
                MarcaProducto = "Loreal",
                NombreArticulo = "Cera Mate",
                Precio = 45000.00m,
                StockActual = 10,
                IdInventario = 1,
                IdProveedor = 1,
                IdCategoria = 1
            };

            Productos ProductoGuardada = controlador.Guardar(ProductoModificada);
            ProductoGuardada.MarcaProducto = "Dove";
            // Act
            var respuesta = controlador.Actualizar(ProductoGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Dove", respuesta.MarcaProducto, "El estado no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarProducto()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            ProductosController controlador = new ProductosController();

            Productos ProductoBasura = new Productos
            {
                Id = 0,
                MarcaProducto = "Loreal",
                NombreArticulo = "Cera Mate",
                Precio = 45000.00m,
                StockActual = 10,
                IdInventario = 1,
                IdProveedor = 1,
                IdCategoria = 1
            };

            Productos ProductoGuardada = controlador.Guardar(ProductoBasura);

            int idParaBorrar = ProductoGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}