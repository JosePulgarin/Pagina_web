using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class InventariosUTC
    {
        [TestMethod]
        public void ConsultarInventarios()
        {// Debe retornar un estado 200 OK al obtener las Inventarios

            // Instanciamos el controlador directamente, ya no la Conexion
            InventariosController controlador = new InventariosController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<Inventarios>), "El controlador no devolvió el formato esperado (lista Inventarios).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una Inventario
            // Arrange
            InventariosController controlador = new InventariosController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de Inventarios, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto Inventarios. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is Inventarios, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarInventarios()
        {
            // Arrange
            InventariosController controlador = new InventariosController();
            Inventarios nuevaInventario = new Inventarios
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Capa Barbero",
                Descripcion = "Capas negras impermeables",
                CantidadActual = 20,
                FechaAbastecimiento = new DateOnly(2026, 3, 15),
                IdSede = 1

            };

            // Act
            var respuesta = controlador.Guardar(nuevaInventario);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la Inventario guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(Inventarios), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La Inventario no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarInventarios()
        {// Deber retornar las Inventarios actualizadas si el ID existe, o nulo si no existe (y no explotar)

            InventariosController controlador = new InventariosController();
            Inventarios InventarioModificada = new Inventarios
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Capa Barbero",
                Descripcion = "Capas negras impermeables",
                CantidadActual = 20,
                FechaAbastecimiento = new DateOnly(2026, 3, 15),
                IdSede = 1
            };

            Inventarios InventarioGuardada = controlador.Guardar(InventarioModificada);
            InventarioGuardada.Nombre = "Cuchillas";
            // Act
            var respuesta = controlador.Actualizar(InventarioGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Cuchillas", respuesta.Nombre, "El estado no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarInventario()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            InventariosController controlador = new InventariosController();

            Inventarios InventarioBasura = new Inventarios
            {
                Id = 0,
                Nombre = "Capa Barbero",
                Descripcion = "Capas negras impermeables",
                CantidadActual = 20,
                FechaAbastecimiento = new DateOnly(2026, 3, 15),
                IdSede = 1
            };

            Inventarios InventarioGuardada = controlador.Guardar(InventarioBasura);

            int idParaBorrar = InventarioGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}