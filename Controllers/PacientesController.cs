using Microsoft.AspNetCore.Mvc;
using SistemaAgendamentoMedico.Models;
using SistemaAgendamentoMedico.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAgendamentoMedico.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly PacienteService _pacienteService;

        public PacientesController(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Paciente>>> Get() => await _pacienteService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> GetById(string id) => await _pacienteService.GetByIdAsync(id);

        [HttpPost]
        public async Task<IActionResult> Create(Paciente paciente)
        {
            await _pacienteService.CreateAsync(paciente);
            return CreatedAtAction(nameof(GetById), new { id = paciente.Id }, paciente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Paciente updatedPaciente)
        {
            await _pacienteService.UpdateAsync(id, updatedPaciente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _pacienteService.DeleteAsync(id);
            return NoContent();
        }
    }
}
