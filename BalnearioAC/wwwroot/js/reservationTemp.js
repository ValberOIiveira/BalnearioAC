/*  ------------------  elementos  ------------------ */
const checkinInput = document.getElementById("data-checkin");
const checkoutInput = document.getElementById("data-checkout");
const tipoSelect = document.getElementById("tipo-quiosque");
const resumoAtual = document.getElementById("resumo-atual");
const totalSpan = document.getElementById("resumo-total");
const placeholder = document.getElementById("resumo-placeholder");
const listaReservas = document.getElementById("lista-reservas");
const formVisitante = document.getElementById("formVisitante");

/* -------------------  valores  -------------------- */
const valores = {
    "Quiosque Beira-Rio": 180,
    "Quiosque Sombra Natural": 160,
    "Quiosque Central": 150,
    "Quiosque Infantil": 140,
    "Quiosque Panorâmico": 190,
    "Churrasqueira Coberta - Pedra": 250,
    "Churrasqueira Coberta - Madeira": 270,
};

let visitantes = [];   // armazena visitantes enquanto usuário monta reserva

/* -------------------  helpers  -------------------- */
function diffEmDias(a, b) { return Math.max((b - a) / (1000 * 60 * 60 * 24), 0); }

function atualizarResumo() {
    const ci = checkinInput.value, co = checkoutInput.value, tipo = tipoSelect.value;
    if (!(ci && co && valores[tipo])) return;

    const dias = diffEmDias(new Date(ci), new Date(co));
    const total = dias * valores[tipo];

    let html = `
      <li><strong>Check‑in:</strong> ${ci}</li>
      <li><strong>Check‑out:</strong> ${co}</li>
      <li><strong>Noites:</strong> ${dias}</li>
      <li><strong>Espaço:</strong> ${tipo}</li>`;
    if (visitantes.length) {
        html += `<li><strong>Visitantes:</strong> ${visitantes.length}</li>`;
        visitantes.forEach(v => html += `<li>- ${v.nome}</li>`);
    }
    resumoAtual.innerHTML = html;
    totalSpan.textContent = "R$ " + total.toFixed(2);
    placeholder.style.display = "none";
}

/* ------------  listeners de formulário  ----------- */
[checkinInput, checkoutInput, tipoSelect].forEach(el => el.addEventListener("change", atualizarResumo));

formVisitante.addEventListener("submit", e => {
    e.preventDefault();
    const { nome, cpf, nascimento } = formVisitante;
    visitantes.push({ nome: nome.value.trim(), cpf: cpf.value.trim(), nascimento: nascimento.value });
    formVisitante.reset();
    closeModal("modalVisitantes");
    atualizarResumo();
});

/* ------------  confirmar e salvar reserva  -------- */
document.querySelector(".btn-green").addEventListener("click", e => {
    e.preventDefault();
    const ci = checkinInput.value, co = checkoutInput.value, tipo = tipoSelect.value;
    const dias = diffEmDias(new Date(ci), new Date(co));
    if (!(ci && co && valores[tipo]) || dias <= 0) {
        alert("Preencha todos os campos corretamente.");
        return;
    }
    const total = dias * valores[tipo];
    const ul = document.createElement("ul");
    ul.className = "summary-list";
    ul.innerHTML = `
     <li><strong>Check‑in:</strong> ${ci}</li>
     <li><strong>Check‑out:</strong> ${co}</li>
     <li><strong>Noites:</strong> ${dias}</li>
     <li><strong>Espaço:</strong> ${tipo}</li>
     <li><strong>Total:</strong> R$ ${total.toFixed(2)}</li>`;
    if (visitantes.length) {
        ul.innerHTML += `<li><strong>Visitantes:</strong> ${visitantes.length}</li>`;
        visitantes.forEach(v => ul.innerHTML += `<li>${v.nome} - ${v.cpf} - ${v.nascimento}</li>`);
    }
    listaReservas.appendChild(ul);

    /* limpa estado para nova reserva */
    visitantes = [];
    resumoAtual.innerHTML = "";
    totalSpan.textContent = "R$ 0";
    placeholder.style.display = "";
    checkinInput.value = checkoutInput.value = tipoSelect.value = "";
});

/* -------------  abre/fecha modal (global) ---------- */
// modal.js → já incluído; se quiser centralizar funções:
function openModal(id) { document.getElementById(id).classList.add("show"); }
function closeModal(id) { document.getElementById(id).classList.remove("show"); }
