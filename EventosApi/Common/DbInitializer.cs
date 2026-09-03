using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace EventosApi.Common
{
    // Como no usamos Entity Framework (no hay Database.Migrate() automatico como en la Guia #6),
    // este metodo crea la base de datos y el esquema a mano la primera vez que arranca la API.
    // docker-compose ya espera a que SQL Server este "healthy" antes de levantar la API
    // (depends_on: condition: service_healthy), por lo que aqui no hace falta reintentar la conexion.
    public static class DbInitializer
    {
        public static async Task InitializeAsync(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            var databaseName = builder.InitialCatalog;
            builder.InitialCatalog = "master";

            await using (var masterConnection = new SqlConnection(builder.ConnectionString))
            {
                await masterConnection.OpenAsync();

                var checkDbCmd = new SqlCommand("SELECT database_id FROM sys.databases WHERE name = @Name", masterConnection);
                checkDbCmd.Parameters.AddWithValue("@Name", databaseName);
                var dbExists = await checkDbCmd.ExecuteScalarAsync() != null;

                if (!dbExists)
                {
                    var createCmd = new SqlCommand($"CREATE DATABASE [{databaseName}]", masterConnection);
                    await createCmd.ExecuteNonQueryAsync();
                    Console.WriteLine($"Base de datos '{databaseName}' creada.");
                }
            }

            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var checkTableCmd = new SqlCommand("SELECT OBJECT_ID('dbo.Eventos', 'U')", connection);
            var tableExistsResult = await checkTableCmd.ExecuteScalarAsync();
            var tableExists = tableExistsResult != null && tableExistsResult != DBNull.Value;

            if (tableExists)
            {
                Console.WriteLine("Las tablas ya existen. Omitiendo creacion del esquema.");
                return;
            }

            var scriptPath = Path.Combine(AppContext.BaseDirectory, "Scripts", "schema.sql");
            var script = await File.ReadAllTextAsync(scriptPath);
            var batches = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            foreach (var batch in batches)
            {
                var trimmed = batch.Trim();
                if (string.IsNullOrWhiteSpace(trimmed))
                {
                    continue;
                }

                await using var cmd = new SqlCommand(trimmed, connection);
                await cmd.ExecuteNonQueryAsync();
            }

            Console.WriteLine("Esquema y datos iniciales creados correctamente.");
        }
    }
}
