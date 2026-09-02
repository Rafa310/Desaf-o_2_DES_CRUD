using MySql.Data.MySqlClient;
using System.IO;

namespace WebApplication1
{
    public static class DatabaseInitializer
    {
        public static void Initialize(string connectionString)
        {
            try
            {
                // Leer el script SQL desde el archivo
                string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts", "bd_des104_d2.sql");
                string sqlScript = File.ReadAllText(scriptPath);

                // Ejecutar el script
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    var commands = sqlScript.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var command in commands)
                    {
                        if (!string.IsNullOrWhiteSpace(command.Trim()))
                        {
                            using (var cmd = new MySqlCommand(command.Trim(), connection))
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    Console.WriteLine("✅ Base de datos creada exitosamente desde el script SQL.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al inicializar la base de datos: {ex.Message}");
                throw;
            }
        }
    }
}