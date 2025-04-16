const apiUrl = "http://localhost:5237/api/Kiosk";

document.addEventListener("DOMContentLoaded", () => {
    document.getElementById("kioskForm").addEventListener("submit", salvarKiosk);
    listarKiosks();
});

async function apiRequest(url, method = 'GET', body = null) {
    const options = {
        method,
        headers: { 'Content-Type': 'application/json' },
    };

    if (body) options.body = JSON.stringify(body);

    const res = await fetch(url, options);
    if (!res.ok) throw new Error(await res.text());
    return res.status === 204 ? null : res.json();
}

async function listarKiosks() {
    try {
        const data = await apiRequest(apiUrl);
        const tbody = document.querySelector("#tabelaKiosks tbody");
        tbody.innerHTML = "";

        data.forEach(kiosk => {
            const tr = document.createElement("tr");
            tr.innerHTML = `
                <td>${kiosk.id}</td>
                <td>${kiosk.capacity}</td>
                <td>R$ ${kiosk.price}</td>
                <td>${kiosk.status}</td>
                <td>
                    <button onclick="editarKiosk(${kiosk.id})">Editar</button>
                    <button onclick="excluirKiosk(${kiosk.id})">Excluir</button>
                </td>
            `;
            tbody.appendChild(tr);
        });
    } catch (error) {
        console.error("Erro ao listar quiosques:", error);
    }
}

async function salvarKiosk(event) {
    event.preventDefault();


    const idInput = document.getElementById("id");
    const idValue = idInput.value.trim();
    const capacity = parseInt(document.getElementById("capacity").value);
    const price = parseFloat(document.getElementById("price").value);
    const status = document.getElementById("status").value;

    if (!capacity || !price || !status) {
        alert("Preencha todos os campos.");
        return;
    }

    const kiosk = {
        capacity,
        price,
        status
    };

    try {
        if (idValue) {
           
            kiosk.id = parseInt(idValue);
            await apiRequest(`http://localhost:5237/api/Kiosk/${kiosk.id}`, "PUT", kiosk);
        } else {
            
            await apiRequest("http://localhost:5237/api/Kiosk", "POST", kiosk);
        }

      
        document.getElementById("kioskForm").reset();
        listarKiosks();
    } catch (error) {
        console.error("Erro ao salvar quiosque:", error);
    }
}


async function editarKiosk(id) {
    try {
        const kiosk = await apiRequest(`${apiUrl}/${id}`);
        document.getElementById("id").value = kiosk.id;
        document.getElementById("capacity").value = kiosk.capacity;
        document.getElementById("price").value = kiosk.price;
        document.getElementById("status").value = kiosk.status;
    } catch (error) {
        console.error("Erro ao carregar quiosque:", error);
    }
}

async function excluirKiosk(id) {
    if (!confirm("Tem certeza que deseja excluir este quiosque?")) return;
    try {
        await apiRequest(`${apiUrl}/${id}`, "DELETE");
        listarKiosks();
    } catch (error) {
        console.error("Erro ao excluir quiosque:", error);
    }
}
