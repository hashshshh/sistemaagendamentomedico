const apiBaseUrl = 'http://localhost:5200/api';

// Exibir mensagens de erro
function exibirErro(mensagem) {
    console.error(mensagem);
    alert(`Erro: ${mensagem}`);
}

// Funções para Médicos
async function fetchMedicos() {
    try {
        const response = await fetch(`${apiBaseUrl}/medicos`);
        if (!response.ok) throw new Error("Erro ao buscar médicos");
        const medicos = await response.json();

        const medicoList = document.getElementById('medicoList');
        medicoList.innerHTML = '';
        medicos.forEach(medico => {
            const li = document.createElement('li');
            li.textContent = `${medico.nome} - ${medico.especialidade}`;
            medicoList.appendChild(li);
        });

        carregarMedicosDropdown(medicos); // Atualiza o dropdown de médicos
    } catch (error) {
        exibirErro("Não foi possível carregar os médicos.");
    }
}

async function cadastrarMedico(e) {
    e.preventDefault();
    const nome = document.getElementById('medicoNome').value.trim();
    const especialidade = document.getElementById('medicoEspecialidade').value.trim();

    if (!nome || !especialidade) {
        alert('Por favor, preencha todos os campos.');
        return;
    }

    try {
        const response = await fetch(`${apiBaseUrl}/medicos`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ nome, especialidade })
        });
        if (!response.ok) throw new Error("Erro ao cadastrar médico");
        fetchMedicos(); // Atualiza a lista de médicos
    } catch (error) {
        exibirErro(error.message);
    }
}

// Funções para Pacientes
async function fetchPacientes() {
    try {
        const response = await fetch(`${apiBaseUrl}/pacientes`);
        if (!response.ok) throw new Error("Erro ao buscar pacientes");
        const pacientes = await response.json();

        const pacienteList = document.getElementById('pacienteList');
        pacienteList.innerHTML = '';
        pacientes.forEach(paciente => {
            const li = document.createElement('li');
            li.textContent = paciente.nome;
            pacienteList.appendChild(li);
        });

        carregarPacientesDropdown(pacientes); // Atualiza o dropdown de pacientes
    } catch (error) {
        exibirErro("Não foi possível carregar os pacientes.");
    }
}

async function cadastrarPaciente(e) {
    e.preventDefault();
    const nome = document.getElementById('pacienteNome').value.trim();
    const contato = document.getElementById('pacienteContato').value.trim();

    if (!nome || !contato) {
        alert('Por favor, preencha todos os campos.');
        return;
    }

    try {
        const response = await fetch(`${apiBaseUrl}/pacientes`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ nome, contato })
        });
        if (!response.ok) throw new Error("Erro ao cadastrar paciente");
        fetchPacientes(); // Atualiza a lista de pacientes
    } catch (error) {
        exibirErro(error.message);
    }
}

// Funções para Agendamentos
async function fetchAgendamentos() {
    try {
        const response = await fetch(`${apiBaseUrl}/agendamentos`);
        if (!response.ok) throw new Error("Erro ao buscar agendamentos");
        const agendamentos = await response.json();

        const agendamentoList = document.getElementById('agendamentoList');
        agendamentoList.innerHTML = '';
        agendamentos.forEach(agendamento => {
            const li = document.createElement('li');
            li.textContent = `Médico: ${agendamento.medicoId}, Paciente: ${agendamento.pacienteId}, Data/Hora: ${agendamento.dataHora}`;
            agendamentoList.appendChild(li);
        });
    } catch (error) {
        exibirErro("Não foi possível carregar os agendamentos.");
    }
}

async function agendarConsulta(e) {
    e.preventDefault();
    const medicoId = document.getElementById('agendamentoMedicoId').value;
    const pacienteId = document.getElementById('agendamentoPacienteId').value;
    const dataHora = document.getElementById('agendamentoDataHora').value;

    if (!medicoId || !pacienteId || !dataHora) {
        alert("Por favor, preencha todos os campos.");
        return;
    }

    try {
        const response = await fetch(`${apiBaseUrl}/agendamentos`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ medicoId, pacienteId, dataHora })
        });
        if (!response.ok) throw new Error("Erro ao agendar consulta");
        fetchAgendamentos(); // Atualiza a lista de agendamentos
    } catch (error) {
        exibirErro(error.message);
    }
}

// Atualizar dropdowns
function carregarMedicosDropdown(medicos) {
    const medicoDropdown = document.getElementById('agendamentoMedicoId');
    medicoDropdown.innerHTML = '<option value="" disabled selected>Selecione um médico</option>';
    medicos.forEach(medico => {
        const option = document.createElement('option');
        option.value = medico.id; // O ID do médico é usado no value
        option.textContent = `${medico.nome} (${medico.especialidade})`;
        medicoDropdown.appendChild(option);
    });
}

function carregarPacientesDropdown(pacientes) {
    const pacienteDropdown = document.getElementById('agendamentoPacienteId');
    pacienteDropdown.innerHTML = '<option value="" disabled selected>Selecione um paciente</option>';
    pacientes.forEach(paciente => {
        const option = document.createElement('option');
        option.value = paciente.id; // O ID do paciente é usado no value
        option.textContent = paciente.nome;
        pacienteDropdown.appendChild(option);
    });
}

// Inicializar os dropdowns e carregar dados iniciais
document.getElementById('medicoForm').addEventListener('submit', cadastrarMedico);
document.getElementById('pacienteForm').addEventListener('submit', cadastrarPaciente);
document.getElementById('agendamentoForm').addEventListener('submit', agendarConsulta);

fetchMedicos();
fetchPacientes();
fetchAgendamentos();
