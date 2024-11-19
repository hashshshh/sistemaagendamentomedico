using MongoDB.Driver;
using SistemaAgendamentoMedico.Models;
using SistemaAgendamentoMedico.Data;

namespace SistemaAgendamentoMedico.Services
{
    public class AgendamentoService
    {
        private readonly IMongoCollection<Agendamento> _agendamentos;

        public AgendamentoService(MongoDBContext context)
        {
            _agendamentos = context.Agendamentos;
        }

        public async Task<List<Agendamento>> GetAllAsync() => await _agendamentos.Find(_ => true).ToListAsync();

        public async Task<Agendamento> GetByIdAsync(string id) => await _agendamentos.Find(a => a.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Agendamento agendamento) => await _agendamentos.InsertOneAsync(agendamento);

        public async Task UpdateAsync(string id, Agendamento updatedAgendamento) =>
            await _agendamentos.ReplaceOneAsync(a => a.Id == id, updatedAgendamento);

        public async Task DeleteAsync(string id) => await _agendamentos.DeleteOneAsync(a => a.Id == id);
    }
}
