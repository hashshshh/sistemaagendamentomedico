using Microsoft.AspNetCore.Mvc;
using SistemaAgendamentoMedico.Models;
using SistemaAgendamentoMedico.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAgendamentoMedico.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentosController : ControllerBase
    {
        private readonly AgendamentoService _agendamentoService;

        public AgendamentosController(AgendamentoService agendamentoService)
        {
            _agendamentoService = agendamentoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Agendamento>>> Get() => await _agendamentoService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Agendamento>> GetById(string id) => await _agendamentoService.GetByIdAsync(id);

        [HttpPost]
        public async Task<IActionResult> Create(Agendamento agendamento)
        {
            await _agendamentoService.CreateAsync(agendamento);
            return CreatedAtAction(nameof(GetById), new { id = agendamento.Id }, agendamento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Agendamento updatedAgendamento)
        {
            await _agendamentoService.UpdateAsync(id, updatedAgendamento);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _agendamentoService.DeleteAsync(id);
            return NoContent();
        }
    }
}
