import { apiRequest } from "./script";

async function getReportProductSold() {
    try {
        const report = await apiRequest("http://localhost:5237/api/Relatorios/product-sold");
        console.log(report); // Verifique os dados recebidos

        // Ordena os dados do menor para o maior ID
        report.sort((a, b) => a.id - b.id);

        const tbody = document.querySelector("#tabelaReporteProdutosVendidos tbody");
        tbody.innerHTML = "";

        report.forEach(report => {
            const tr = document.createElement("tr");
            tr.innerHTML = `
                <td>${report.id}</td>
                <td>${new Date(report.reportDate).toLocaleDateString()}</td>
                <td>${report.product ? report.product.name : '-'}</td>
                <td>${report.quantity}</td>
                <td>${report.product ? report.product.price : '-'}</td>
            `;
            tbody.appendChild(tr);
        });
    } catch (error) {
        console.error('Erro ao carregar os dados:', error);
    }
}