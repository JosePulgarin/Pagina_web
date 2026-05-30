using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class HorariosLaboralesUT
    {
        [TestMethod]
        public void ConsultarHorariosLaborales() // Debe retornar una lista de HorariosLaborales (aunque esté vacía)
        {
            HorariosLaboralesNegocio negocio = new HorariosLaboralesNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<HorariosLaborales>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar HorarioLaboral O Nulo
        {
            HorariosLaboralesNegocio negocio = new HorariosLaboralesNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is HorariosLaborales);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje HorarioLaboralr en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarHorariosLaboralesExitoso() // POLICÍA BUENO
        {
            HorariosLaboralesNegocio negocio = new HorariosLaboralesNegocio();
            HorariosLaborales HorarioLaboralValida = new HorariosLaborales
            {
                Id = 0,
             
                Dia = "Martes",
                HoraApertura = new TimeOnly(14, 0, 0),
                HoraCierre = new TimeOnly(16, 0, 30),
                DiaFestivo = true,
                IdSede = 1
            };

            var respuesta = negocio.Guardar(HorarioLaboralValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La HorarioLaboral válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarHorariosLaborales() //Debe Modificar Y Retornar
        {
            HorariosLaboralesNegocio negocio = new HorariosLaboralesNegocio();

            // Creamos una HorarioLaboral VÁLIDA (con fecha de mañana para que no explote tu validación)
            HorariosLaborales temporal = new HorariosLaborales
            {
                Id = 0,
                Dia = "Martes",
                HoraApertura = new TimeOnly(14, 0, 0),
                HoraCierre = new TimeOnly(16, 0, 30),
                DiaFestivo = true,
                IdSede = 1
            };
            HorariosLaborales guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.HoraApertura = new TimeOnly(8, 0, 0);
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual(new TimeOnly(8, 0, 0), respuesta.HoraApertura);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarHorariosLaborales() //Debe Retornar True
        {
            HorariosLaboralesNegocio negocio = new HorariosLaboralesNegocio();

            // Creamos basura temporal VÁLIDA
            HorariosLaborales basura = new HorariosLaborales
            {
                Id = 0,
                Dia = "Martes",
                HoraApertura = new TimeOnly(14, 0, 0),
                HoraCierre = new TimeOnly(16, 0, 30),
                DiaFestivo = true,
                IdSede = 1
            };
            HorariosLaborales guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}