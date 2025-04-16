import { apiRequest } from "./script.js";

async function listarReportSalesByEmployee() {
    try {
        const data = await apiRequest('http://localhost:5237/relatorios/salesbyemployee');
        
        // Verifique os dados recebidos
        console.log("Dados recebidos:", data);

        if (!data || !Array.isArray(data)) {
            console.error('Dados inválidos recebidos:', data);
            return;
        }

        // Ordena os dados, se existirem
        data.sort((a, b) => a.id - b.id);

        const tbody = document.querySelector("#tabelaSalesByEmployee");
        tbody.innerHTML = "";

        data.forEach(report => {
            const tr = document.createElement("tr");
            tr.innerHTML = `
                <td>${report.id}</td>
                <td>${report.reportDate}</td>
                <td>${report.employeeName}</td>
                <td>${report.totalSales}</td>
                <td>${report.totalValue}</td>
            `;

            tbody.appendChild(tr);
        });

    } catch (error) {
        console.error("Erro ao listar vendas por funcionário:", error);
    }
}
listarReportSalesByEmployee();
