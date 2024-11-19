using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SistemaAgendamentoMedico.Models;

namespace SistemaAgendamentoMedico.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Medico> Medicos => _database.GetCollection<Medico>("ProfissionaisMedicos");
        public IMongoCollection<Paciente> Pacientes => _database.GetCollection<Paciente>("Pacientes");
        public IMongoCollection<Agendamento> Agendamentos => _database.GetCollection<Agendamento>("Agendamentos");
    }
}
