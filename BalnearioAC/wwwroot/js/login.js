import { apiRequest } from './script.js';

async function Login(event){
    event.preventDefault();

    try {
        const email = document.getElementById('email').value;
        const Password = document.getElementById('senha').value;

        const response = await apiRequest('http://localhost:5237/api/login', 'POST', { email, Password });

        if (response) {
            console.log( response );
            window.location.href = './index.html';
        }
        else{
            console.log('Erro ao fazer login');
        }
        
        
    } catch (error) {
        console.log(error + "burrão");
        
    }
}
document.getElementById('login-form').addEventListener('submit', Login);


// falar com o rhyan sobre o banco de dados tabela usertype 