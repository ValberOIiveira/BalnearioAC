    import { apiRequest } from "./script.js";

    const form = document.getElementById("produtoForm");

    if (form) {
    form.addEventListener("submit", salvarProduto);
    }

    listarProdutos();

    async function listarProdutos() {
        try {
            const data = await apiRequest('http://localhost:5237/Product');
            console.log(data);
            // Ordena os produtos do maior para o menor ID
            data.sort((a, b) => a.id - b.id);

            const tbody = document.querySelector("#tabelaProdutos tbody");
            tbody.innerHTML = "";

            data.forEach(produto => {
                const tr = document.createElement("tr");
                tr.innerHTML = `
                    <td>${produto.id}</td>
                    <td>${produto.name}</td>
                    <td>${produto.price}</td>
                    <td>${produto.quantity}</td>
                    <td>${produto.category}</td>
                    <td><button class="editBtn">Editar</button></td>
                `;

                const btn = tr.querySelector(".editBtn");
                btn.addEventListener("click", () => editarProduto(produto.id));

                tbody.appendChild(tr);
            });

        } catch (error) {
            console.error("Erro ao listar produtos:", error);
        }
    }


    async function salvarProduto(event) {
        event.preventDefault();

        try {
            const name = document.getElementById('name').value;
            const price = document.getElementById('price').value;
            const quantity = document.getElementById('qtd').value; // Aqui usamos o id "qtd"
            const category = document.getElementById('category').value; // Aqui usamos o id "qtd"

            if (!name || !price || !quantity || !category) {
                alert('Preencha todos os campos');
                return;
            }

            // Se o ID do produto está preenchido, fazemos um PUT para editar
            const id = document.getElementById('id').value;

            let response;
            if (id) {
                // Atualiza o produto (PUT)
                response = await apiRequest(`http://localhost:5237/Product/${id}`, 'PUT', { id, name, price, quantity, category });
            } else {
                // Cria um novo produto (POST)
                response = await apiRequest('http://localhost:5237/Product', 'POST', { name, price, quantity, category });
            }

            console.log(response);

            // Limpa o formulário e recarrega a lista de produtos
            document.getElementById("produtoForm").reset();
            listarProdutos();
        } catch (error) {
            console.error('Erro ao salvar:', error);
        }
    }

    async function editarProduto(id) {
        try {
            const produto = await apiRequest(`http://localhost:5237/Product/${id}`, 'GET');

            // Preenche os campos do formulário com os dados do produto
            document.getElementById('id').value = produto.id;
            document.getElementById('name').value = produto.name;
            document.getElementById('price').value = produto.price;
            document.getElementById('qtd').value = produto.quantity; // Garantimos que estamos usando "quantity"
            document.getElementById('category').value = produto.category; // Garantimos que estamos usando "quantity"
        } catch (error) {
            console.error("Erro ao carregar produto para edição:", error);
        }
    }
