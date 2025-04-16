let idEmEdicao = null; // Quando estiver editando uma reserva

const form = document.getElementById('create-form');
const tabela = document.getElementById('reservation-table-body');

// Função para fazer requisição genérica à API
async function apiRequest(url, method, data = {}) {
    const response = await fetch(url, {
        method: method,
        headers: { 'Content-Type': 'application/json' },
        body: method !== 'GET' ? JSON.stringify(data) : null
    });

    if (!response.ok) {
        throw new Error('Erro na requisição');
    }

    return await response.json();
}

// Listar todas as reservas
async function getReservations() {
    try {
        const reservas = await apiRequest('http://localhost:5237/reservation', 'GET');
        renderTable(reservas);
    } catch (error) {
        console.error('Erro ao obter reservas:', error);
    }
}

// Criar OU atualizar reserva
form.addEventListener('submit', async (e) => {
    e.preventDefault();

    const reservation = {
        startDate: document.getElementById('start_date_create').value,
        endDate: document.getElementById('end_date_create').value,
        visitorId: parseInt(document.getElementById('visitor_id_create').value),
        kioskId: document.getElementById('kiosk_id_create').value || null
    };

    try {
        if (idEmEdicao) {
            // Atualizar
            await apiRequest(`http://localhost:5237/reservation/${idEmEdicao}`, 'PUT', reservation);
            alert('Reserva atualizada com sucesso!');
        } else {
            // Criar
            await apiRequest('http://localhost:5237/reservation', 'POST', reservation);
            alert('Reserva criada com sucesso!');
        }

        idEmEdicao = null;
        form.querySelector('button').textContent = "Criar Reserva";
        form.reset();
        await getReservations();
    } catch (error) {
        console.error('Erro ao salvar reserva:', error);
    }
});

// Deletar reserva
async function deleteReservation(id) {
    if (confirm('Tem certeza que deseja excluir esta reserva?')) {
        try {
            await apiRequest(`http://localhost:5237/reservation/${id}`, 'DELETE');
            alert('Reserva excluída com sucesso!');
            await getReservations();
        } catch (error) {
            console.error('Erro ao deletar reserva:', error);
        }
    }
}

// Preencher formulário para edição
async function editReservation(id) {
    try {
        const reserva = await apiRequest(`http://localhost:5237/reservation/${id}`, 'GET');

        document.getElementById('start_date_create').value = reserva.startDate.slice(0, 16);
        document.getElementById('end_date_create').value = reserva.endDate.slice(0, 16);
        document.getElementById('visitor_id_create').value = reserva.visitorId;
        document.getElementById('kiosk_id_create').value = reserva.kioskId ?? '';

        idEmEdicao = id;
        form.querySelector('button').textContent = "Atualizar Reserva";
    } catch (error) {
        console.error('Erro ao carregar reserva:', error);
    }
}

// Renderizar tabela
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
                <button onclick="editReservation(${reserva.id})">Editar</button>
                <button onclick="deleteReservation(${reserva.id})">Excluir</button>
            </td>
        `;
        tabela.appendChild(tr);
    });
}

// Formatar data para exibição (ex: 2025-04-15 10:00)
function formatDate(dateStr) {
    return new Date(dateStr).toLocaleString('pt-BR', {
        dateStyle: 'short',
        timeStyle: 'short'
    });
}

// Carregar reservas ao iniciar
getReservations();

// Expor funções no escopo global (necessário para usar onclick no HTML)
window.editReservation = editReservation;
window.deleteReservation = deleteReservation;
