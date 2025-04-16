import { apiRequest } from './script.js';

async function Login(event){
    event.preventDefault();

    try {
        const email = document.getElementById('email').value;
        const Password = document.getElementById('senha').value;

        const response = await apiRequest('http://localhost:5237/api/login', 'POST', { email, Password });

        if (response) {
            console.log( response );
            if (response.id_user_type == 1 || response.id_user_type == 2){ 
                window.location.href = './homeAdmin.html';
            }
            else{
                window.location.href = './homeUser.html';
            }
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