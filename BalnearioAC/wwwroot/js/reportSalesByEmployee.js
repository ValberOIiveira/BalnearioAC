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

        const tbody = document.querySelector("#tabelaSalesByEmployee tbody");
        tbody.innerHTML = "";

        data.forEach(report => {
            const tr = document.createElement("tr");
            tr.innerHTML = `
                <td>${report.id}</td>
                <td>${report.report_date}</td>
                <td>${report.employee_name}</td>
                <td>${report.total_sales}</td>
                <td>${report.total_value}</td>
            `;
            tbody.appendChild(tr);
        });

    } catch (error) {
        console.error("Erro ao listar vendas por funcionário:", error);
    }
}
