namespace SistemaAgendamentoMedico.Models
{
    public class Agendamento
    {
        public required string Id { get; set; }
        public required string MedicoId { get; set; }
        public required string PacienteId { get; set; }
        public DateTime DataHora { get; set; }
    }
}
