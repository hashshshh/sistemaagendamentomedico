using MongoDB.Driver;
using SistemaAgendamentoMedico.Models;
using SistemaAgendamentoMedico.Data;

namespace SistemaAgendamentoMedico.Services
{
    public class MedicoService
    {
        private readonly IMongoCollection<Medico> _medicos;

        public MedicoService(MongoDBContext context)
        {
            _medicos = context.Medicos;
        }

        public async Task<List<Medico>> GetAllAsync() => await _medicos.Find(_ => true).ToListAsync();
        public async Task<Medico> GetByIdAsync(string id) => await _medicos.Find(m => m.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Medico medico) => await _medicos.InsertOneAsync(medico);
        public async Task UpdateAsync(string id, Medico updatedMedico) => await _medicos.ReplaceOneAsync(m => m.Id == id, updatedMedico);
        public async Task DeleteAsync(string id) => await _medicos.DeleteOneAsync(m => m.Id == id);
    }
}
