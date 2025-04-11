import { apiRequest } from "./script.js";

function login(event) {
    event.PreventDefault();
    try {
        const email = document.getElementById("email").value;
        const senha = document.getElementById("senha").value;

        const login = apiResquest(
            'http://localhost:5237/User',
            'POST',
            {
                email:email,
                passwd:senha
            }
            
        )

        console.log(login);
        
    } catch (error) {
        console.log("Deu erro" + error);
    }
    
}