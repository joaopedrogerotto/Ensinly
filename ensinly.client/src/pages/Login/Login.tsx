import React, { useState} from "react";
import style from "./Login.module.css"
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../Context/AuthContext";

interface LoginRequest{
  Email: string,
  Senha: string
}

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();
  const {setUser} = useAuth();

  async function handleLogin(event: React.SubmitEvent) {
    event.preventDefault();

    const login: LoginRequest = {Email: email, Senha: password}

    const response = await fetch("api/login",{
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      credentials: "include",
      body: JSON.stringify(
        login
      )
    });
    
    if(response.ok){
        const usuario = await response.json();
        setUser(usuario);
        navigate("/home", {replace: true});
    }else{
      const message = await response.text();
      setError(message || "Email e/ou senha inválidos.");
    }
  }

  return (
    <main className={style.main}>
      <img src="/Ensinly_Logo.png" alt="Ensinly Logo" />
      <form onSubmit={handleLogin} className={style.formLogin}>
        <h1>Login</h1>
        
        <div className={style.inputEmail}>
          <label htmlFor="loginEmail">Email:</label>
          <input type="email" name="loginEmail" id="loginEmail" value={email} required onChange={(e) => setEmail(e.target.value)}/>
        </div>

        <div className={style.inputPassword}>
          <label htmlFor="loginPwd">Senha:</label>
          <input type="password" name="loginPwd" id="loginPwd" value={password} required onChange={(e) => setPassword(e.target.value)}/>
        </div>

        <div className={style.errorLogin} id="errorLogin">
          <p>{error}</p>
        </div>

        <div className={style.groupBtnLogin}>
          <p>Não possui Login? <a href={style.linkRegister} onClick={() => navigate("/Registration/Registration")}>Cadastrar-se</a></p>
          
          <button type="submit">Entrar</button>
        </div>
      </form>
    </main>    
  );
}
