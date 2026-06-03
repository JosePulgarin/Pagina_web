using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class PerfilUsuariosUT
    {
        [TestMethod]
        public void ConsultarPerfilUsuarios() // Debe retornar una lista de PerfilUsuarios (aunque esté vacía)
        {
            PerfilUsuariosNegocio negocio = new PerfilUsuariosNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<PerfilUsuarios>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar PerfilUsuario O Nulo
        {
            PerfilUsuariosNegocio negocio = new PerfilUsuariosNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is PerfilUsuarios);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje PerfilUsuarior en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarPerfilUsuarios() // Este rompe a proposito las validaciones (POLICIA MALO)
        {
            PerfilUsuariosNegocio negocio = new PerfilUsuariosNegocio();
            PerfilUsuarios PerfilUsuarioPasada = new PerfilUsuarios
            {
                Id = 0,
                
                Correo = "carlos.barber@mail.com", // Repetido
                Contraseña = "lopezito20254",
                Estado = "Activo",
                IdRol = 2


            };

            var excepcion = Assert.ThrowsException<Exception>(() => negocio.Guardar(PerfilUsuarioPasada));
            // Validamos que el mensaje contenga "pasado" (como lo escribiste en tu Exception)
            Assert.IsTrue(excepcion.Message.Contains("pasado"), "El error de fecha no fue el esperado.");
        }

        [TestMethod]
        public void GuardarPerfilUsuariosExitoso() // POLICÍA BUENO
        {
            PerfilUsuariosNegocio negocio = new PerfilUsuariosNegocio();
            PerfilUsuarios PerfilUsuarioValida = new PerfilUsuarios
            {
                Id = 0,
                Correo = "pepix.barber@mail.com",
                Contraseña = "lopezito20254",
                Estado = "Activo",
                IdRol = 2
            };

            var respuesta = negocio.Guardar(PerfilUsuarioValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La PerfilUsuario válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarPerfilUsuarios() //Debe Modificar Y Retornar
        {
            PerfilUsuariosNegocio negocio = new PerfilUsuariosNegocio();

            // Creamos una PerfilUsuario VÁLIDA (con fecha de mañana para que no explote tu validación)
            PerfilUsuarios temporal = new PerfilUsuarios
            {
                Id = 0,
                Correo = "pepix.barber@mail.com",
                Contraseña = "lopezito20254",
                Estado = "Activo",
                IdRol = 2
            };
            PerfilUsuarios guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.Contraseña = "Colombia2090.";
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Colombia2090.", respuesta.Contraseña);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarPerfilUsuarios() //Debe Retornar True
        {
            PerfilUsuariosNegocio negocio = new PerfilUsuariosNegocio();

            // Creamos basura temporal VÁLIDA
            PerfilUsuarios basura = new PerfilUsuarios
            {
                Id = 0,
                Correo = "pepix.barber@mail.com",
                Contraseña = "lopezito20254",
                Estado = "Activo",
                IdRol = 2
            };
            PerfilUsuarios guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}