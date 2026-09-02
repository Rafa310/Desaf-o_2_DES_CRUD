using WebApplication1;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// Agregar controladores, esto es de ustedes.
builder.Services.AddControllers();

var app = builder.Build();


try
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    bool dbExists = false;
    string databaseName = "bd_des104_d2";

    using (var connection = new MySqlConnection(connectionString))
    {
        connection.Open();
        var cmd = new MySqlCommand($"SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '{databaseName}'", connection);
        var result = cmd.ExecuteScalar();
        dbExists = result != null;
    }

    if (dbExists)
    {
        Console.WriteLine($"✅ Base de datos '{databaseName}' ya existe. Saltando creación.");
    }
    else
    {
        Console.WriteLine($"🔨 Creando base de datos '{databaseName}'...");

        string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts", "bd_des104_d2.sql");
        string sqlScript = File.ReadAllText(scriptPath);

        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            using (var cmd = new MySqlCommand(sqlScript, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        Console.WriteLine("✅ ¡Base de datos creada exitosamente!");
    }

    Console.WriteLine($"📊 Base de datos: {databaseName}");
    Console.WriteLine($"🔗 Conecta en: localhost:3307");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Error al verificar/crear la base de datos: {ex.Message}");
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();