using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class PortafoliosUT
    {
        [TestMethod]
        public void ConsultarPortafolios() // Debe retornar una lista de Portafolios (aunque esté vacía)
        {
            PortafoliosNegocio negocio = new PortafoliosNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<Portafolios>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar Portafolio O Nulo
        {
            PortafoliosNegocio negocio = new PortafoliosNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is Portafolios);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje Portafolior en el pasado)
        // ==========================================

    

        [TestMethod]
        public void GuardarPortafoliosExitoso() // POLICÍA BUENO
        {
            PortafoliosNegocio negocio = new PortafoliosNegocio();
            Portafolios PortafolioValida = new Portafolios
            {
                Id = 0,
               
                Ruta = "https://miservidor.com/img/fedex_design1.jpg",
                TituloCorte = "Fedex",
                Descripcion = "Corte como un hongo",
                IdBarbero = 2
            };

            var respuesta = negocio.Guardar(PortafolioValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La Portafolio válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarPortafolios() //Debe Modificar Y Retornar
        {
            PortafoliosNegocio negocio = new PortafoliosNegocio();

            // Creamos una Portafolio VÁLIDA (con fecha de mañana para que no explote tu validación)
            Portafolios temporal = new Portafolios
            {
                Id = 0,
                Ruta = "https://miservidor.com/img/fedex_design1.jpg",
                TituloCorte = "Fedex",
                Descripcion = "Corte como un hongo",
                IdBarbero = 2
            };
            Portafolios guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.Descripcion = "Corte elegante.";
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Corte elegante.", respuesta.Descripcion);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarPortafolios() //Debe Retornar True
        {
            PortafoliosNegocio negocio = new PortafoliosNegocio();

            // Creamos basura temporal VÁLIDA
            Portafolios basura = new Portafolios
            {
                Id = 0,
                Ruta = "https://miservidor.com/img/fedex_design1.jpg",
                TituloCorte = "Fedex",
                Descripcion = "Corte como un hongo",
                IdBarbero = 2
            };
            Portafolios guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}