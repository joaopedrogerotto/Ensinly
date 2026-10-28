import React, { useEffect, useState } from "react";
import style from "./StudentRegistration.module.css"
import { useNavigate } from "react-router-dom";

interface NivelAprendizado {
    id: number;
    nivel: string;
};

interface AlunoRequest{
    Nome: string;
    Email: string;
    Senha: string;
    DataNascimento: string;
    IdNivelAprendizado: number;
}

export default function CadastroAluno() {
    const [niveis, setNiveis] = useState<NivelAprendizado[]>([]);

    const [senhasIguais, setSenhasIguais] = useState("");

    const navigate = useNavigate();

    //Dados Aluno
    const [nome, setNome] = useState("");
    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");
    const [dtNascimento, setDtNascimento] = useState("");
    const [idNivelAprendizado, setIdNivelAprendizado] = useState(0);

    async function getNiveisAprendizado() {
        const response = await fetch("/api/nivelAprendizado");

        if (!response.ok) {
            const error = await response.json();
            throw new Error("Erro ao buscar níveis de aprendizado: " + error.message);
        }

        const data: NivelAprendizado[] = await response.json();
        setNiveis(data);
    }

    async function postAluno(event: React.SubmitEvent) {
        event.preventDefault();

        const alunoRequest: AlunoRequest = {
            Nome: nome,
            Email: email,
            Senha: senha,
            DataNascimento: dtNascimento,
            IdNivelAprendizado: idNivelAprendizado
        };

        const response = await fetch("/api/aluno",{
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            credentials: "include",
            body: JSON.stringify(
                alunoRequest
            )
        });
            
        if(response.ok){
            navigate("/login", {replace: true, state: {email: alunoRequest.Email}});
        }else{
            console.log(await response.text());
        }
    }

    function validatePassword(password: string, confirmPassword:string){
        if(password === confirmPassword){
            setSenha(password);
            setSenhasIguais("");
        }else{
            setSenhasIguais("As senhas não coincidem");
        }
    }

    //assim que renderizar o componente, já chama a função
    useEffect(() => {
        getNiveisAprendizado();
    }, []);

    return (
        <>
            <div className={style.formGroup} onSubmit={postAluno}>
                <form className={style.form}>
                    <h1 className={style.title}>Cadastro de Aluno</h1>

                    <div className={style.inputGroup}>
                        <label htmlFor="nomeAluno">Nome:</label>
                        <input type="text" name="nomeAluno" id="nomeAluno" placeholder="Digite o nome completo" required  onChange={(e) => setNome(e.target.value)}/>
                    </div>

                    <div className={style.inputGroup}>
                        <label htmlFor="emailAluno">Email:</label>
                        <input type="email" name="emailAluno" id="emailAluno" placeholder="aluno@email.com" required  onChange={(e) => setEmail(e.target.value)}/>
                    </div>

                    <div className={style.row}>
                        <div className={style.inputGroup}>
                            <label htmlFor="dtNascimentoAluno">Data de nascimento:</label>
                            <input type="date" name="dtNascimentoAluno" id="dtNascimentoAluno" required onChange={(e) => setDtNascimento(e.target.value)}/>
                        </div>

                        <div className={style.inputGroup}>
                            <label htmlFor="selectNivelAprendizado">Nível de aprendizado:</label>
                            <select id="selectNivelAprendizado" name="nivelAprendizado" defaultValue="" required onChange={(e) => {setIdNivelAprendizado(Number.parseInt(e.target.value))}}>
                                <option value="" disabled>Selecione...</option>
                                {niveis.map((nivel) => (
                                    <option key={nivel.id} value={nivel.id}>
                                        {nivel.nivel}
                                    </option>
                                ))}
                            </select>
                        </div>

                        <div className={style.inputGroup}>
                            <label htmlFor="senhaAluno">Senha:</label>
                            <input type="password" name="senhaAluno" id="senhaAluno" placeholder="Digite sua senha" required onChange={(e) => setSenha(e.target.value)}/>
                        </div>

                        <div className={style.inputGroup}>
                            <label htmlFor="confirmSenha">Confirmar Senha:</label>
                            <input type="password" name="senhaAluno" id="senhaAluno" placeholder="Confirme a senha" required  onChange={(e) =>
                                validatePassword(senha, e.target.value)
                            }/>
                            {senhasIguais && (
                                <span className={style.errorMessage}>
                                    {senhasIguais}
                                </span>                          
                            )}
                        </div>
                    </div>

                    <button type="submit" className={style.button}>Cadastrar</button>
                </form>
            </div>
        </>
    );
}