namespace LibreriaBarberia.Nucleo
{
    public class Configuraciones
    {
        public static string obtener(string clave)
        {
            return "server=localhost\\SQLEXPRESS;database=db_barberia;Integrated Security=True;TrustServerCertificate=true;";
        }
    }
}

// Nombre del PC mío: DESKTOP-BJQKKO0
// Para usar en otro PC: localhost