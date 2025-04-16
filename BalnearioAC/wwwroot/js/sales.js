import { apiRequest } from "./script.js";
// const produtos = [
//     { nome: 'Cerveja Pilsen', categoria: 'Bebidas', preco: 12.00 },
//     { nome: 'Refrigerante Cola', categoria: 'Bebidas', preco: 8.00 },
//     { nome: 'Água Mineral', categoria: 'Bebidas', preco: 5.00 },
//     { nome: 'Porção de Batata', categoria: 'Alimentos', preco: 25.00 },
//     { nome: 'Espetinho Carne', categoria: 'Alimentos', preco: 15.00 }
// ];

let carrinho = [];

async function renderizarProdutos() {
    const produtos = await apiRequest('http://localhost:5237/Product', 'GET');
    console.log(produtos);
    
    const lista = document.getElementById("lista-produtos");
    lista.innerHTML = "";
    const termo = document.getElementById("buscar-produto-modal").value.toLowerCase();
    produtos.filter(p => p.name.toLowerCase().includes(termo)).forEach(prod => {
        const item = document.createElement("li");
        item.className = "produto-item";
        item.innerHTML = `
    <div>
    <strong>${prod.name}</strong>
    <p class="font-2-s">${prod.quantity}</p>
    </div>
    <div>
    <span>R$ ${prod.price.toFixed(2)}</span>
    <button class="btn btn-outline" onclick='adicionarAoCarrinho(${JSON.stringify(prod).replace(/"/g, "&quot;")})'>+</button>
    </div>`;
        lista.appendChild(item);
    });
}


function adicionarAoCarrinho(produto) {
    const existente = carrinho.find(p => p.name === produto.name);
    if (existente) {
        existente.quantity++;
    } else {
        carrinho.push({ ...produto, quantity: 1 });
    }
    renderizarCarrinho();
}

function alterarQuantidade(nome, delta) {
    const item = carrinho.find(p => p.name === nome);
    if (item) {
        item.quantity += delta;
        if (item.quantity <= 0) carrinho = carrinho.filter(p => p.name !== nome);
        renderizarCarrinho();
    }
}

function removerDoCarrinho(nome) {
    carrinho = carrinho.filter(p => p.name !== nome);
    renderizarCarrinho();
}

function limparCarrinho() {
    carrinho = [];
    renderizarCarrinho();
}

async function finalizarVenda(event) {
    event.preventDefault();

    try{
        carrinho.forEach(item => {
            apiRequest('http://localhost:5237/ItemsSale', 'POST', {
                
            })
        })
        
        
        limparCarrinho();
        closeModal('modalVenda');
    }
    catch(error){
        console.error(error);
    }

    alert("Venda finalizada!");
    limparCarrinho();
    closeModal('modalVenda');
}



function renderizarCarrinho() {
    const lista = document.getElementById("carrinho-produtos");
    lista.innerHTML = "";
    let total = 0;
    carrinho.forEach(item => {
        const li = document.createElement("li");
        li.className = "carrinho-item";
        total += item.price * item.quantity;
        li.innerHTML = `
<div class="item-top">
  <strong>${item.name}</strong>
  <span>R$ ${item.price.toFixed(2)}</span>
</div>
<div class="item-controls">
  <button onclick="alterarQuantidade('${item.name}', -1)">-</button>
  <span>${item.quantity}</span>
  <button onclick="alterarQuantidade('${item.name}', 1)">+</button>
  <a onclick="removerDoCarrinho('${item.name}')">Remover</a>
</div>`;
        lista.appendChild(li);
    });
    document.getElementById("valor-total").textContent = `R$ ${total.toFixed(2)}`;
}

function filtrarProdutos() {
    renderizarProdutos();
}

document.addEventListener("DOMContentLoaded", () => renderizarProdutos());

async function CadastrarVenda(event) {
    event.PreventDefault(); 

    try{
        carrinho.forEach(item => {
            apiRequest('http://localhost:5237/sales', 'POST', {
                "name": item.name,
                "price": item.price,
                "quantity": item.quantity,
                "total": item.price * item.quantity
            })
        })
        
        
        limparCarrinho();
        closeModal('modalVenda');
    }
    catch(error){
        console.error(error);
    }
    
}



async function listarVendas() {
    try {

        const response = await apiRequest('http://localhost:5237/sales/completo', 'GET');

        const table = document.getElementById('vendas');
        table.innerHTML = '';
        response.forEach((sale) => {
            const quantidadeTotal = sale.itens.reduce((total, item) => total + item.quantidade, 0);
            const row = `
                <div>${sale.dataVenda}</div>
                <div>${quantidadeTotal}</div>
                <div>R$ ${sale.total}</div>
                <div>PIX</div>
                <div class="table-actions">
                    <button class="btn btn-outline">Editar</button>
                </div>
            `
            
            table.innerHTML += row
    })
    } catch (error) {
        console.error(error);
    }
}
document.addEventListener('DOMContentLoaded', listarVendas);

window.adicionarAoCarrinho = adicionarAoCarrinho;
window.alterarQuantidade = alterarQuantidade;
window.removerDoCarrinho = removerDoCarrinho;
window.limparCarrinho = limparCarrinho;
window.finalizarVenda = finalizarVenda;
window.filtrarProdutos = filtrarProdutos;

