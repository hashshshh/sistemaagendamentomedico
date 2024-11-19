using MongoDB.Driver;
using SistemaAgendamentoMedico.Models;
using SistemaAgendamentoMedico.Data;

namespace SistemaAgendamentoMedico.Services
{
    public class PacienteService
    {
        private readonly IMongoCollection<Paciente> _pacientes;

        public PacienteService(MongoDBContext context)
        {
            _pacientes = context.Pacientes;
        }

        public async Task<List<Paciente>> GetAllAsync() => await _pacientes.Find(_ => true).ToListAsync();

        public async Task<Paciente> GetByIdAsync(string id) => await _pacientes.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Paciente paciente) => await _pacientes.InsertOneAsync(paciente);

        public async Task UpdateAsync(string id, Paciente updatedPaciente) =>
            await _pacientes.ReplaceOneAsync(p => p.Id == id, updatedPaciente);

        public async Task DeleteAsync(string id) => await _pacientes.DeleteOneAsync(p => p.Id == id);
    }
}
