import { apiRequest } from "./script.js";


listarProdutos();

async function listarProdutos() {
    try {
        const data = await apiRequest('http://localhost:5237/api/Relatorios/reservations');

        console.log(data);
        // Ordena os produtos do maior para o menor ID
        data.sort((a, b) => a.id - b.id);

        const tbody = document.querySelector("#tabelaReporteReservas tbody");
        tbody.innerHTML = "";

        data.forEach(report => {
            const tr = document.createElement("tr");
            tr.innerHTML = `
                <td>${report.id}</td>
                <td>${report.reportDate}</td>
                <td>${report.report.visitor.name}</td>
                <td>${report.report.kiosk.id}</td>
                <td>${report.startDate}</td>
                <td>${report.endDate}</td>
            `;

            tbody.appendChild(tr);
        });

    } catch (error) {
        console.error("Erro ao listar produtos:", error);
    }
}