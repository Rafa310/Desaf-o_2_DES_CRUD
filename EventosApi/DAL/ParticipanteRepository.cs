using EventosApi.DAL.Interfaces;
using EventosApi.Models;

namespace EventosApi.DAL
{
    public class ParticipanteRepository(IDatabaseRepository databaseRepository) : IParticipanteRepository
    {
        private static class Queries
        {
            public const string GetAll = "SELECT IdParticipante, Nombre, Email, IdEvento FROM Participantes ORDER BY IdParticipante";
            public const string GetById = "SELECT IdParticipante, Nombre, Email, IdEvento FROM Participantes WHERE IdParticipante = @Id";
            public const string Insert = "INSERT INTO Participantes (Nombre, Email, IdEvento) VALUES (@Nombre, @Email, @IdEvento); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            public const string Update = "UPDATE Participantes SET Nombre = @Nombre, Email = @Email, IdEvento = @IdEvento WHERE IdParticipante = @IdParticipante";
            public const string Delete = "DELETE FROM Participantes WHERE IdParticipante = @IdParticipante";
        }

        public async Task<List<Participante>> GetParticipantesAsync()
        {
            return [.. (await databaseRepository.QueryAsync<Participante>(Queries.GetAll))];
        }

        public async Task<Participante?> GetParticipanteByIdAsync(int id)
        {
            return await databaseRepository.QueryFirstOrDefaultAsync<Participante>(Queries.GetById, new { Id = id });
        }

        public async Task<int> InsertParticipanteAsync(Participante participante)
        {
            return await databaseRepository.ExecuteScalarAsync<int>(Queries.Insert, new { participante.Nombre, participante.Email, participante.IdEvento });
        }

        public async Task<bool> UpdateParticipanteAsync(Participante participante)
        {
            var rowsAffected = await databaseRepository.ExecuteAsync(Queries.Update, new { participante.IdParticipante, participante.Nombre, participante.Email, participante.IdEvento });
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteParticipanteAsync(int id)
        {
            return await databaseRepository.ExecuteAsync(Queries.Delete, new { IdParticipante = id }) > 0;
        }
    }
}
