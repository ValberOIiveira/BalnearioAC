import { apiRequest } from "./script.js";

const form = document.getElementById("saleForm");

if (form) {
    form.addEventListener("submit", salvarSale);
}

listarSale();

async function listarSale() {
    try {
        const data = await apiRequest('http://localhost:5237/Sales');

        const response = await fetch('http://localhost:5237/sales');
        const sales = await response.json();

        // Verifique a estrutura dos dados no console
        console.log(sales);

        data.sort((a, b) => a.id - b.id);

        const tbody = document.querySelector("#tabelaSales tbody");
        tbody.innerHTML = "";

        data.forEach(sale => {
            const tr = document.createElement("tr");
            tr.innerHTML = `
                <td>${sale.id}</td>
                <td>${new Date(sale.saleDate).toLocaleDateString('pt-BR')}</td>
                <td>R$ ${sale.totalValue}</td>
                <td>${sale.employeeName}</td>
                <td>${sale.itemSales.map(item => item.productName).join(", ")}</td> 
            `;



            tbody.appendChild(tr);
        });

    } catch (error) {
        console.error("Erro ao listar sales:", error);
    }
}

let produtos = [];
let carrinho = [];

async function renderizarProdutos() {
    const lista = document.getElementById("lista-produtos");
    lista.innerHTML = "";
    const termo = document.getElementById("buscar-produto-modal").value.toLowerCase();

    try {
        const response = await fetch(`http://localhost:5237/Product`);
        produtos = await response.json();

        const filtrados = produtos.filter(p => p.name.toLowerCase().includes(termo));

        filtrados.forEach(prod => {
            const item = document.createElement("li");
            item.className = "produto-item";
            item.innerHTML = `
                <div>
                    <strong>${prod.name}</strong>
                    <p class="font-2-s">Estoque: ${prod.quantity}</p>
                </div>
                <div>
                    <span>R$ ${prod.price.toFixed(2)}</span>
                    <button class="btn btn-outline" onclick='adicionarAoCarrinho(${JSON.stringify(prod).replace(/"/g, "&quot;")})'>+</button>
                </div>`;
            lista.appendChild(item);
        });

    } catch (error) {
        console.error("Erro ao carregar produtos:", error);
    }
}

function adicionarAoCarrinho(produto) {
    const existente = carrinho.find(p => p.id === produto.id);
    if (existente) {
        existente.qtd++;
    } else {
        carrinho.push({ ...produto, qtd: 1 });
    }
    renderizarCarrinho();
}

function alterarQuantidade(id, delta) {
    const item = carrinho.find(p => p.id === id);
    if (item) {
        item.qtd += delta;
        if (item.qtd <= 0) carrinho = carrinho.filter(p => p.id !== id);
        renderizarCarrinho();
    }
}

function removerDoCarrinho(id) {
    carrinho = carrinho.filter(p => p.id !== id);
    renderizarCarrinho();
}

function limparCarrinho() {
    carrinho = [];
    renderizarCarrinho();
}



function renderizarCarrinho() {
    const lista = document.getElementById("carrinho-produtos");
    lista.innerHTML = "";
    let total = 0;

    carrinho.forEach(item => {
        const li = document.createElement("li");
        li.className = "carrinho-item";
        total += item.price * item.qtd;
        li.innerHTML = `
        <div class="item-top">
            <strong>${item.name}</strong>
            <span>R$ ${item.price.toFixed(2)}</span>
        </div>
        <div class="item-controls">
            <button onclick="alterarQuantidade(${item.id}, -1)">-</button>
            <span>${item.qtd}</span>
            <button onclick="alterarQuantidade(${item.id}, 1)">+</button>
            <a onclick="removerDoCarrinho(${item.id})">Remover</a>
        </div>`;
        lista.appendChild(li);
    });

    document.getElementById("valor-total").textContent = `R$ ${total.toFixed(2)}`;
}

function filtrarProdutos() {
    console.log("🔍 Buscando produto...");
    renderizarProdutos();
}

async function finalizarVenda() {
    try {
        // Criar o objeto `venda` com todos os dados necessários
        const venda = {
            saleDate: new Date().toISOString(), // Data da venda (data atual)
            employeeId: 1, // ID do funcionário (ajustar conforme necessário)
            totalValue: carrinho.reduce((total, item) => total + (item.price * item.qtd), 0), // Total da venda
            itemSales: carrinho.map(item => ({
                productId: item.id, // ID do produto
                quantity: item.qtd, // Quantidade do produto
            }))
        };

        console.log("🔔 Dados da venda:", JSON.stringify(venda)); // Verifique os dados enviados

        // Enviar a requisição POST para a API
        const response = await apiRequest('http://localhost:5237/Sales')

        // Verificar se a resposta foi bem-sucedida (status 200-299)
        if (!response.ok) {
            const errorDetails = await response.json(); // Use .json() para obter os detalhes de erro
            console.error("Erro ao salvar venda:", errorDetails);
            throw new Error(errorDetails.message || 'Erro desconhecido');
        }

        // Caso a venda seja concluída com sucesso
        alert("Venda finalizada com sucesso!");
        limparCarrinho(); // Função para limpar o carrinho
        closeModal('modalVenda'); // Função para fechar o modal de venda

    } catch (error) {
        console.error("Erro ao finalizar venda:", error); // Log de erro, se houver
    }
}







document.addEventListener("DOMContentLoaded", () => renderizarProdutos());

window.adicionarAoCarrinho = adicionarAoCarrinho;
window.alterarQuantidade = alterarQuantidade;
window.removerDoCarrinho = removerDoCarrinho;
window.limparCarrinho = limparCarrinho;
window.finalizarVenda = finalizarVenda;
window.filtrarProdutos = filtrarProdutos;
