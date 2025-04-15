import { apiRequest } from "./script.js";

let idEmEdicao = null; // Quando estiver editando uma reserva
const form = document.getElementById('create-form');
const tabela = document.getElementById('reservation-table-body');

// Função para listar as reservas
async function listarReservas() {
    try {
        const reservas = await apiRequest('http://localhost:5237/reservation', 'GET');
        renderTable(reservas);
    } catch (error) {
        console.error('Erro ao obter reservas:', error);
    }
}


async function criarReserva(reservation) {
    const body = {
        startDate: new Date(reservation.startDate).toISOString(),
        endDate: new Date(reservation.endDate).toISOString(),
        visitorId: parseInt(reservation.visitorId),
        kioskId: reservation.kioskId ? parseInt(reservation.kioskId) : null
    };

    console.log("Enviando dados:", body);  // Adicione esta linha para depurar

    await apiRequest('http://localhost:5237/reservation', 'POST', body);
}


// Função para atualizar uma reserva existente
async function atualizarReserva(id, reservation) {
    // Garantir que as datas sejam no formato ISO
    reservation.startDate = new Date(reservation.startDate).toISOString();
    reservation.endDate = new Date(reservation.endDate).toISOString();

    try {
        const response = await apiRequest(`http://localhost:5237/reservation/${id}`, 'PUT', reservation);
        console.log('Resposta da API:', response);
        alert('Reserva atualizada com sucesso!');
    } catch (error) {
        console.error('Erro ao atualizar reserva:', error);
        if (error.response) {
            console.error('Detalhes do erro:', error.response.data);
        }
    }
}


// Função para criar ou atualizar a reserva (separando as operações de criação e atualização)
async function criarOuAtualizarReserva(reservation) {
    if (idEmEdicao) {
        // Atualizar
        await atualizarReserva(idEmEdicao, reservation);
    } else {
        // Criar
        await criarReserva(reservation);
    }

    // Resetando a edição após criação ou atualização
    idEmEdicao = null;
    form.querySelector('button').textContent = "Criar Reserva";
    form.reset();
    await listarReservas();
}

// Função para deletar uma reserva
async function deletarReserva(id) {
    if (confirm('Tem certeza que deseja excluir esta reserva?')) {
        try {
            await apiRequest(`http://localhost:5237/reservation/${id}`, 'DELETE');
            alert('Reserva excluída com sucesso!');
            await listarReservas();
        } catch (error) {
            console.error('Erro ao deletar reserva:', error);
        }
    }
}

// Função para preencher o formulário de edição
async function preencherFormularioParaEdicao(id) {
    try {
        const reserva = await apiRequest(`http://localhost:5237/reservation/${id}`, 'GET');

        // Preenchendo o formulário com os dados da reserva
        document.getElementById('start_date_create').value = reserva.startDate.slice(0, 16); // Formato correto para datetime-local
        document.getElementById('end_date_create').value = reserva.endDate.slice(0, 16); // Formato correto para datetime-local
        document.getElementById('visitor_id_create').value = reserva.visitorId;
        document.getElementById('kiosk_id_create').value = reserva.kioskId ?? '';

        // Configurando a edição
        idEmEdicao = id;
        form.querySelector('button').textContent = "Atualizar Reserva";
    } catch (error) {
        console.error('Erro ao carregar reserva:', error);
    }
}

// Função para renderizar a tabela
function renderTable(reservas) {
    tabela.innerHTML = '';

    reservas.forEach(reserva => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
            <td>${reserva.id}</td>
            <td>${formatDate(reserva.startDate)}</td>
            <td>${formatDate(reserva.endDate)}</td>
            <td>${reserva.visitorId}</td>
            <td>${reserva.kioskId ?? '-'}</td>
            <td>
                <button onclick="preencherFormularioParaEdicao(${reserva.id})">Editar</button>
                <button onclick="deletarReserva(${reserva.id})">Excluir</button>
            </td>
        `;
        tabela.appendChild(tr);
    });
}

// Função para formatar a data
function formatDate(dateStr) {
    return new Date(dateStr).toLocaleString('pt-BR', {
        dateStyle: 'short',
        timeStyle: 'short'
    });
}

// Carregar reservas ao iniciar
listarReservas();

// Expor funções no escopo global (necessário para usar onclick no HTML)
window.preencherFormularioParaEdicao = preencherFormularioParaEdicao;
window.deletarReserva = deletarReserva;

// Adicionar listener no formulário
form.addEventListener('submit', (e) => {
    e.preventDefault();

    const reservation = {
        startDate: document.getElementById('start_date_create').value,
        endDate: document.getElementById('end_date_create').value,
        visitorId: parseInt(document.getElementById('visitor_id_create').value),
        kioskId: document.getElementById('kiosk_id_create').value || null
    };

    // Certificando-se de que as datas sejam enviadas no formato correto
    criarOuAtualizarReserva(reservation);
});
