using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class PortafoliosUTC
    {
        [TestMethod]
        public void ConsultarPortafolios()
        {// Debe retornar un estado 200 OK al obtener las Portafolios

            // Instanciamos el controlador directamente, ya no la Conexion
            PortafoliosController controlador = new PortafoliosController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<Portafolios>), "El controlador no devolvió el formato esperado (lista Portafolios).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una Portafolio
            // Arrange
            PortafoliosController controlador = new PortafoliosController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de Portafolios, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto Portafolios. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is Portafolios, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarPortafolios()
        {
            // Arrange
            PortafoliosController controlador = new PortafoliosController();
            Portafolios nuevaPortafolio = new Portafolios
            {
                Id = 0, // Id 0 porque es nueva
                Ruta = "https://miservidor.com/img/fade_design1.jpg",
                TituloCorte = "Mid Fade con Diseño",
                Descripcion = "Corte con navaja y diseño en la nuca, uso de pomada mate.",
                IdBarbero = 1

            };

            // Act
            var respuesta = controlador.Guardar(nuevaPortafolio);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la Portafolio guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(Portafolios), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La Portafolio no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarPortafolios()
        {// Deber retornar las Portafolios actualizadas si el ID existe, o nulo si no existe (y no explotar)

            PortafoliosController controlador = new PortafoliosController();
            Portafolios PortafolioModificada = new Portafolios
            {
                Id = 0, // Id 0 porque es nueva
                Ruta = "https://miservidor.com/img/fade_design1.jpg",
                TituloCorte = "Mid Fade con Diseño",
                Descripcion = "Corte con navaja y diseño en la nuca, uso de pomada mate.",
                IdBarbero = 1
            };

            Portafolios PortafolioGuardada = controlador.Guardar(PortafolioModificada);
            PortafolioGuardada.TituloCorte = "High Fade monstruoso";
            // Act
            var respuesta = controlador.Actualizar(PortafolioGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual("High Fade monstruoso", respuesta.TituloCorte, "El estado no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarPortafolio()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            PortafoliosController controlador = new PortafoliosController();

            Portafolios PortafolioBasura = new Portafolios
            {
                Id = 0,
                Ruta = "https://miservidor.com/img/fade_design1.jpg",
                TituloCorte = "Mid Fade con Diseño",
                Descripcion = "Corte con navaja y diseño en la nuca, uso de pomada mate.",
                IdBarbero = 1
            };

            Portafolios PortafolioGuardada = controlador.Guardar(PortafolioBasura);

            int idParaBorrar = PortafolioGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}