const checkinInput = document.getElementById("data-checkin");
const checkoutInput = document.getElementById("data-checkout");
const tipoSelect = document.getElementById("tipo-quiosque");
const resumoAtual = document.getElementById("resumo-atual");
const totalSpan = document.getElementById("resumo-total");
const placeholder = document.getElementById("resumo-placeholder");
const listaReservas = document.getElementById("lista-reservas");

const valores = {
    "Quiosque Beira-Rio": 180,
    "Quiosque Sombra Natural": 160,
    "Quiosque Central": 150,
    "Quiosque Infantil": 140,
    "Quiosque Panorâmico": 190,
    "Churrasqueira Coberta - Pedra": 250,
    "Churrasqueira Coberta - Madeira": 270,
};

function calcularResumo() {
    const checkin = checkinInput.value;
    const checkout = checkoutInput.value;
    const tipo = tipoSelect.value;

    if (!(checkin && checkout && valores[tipo])) return;

    const dataInicio = new Date(checkin);
    const dataFim = new Date(checkout);
    const dias = (dataFim - dataInicio) / (1000 * 60 * 60 * 24);
    if (dias <= 0 || isNaN(dias)) return;

    const total = valores[tipo] * dias;
    resumoAtual.innerHTML = `
    <li><strong>Check-in:</strong> ${checkin}</li>
    <li><strong>Check-out:</strong> ${checkout}</li>
    <li><strong>Noites:</strong> ${dias}</li>
    <li><strong>Espaço Selecionado:</strong> ${tipo}</li>
  `;
    totalSpan.innerText = `R$ ${total.toFixed(2)}`;
    placeholder.style.display = "none";
}

// Atualiza resumo em tempo real
[checkinInput, checkoutInput, tipoSelect].forEach((el) =>
    el.addEventListener("change", calcularResumo)
);

// Confirma reserva e adiciona à lista
document.querySelector(".btn-green").addEventListener("click", function (e) {
    e.preventDefault();

    const checkin = checkinInput.value;
    const checkout = checkoutInput.value;
    const tipo = tipoSelect.value;

    const dataInicio = new Date(checkin);
    const dataFim = new Date(checkout);
    const dias = (dataFim - dataInicio) / (1000 * 60 * 60 * 24);

    if (!checkin || !checkout || !valores[tipo] || dias <= 0) {
        alert("Preencha corretamente todos os campos.");
        return;
    }

    const total = valores[tipo] * dias;

    const reserva = document.createElement("ul");
    reserva.classList.add("summary-list");
    reserva.style.marginBottom = "2rem";
    reserva.innerHTML = `
    <li><strong>Check-in:</strong> ${checkin}</li>
    <li><strong>Check-out:</strong> ${checkout}</li>
    <li><strong>Noites:</strong> ${dias}</li>
    <li><strong>Espaço:</strong> ${tipo}</li>
    <li><strong>Total:</strong> R$ ${total.toFixed(2)}</li>
  `;

    listaReservas.appendChild(reserva);
});
