import { apiRequest } from "./script.js";


getReportReservation();

async function getReportReservation() {
    try {
        const data = await apiRequest('http://localhost:5237/api/Relatorios/reservations');

        console.log(data);  // Verifique os dados recebidos

        // Ordena os dados do menor para o maior ID
        data.sort((a, b) => a.id - b.id);

        const tbody = document.querySelector("#tabelaReporteReservas tbody");
        tbody.innerHTML = "";

        data.forEach(report => {
            const tr = document.createElement("tr");
            tr.innerHTML = `
                <td>${report.id}</td>
                <td>${new Date(report.reportDate).toLocaleDateString()}</td>
                <td>${report.kiosk ? report.kiosk.id : '-'}</td>
                <td>${report.user ? report.user.name : '-'}</td>
                <td>${new Date(report.startDate).toLocaleDateString()}</td>
                <td>${new Date(report.endDate).toLocaleDateString()}</td>
            `;
            tbody.appendChild(tr);
        });
    } catch (error) {
        console.error('Erro ao carregar os dados:', error);
    }
}

