// Exporta a função para poder ser usada em outros arquivos
export async function apiRequest(url, method = 'GET', body = null, headers = {}) {
    try {
        // Cria um objeto de configuração para a requisição
        const config = {
            method, // Define o método HTTP (GET, POST, PUT, DELETE, etc.)

            headers: {
                'Content-Type': 'application/json', // Informa que o corpo da requisição está em JSON
                ...headers // Permite adicionar outros headers customizados passados como parâmetro
            },
        };

        // Se o corpo da requisição (body) for passado (por exemplo em POST ou PUT)
        if (body) {
            config.body = JSON.stringify(body); // Converte o objeto para JSON antes de enviar
        }

        // Faz a requisição usando fetch com a URL e configuração definidas
        const response = await fetch(url, config);

        // Verifica se a resposta da requisição NÃO foi bem sucedida (status diferente de 2xx)
        if (!response.ok) {
            // Lança um erro com o código de status e o texto de status da resposta
            throw new Error(`Erro: ${response.status} - ${response.statusText}`);
        }
        // Se tudo deu certo, retorna o corpo da resposta convertido em JSON
        return await response.json();
    } catch (error) {
        // Captura qualquer erro ocorrido na requisição ou no processamento da resposta
        console.error('Erro na requisição:', error);

        // Retorna null em caso de erro, como fallback
        return null;
    }
}
