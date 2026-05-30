using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class CategoriasProductosUTC
    {
        [TestMethod]
        public void ConsultarCategoriasProductos()
        {// Debe retornar un estado 200 OK al obtener las CategoriasProductos

            // Instanciamos el controlador directamente, ya no la Conexion
            CategoriasProductosController controlador = new CategoriasProductosController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<CategoriasProductos>), "El controlador no devolvió el formato esperado (lista CategoriasProductos).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una CategoriaProducto
            // Arrange
            CategoriasProductosController controlador = new CategoriasProductosController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de CategoriasProductos, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto CategoriasProductos. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is CategoriasProductos, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarCategoriasProductos()
        {
            // Arrange
            CategoriasProductosController controlador = new CategoriasProductosController();
            CategoriasProductos nuevaCategoriaProducto = new CategoriasProductos
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Cuidado de Barba",
                Descripcion = "Aceites, bálsamos, ceras y lociones post-afeitado",
                AplicaImpuesto = true,
                Estado = true

            };

            // Act
            var respuesta = controlador.Guardar(nuevaCategoriaProducto);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la CategoriaProducto guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(CategoriasProductos), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La CategoriaProducto no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarCategoriasProductos()
        {// Deber retornar las CategoriasProductos actualizadas si el ID existe, o nulo si no existe (y no explotar)

            CategoriasProductosController controlador = new CategoriasProductosController();
            CategoriasProductos CategoriaProductoModificada = new CategoriasProductos
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Cuidado de Barba",
                Descripcion = "Aceites, bálsamos, ceras y lociones post-afeitado",
                AplicaImpuesto = true,
                Estado = true
            };

            CategoriasProductos CategoriaProductoGuardada = controlador.Guardar(CategoriaProductoModificada);
            CategoriaProductoGuardada.Descripcion = "Bálsamos";
            // Act
            var respuesta = controlador.Actualizar(CategoriaProductoGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Bálsamos", respuesta.Descripcion, "La descripción no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarCategoriaProducto()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            CategoriasProductosController controlador = new CategoriasProductosController();

            CategoriasProductos CategoriaProductoBasura = new CategoriasProductos
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Cuidado de Barba",
                Descripcion = "Aceites, bálsamos, ceras y lociones post-afeitado",
                AplicaImpuesto = true,
                Estado = true
            };

            CategoriasProductos CategoriaProductoGuardada = controlador.Guardar(CategoriaProductoBasura);

            int idParaBorrar = CategoriaProductoGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}