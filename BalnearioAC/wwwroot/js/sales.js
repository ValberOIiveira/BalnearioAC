import { apiRequest } from './script.js';

async function PostSale(event){
    event.preventDefault();
    try {
        const EmployeeId = document.getElementById('EmployeeId').value;
        const SaleDate = document.getElementById('SaleDate').value;
        const TotalValue = document.getElementById('TotalValue').value;
        if (!EmployeeId || !SaleDate || !TotalValue) {
            alert('Preencha todos os campos');
            return;
        }
        else{
            const response = await apiRequest('http://localhost:5237/sales', 'POST', { SaleId, ProductId, Quantity });
        }
    } catch (error) {
        console.log(error)
    }
}

async function GetSales(event) {
    event.preventDefault();
    try{
        const Sales = await apiRequest('http://localhost:5237/sales', 'GET');
        console.log(Sales);
        if (Sales) {
            for (let i = 0; i < Sales.length; i++) {
                const SaleId = Sales[i].id;
                const EmployeeId = Sales[i].EmployeeId;
                const SaleDate = Sales[i].SaleDate;
                const TotalValue = Sales[i].TotalValue;
                const Sale = document.createElement('tr');
                Sale.innerHTML = `
                    <td>${SaleId}</td>
                    <td>${EmployeeId}</td>
                    <td>${SaleDate}</td>
                    <td>${TotalValue}</td>    
                `;
                document.getElementById('Sales').appendChild(Sale);
            }
        }
    }   
    catch(error){
        console.log(error);
    }

}