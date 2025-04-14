// Preços dos quartos (você pode substituir isso com dados vindos da API depois)
const roomPrices = {
    "Suíte Luxo com Vista para o Mar": 750,
    "Bangalô Familiar": 950,
    "Chalé Romântico": 680
};

// Preços dos serviços adicionais
const servicePrices = {
    "Café da Manhã Especial": 120,
    "Jantar Romântico": 350,
    "Pacote de Spa": 450,
    "Serviço de Transfer": 280,
    "Tour Guiado": 320
};

// Elementos
const checkinInput = document.querySelector('input[type="date"]:nth-of-type(1)');
const checkoutInput = document.querySelector('input[type="date"]:nth-of-type(2)');
const selectRoom = document.querySelector('select:nth-of-type(3)');
const totalSpan = document.querySelector(".summary-total span");
const summaryList = document.querySelector(".summary-list");
const serviceListItems = document.querySelectorAll(".services-list li");

// Atualiza total sempre que algo mudar
[checkinInput, checkoutInput, selectRoom].forEach(el => {
    el.addEventListener("change", updateTotal);
});

// Serviços adicionais
serviceListItems.forEach(item => {
    item.addEventListener("click", () => {
        item.classList.toggle("selected");
        updateTotal();
    });
});

// Função para calcular diferença de dias
function getNumberOfNights() {
    const checkinDate = new Date(checkinInput.value);
    const checkoutDate = new Date(checkoutInput.value);
    const diff = checkoutDate - checkinDate;
    return Math.max(Math.round(diff / (1000 * 60 * 60 * 24)), 0);
}

// Atualiza total da reserva
function updateTotal() {
    const nights = getNumberOfNights();
    const room = selectRoom.value;
    let total = 0;

    if (room && roomPrices[room]) {
        total += roomPrices[room] * nights;
    }

    serviceListItems.forEach(item => {
        if (item.classList.contains("selected")) {
            const serviceName = item.querySelector("strong").innerText;
            total += servicePrices[serviceName] || 0;
        }
    });

    totalSpan.textContent = `R$ ${total.toFixed(2)}`;

    // Atualiza resumo de noites
    summaryList.innerHTML = `
      <li><strong>Check-in:</strong> ${checkinInput.value.split("-").reverse().join("/")}</li>
      <li><strong>Check-out:</strong> ${checkoutInput.value.split("-").reverse().join("/")}</li>
      <li><strong>Noites:</strong> ${nights}</li>
      <li><strong>Hóspedes:</strong> 2 adultos</li>
    `;
}
