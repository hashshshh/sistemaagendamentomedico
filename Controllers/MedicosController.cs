using Microsoft.AspNetCore.Mvc;
using SistemaAgendamentoMedico.Models;
using SistemaAgendamentoMedico.Services;

namespace SistemaAgendamentoMedico.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicosController : ControllerBase
    {
        private readonly MedicoService _medicoService;

        public MedicosController(MedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Medico>>> Get()
        {
            return await _medicoService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Medico>> GetById(string id)
        {
            var medico = await _medicoService.GetByIdAsync(id);
            if (medico == null)
            {
                return NotFound();
            }
            return medico;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Medico medico)
        {
            if (medico == null)
            {
                return BadRequest("Os dados do médico são inválidos.");
            }

            try
            {
                await _medicoService.CreateAsync(medico);
                return CreatedAtAction(nameof(GetById), new { id = medico.Id }, medico);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cadastrar médico: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Medico updatedMedico)
        {
            var existingMedico = await _medicoService.GetByIdAsync(id);
            if (existingMedico == null)
            {
                return NotFound();
            }

            updatedMedico.Id = id;
            await _medicoService.UpdateAsync(id, updatedMedico);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingMedico = await _medicoService.GetByIdAsync(id);
            if (existingMedico == null)
            {
                return NotFound();
            }

            await _medicoService.DeleteAsync(id);
            return NoContent();
        }
    }
}
