const produtos = [
    { nome: 'Cerveja Pilsen', categoria: 'Bebidas', preco: 12.00 },
    { nome: 'Refrigerante Cola', categoria: 'Bebidas', preco: 8.00 },
    { nome: 'Água Mineral', categoria: 'Bebidas', preco: 5.00 },
    { nome: 'Porção de Batata', categoria: 'Alimentos', preco: 25.00 },
    { nome: 'Espetinho Carne', categoria: 'Alimentos', preco: 15.00 }
];

let carrinho = [];

function renderizarProdutos() {
    const lista = document.getElementById("lista-produtos");
    lista.innerHTML = "";
    const termo = document.getElementById("buscar-produto-modal").value.toLowerCase();
    produtos.filter(p => p.nome.toLowerCase().includes(termo)).forEach(prod => {
        const item = document.createElement("li");
        item.className = "produto-item";
        item.innerHTML = `
    <div>
    <strong>${prod.nome}</strong>
    <p class="font-2-s">${prod.categoria}</p>
    </div>
    <div>
    <span>R$ ${prod.preco.toFixed(2)}</span>
    <button class="btn btn-outline" onclick='adicionarAoCarrinho(${JSON.stringify(prod).replace(/"/g, "&quot;")})'>+</button>
    </div>`;
        lista.appendChild(item);
    });
}

function adicionarAoCarrinho(produto) {
    const existente = carrinho.find(p => p.nome === produto.nome);
    if (existente) {
        existente.qtd++;
    } else {
        carrinho.push({ ...produto, qtd: 1 });
    }
    renderizarCarrinho();
}

function alterarQuantidade(nome, delta) {
    const item = carrinho.find(p => p.nome === nome);
    if (item) {
        item.qtd += delta;
        if (item.qtd <= 0) carrinho = carrinho.filter(p => p.nome !== nome);
        renderizarCarrinho();
    }
}

function removerDoCarrinho(nome) {
    carrinho = carrinho.filter(p => p.nome !== nome);
    renderizarCarrinho();
}

function limparCarrinho() {
    carrinho = [];
    renderizarCarrinho();
}

function finalizarVenda() {
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
        total += item.preco * item.qtd;
        li.innerHTML = `
<div class="item-top">
  <strong>${item.nome}</strong>
  <span>R$ ${item.preco.toFixed(2)}</span>
</div>
<div class="item-controls">
  <button onclick="alterarQuantidade('${item.nome}', -1)">-</button>
  <span>${item.qtd}</span>
  <button onclick="alterarQuantidade('${item.nome}', 1)">+</button>
  <a onclick="removerDoCarrinho('${item.nome}')">Remover</a>
</div>`;
        lista.appendChild(li);
    });
    document.getElementById("valor-total").textContent = `R$ ${total.toFixed(2)}`;
}

function filtrarProdutos() {
    renderizarProdutos();
}

document.addEventListener("DOMContentLoaded", () => renderizarProdutos());