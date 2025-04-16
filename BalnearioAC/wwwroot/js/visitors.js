import { apiRequest } from "./script.js";

document.getElementById("visitorForm").addEventListener("submit", salvarVisitante);

listarVisitantes();

async function listarVisitantes() {
    try {
        const data = await apiRequest('http://localhost:5237/Visitor');

        data.sort((a, b) => a.id - b.id);

        const tbody = document.querySelector("#visitorTableBody");
        tbody.innerHTML = "";

        data.forEach(visitor => {
            const tr = document.createElement("tr");
            tr.innerHTML = `
                <td>${visitor.id}</td>
                <td>${visitor.name}</td>
                <td>${visitor.cpf}</td>
                <td>${new Date(visitor.birthDate).toLocaleDateString()}</td>
                <td>${visitor.user ? visitor.user.name : 'Usuário não encontrado'}</td> <!-- Verifica se o user existe -->
                <td><button class="editBtn">Editar</button></td>
            `;

            const btn = tr.querySelector(".editBtn");
            btn.addEventListener("click", () => editarVisitante(visitor.id));

            tbody.appendChild(tr);
        });

    } catch (error) {
        console.error("Erro ao listar visitantes:", error);
    }
}

async function salvarVisitante(event) {
    event.preventDefault();

    try {
        const name = document.getElementById('name').value;
        const cpf = document.getElementById('cpf').value;
        const birthDate = document.getElementById('birthDate').value;
        const idUser = parseInt(document.getElementById('idUser').value);
        const id = document.getElementById('id').value;

        // Verificando se todos os campos foram preenchidos
        if (!name || !cpf || !birthDate || !idUser) {
            alert('Preencha todos os campos');
            return;
        }


        // Criando o objeto payload com os dados do visitante
        const payload = { name, cpf, birthDate, idUser };

        // Adicionando log para verificação do payload
        console.log("Payload para salvar visitante:", payload); // 
        let response;
        if (id) {
            response = await apiRequest(`http://localhost:5237/Visitor/${id}`, 'PUT', {id, ...payload});
        } else {
            response = await apiRequest('http://localhost:5237/Visitor', 'POST', payload);
        }

        if (response && response.id) {
            console.log("Resposta da requisição:", response);
            document.getElementById("visitorForm").reset();
            listarVisitantes();
        } else {
            console.error("Erro na requisição:", response); // Adicione aqui para ver a resposta completa
            alert("Erro ao salvar visitante. Verifique os dados.");
        }
        



        // Reseta o formulário e atualiza a lista de visitantes
        document.getElementById("visitorForm").reset();
        listarVisitantes();
    } catch (error) {
        console.error('Erro ao salvar visitante:', error);
    }
}

async function editarVisitante(id) {
    try {
        const visitor = await apiRequest(`http://localhost:5237/Visitor/${id}`, 'GET');

        document.getElementById('id').value = visitor.id;
        document.getElementById('name').value = visitor.name;
        document.getElementById('cpf').value = visitor.cpf;
        document.getElementById('birthDate').value = visitor.birthDate.slice(0, 10); // yyyy-MM-dd
        document.getElementById('idUser').value = visitor.idUser;
    } catch (error) {
        console.error("Erro ao carregar visitante para edição:", error);
    }
}
