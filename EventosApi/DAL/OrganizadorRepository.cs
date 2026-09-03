using EventosApi.DAL.Interfaces;
using EventosApi.Models;

namespace EventosApi.DAL
{
    public class OrganizadorRepository(IDatabaseRepository databaseRepository) : IOrganizadorRepository
    {
        private static class Queries
        {
            public const string GetAll = "SELECT IdOrganizador, Nombre, Cargo, IdEvento FROM Organizadores ORDER BY IdOrganizador";
            public const string GetById = "SELECT IdOrganizador, Nombre, Cargo, IdEvento FROM Organizadores WHERE IdOrganizador = @Id";
            public const string Insert = "INSERT INTO Organizadores (Nombre, Cargo, IdEvento) VALUES (@Nombre, @Cargo, @IdEvento); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            public const string Update = "UPDATE Organizadores SET Nombre = @Nombre, Cargo = @Cargo, IdEvento = @IdEvento WHERE IdOrganizador = @IdOrganizador";
            public const string Delete = "DELETE FROM Organizadores WHERE IdOrganizador = @IdOrganizador";
        }

        public async Task<List<Organizador>> GetOrganizadoresAsync()
        {
            return [.. (await databaseRepository.QueryAsync<Organizador>(Queries.GetAll))];
        }

        public async Task<Organizador?> GetOrganizadorByIdAsync(int id)
        {
            return await databaseRepository.QueryFirstOrDefaultAsync<Organizador>(Queries.GetById, new { Id = id });
        }

        public async Task<int> InsertOrganizadorAsync(Organizador organizador)
        {
            return await databaseRepository.ExecuteScalarAsync<int>(Queries.Insert, new { organizador.Nombre, organizador.Cargo, organizador.IdEvento });
        }

        public async Task<bool> UpdateOrganizadorAsync(Organizador organizador)
        {
            var rowsAffected = await databaseRepository.ExecuteAsync(Queries.Update, new { organizador.IdOrganizador, organizador.Nombre, organizador.Cargo, organizador.IdEvento });
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteOrganizadorAsync(int id)
        {
            return await databaseRepository.ExecuteAsync(Queries.Delete, new { IdOrganizador = id }) > 0;
        }
    }
}
