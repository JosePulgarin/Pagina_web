using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class PromocionesServiciosUT
    {
        [TestMethod]
        public void ConsultarPromocionesServicios() // Debe retornar una lista de PromocionesServicios (aunque esté vacía)
        {
            PromocionesServiciosNegocio negocio = new PromocionesServiciosNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<PromocionesServicios>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar PromocionServicio O Nulo
        {
            PromocionesServiciosNegocio negocio = new PromocionesServiciosNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is PromocionesServicios);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje PromocionServicior en el pasado)
        // ==========================================

        

        [TestMethod]
        public void GuardarPromocionesServiciosExitoso() // POLICÍA BUENO
        {
            PromocionesServiciosNegocio negocio = new PromocionesServiciosNegocio();
            PromocionesServicios PromocionServicioValida = new PromocionesServicios
            {
                Id = 0,
              
                MontoDescuento = 200.0m,
                DescuentoFinde = 0.30m,
                IdServicio = 4,
                IdPromocionEspecial = 2
            };

            var respuesta = negocio.Guardar(PromocionServicioValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La PromocionServicio válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarPromocionesServicios() //Debe Modificar Y Retornar
        {
            PromocionesServiciosNegocio negocio = new PromocionesServiciosNegocio();

            // Creamos una PromocionServicio VÁLIDA (con fecha de mañana para que no explote tu validación)
            PromocionesServicios temporal = new PromocionesServicios
            {
                Id = 0,
                MontoDescuento = 200.0m,
                DescuentoFinde = 0.30m,
                IdServicio = 4,
                IdPromocionEspecial = 2
            };
            PromocionesServicios guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.DescuentoFinde = 0.14m;
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual(0.14m , respuesta.DescuentoFinde);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarPromocionesServicios() //Debe Retornar True
        {
            PromocionesServiciosNegocio negocio = new PromocionesServiciosNegocio();

            // Creamos basura temporal VÁLIDA
            PromocionesServicios basura = new PromocionesServicios
            {
                Id = 0,
                MontoDescuento = 200.0m,
                DescuentoFinde = 0.30m,
                IdServicio = 4,
                IdPromocionEspecial = 2
            };
            PromocionesServicios guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}