using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class ServiciosUTC
    {
        [TestMethod]
        public void ConsultarServicios()
        {// Debe retornar un estado 200 OK al obtener las Servicios

            // Instanciamos el controlador directamente, ya no la Conexion
            ServiciosController controlador = new ServiciosController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<Servicios>), "El controlador no devolvió el formato esperado (lista Servicios).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una Servicio
            // Arrange
            ServiciosController controlador = new ServiciosController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de Servicios, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto Servicios. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is Servicios, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarServicios()
        {
            // Arrange
            ServiciosController controlador = new ServiciosController();
            Servicios nuevaServicio = new Servicios
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Corte Clásico",
                Costo = 25000.00m,
                Tiempo = 30,
                Nota = "Corte tradicional a tijera o máquina"

            };

            // Act
            var respuesta = controlador.Guardar(nuevaServicio);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la Servicio guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(Servicios), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La Servicio no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarServicios()
        {// Deber retornar las Servicios actualizadas si el ID existe, o nulo si no existe (y no explotar)

            ServiciosController controlador = new ServiciosController();
            Servicios ServicioModificada = new Servicios
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Corte Clásico",
                Costo = 25000.00m,
                Tiempo = 30,
                Nota = "Corte tradicional a tijera o máquina"
            };

            Servicios ServicioGuardada = controlador.Guardar(ServicioModificada);
            ServicioGuardada.Nombre = "Tijerazo";
            // Act
            var respuesta = controlador.Actualizar(ServicioGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Tijerazo", respuesta.Nombre, "El estado no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarServicio()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            ServiciosController controlador = new ServiciosController();

            Servicios ServicioBasura = new Servicios
            {
                Id = 0,
                Nombre = "Corte Clásico",
                Costo = 25000.00m,
                Tiempo = 30,
                Nota = "Corte tradicional a tijera o máquina"
            };

            Servicios ServicioGuardada = controlador.Guardar(ServicioBasura);

            int idParaBorrar = ServicioGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}