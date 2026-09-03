using EventosApi.DAL.Interfaces;
using EventosApi.Models;

namespace EventosApi.DAL
{
    public class EventoRepository(IDatabaseRepository databaseRepository) : IEventoRepository
    {
        private static class Queries
        {
            public const string GetAll = "SELECT IdEvento, Nombre, Fecha, Lugar FROM Eventos ORDER BY IdEvento";
            public const string GetById = "SELECT IdEvento, Nombre, Fecha, Lugar FROM Eventos WHERE IdEvento = @Id";
            public const string Insert = "INSERT INTO Eventos (Nombre, Fecha, Lugar) VALUES (@Nombre, @Fecha, @Lugar); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            public const string Update = "UPDATE Eventos SET Nombre = @Nombre, Fecha = @Fecha, Lugar = @Lugar WHERE IdEvento = @IdEvento";
            public const string Delete = "DELETE FROM Eventos WHERE IdEvento = @IdEvento";
        }

        public async Task<List<Evento>> GetEventosAsync()
        {
            return [.. (await databaseRepository.QueryAsync<Evento>(Queries.GetAll))];
        }

        public async Task<Evento?> GetEventoByIdAsync(int id)
        {
            return await databaseRepository.QueryFirstOrDefaultAsync<Evento>(Queries.GetById, new { Id = id });
        }

        public async Task<int> InsertEventoAsync(Evento evento)
        {
            return await databaseRepository.ExecuteScalarAsync<int>(Queries.Insert, new { evento.Nombre, evento.Fecha, evento.Lugar });
        }

        public async Task<bool> UpdateEventoAsync(Evento evento)
        {
            var rowsAffected = await databaseRepository.ExecuteAsync(Queries.Update, new { evento.IdEvento, evento.Nombre, evento.Fecha, evento.Lugar });
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteEventoAsync(int id)
        {
            return await databaseRepository.ExecuteAsync(Queries.Delete, new { IdEvento = id }) > 0;
        }
    }
}
