import React, { useEffect, useState } from "react";
import style from "./TeacherRegistration.module.css"
import { useNavigate } from "react-router-dom";

interface Materia{
    id: number;
    nome: string;
}

interface ProfessorRequest{
    Nome: string;
    Email: string;
    Senha: string;
    Descricao: string;
    IdMaterias: number[];
}

export default function TeacherRegistration() {
    const [senhasIguais, setSenhasIguais] = useState("");

    const navigate = useNavigate();

    const [materias, setMaterias] = useState<Materia[]>([]);

    const [selectedMaterias, setSelectedMaterias] = useState<Materia[]>([]);

    //Dados Professor
    const [nome, setNome] = useState("");
    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");
    const [descricao, setDescricao] = useState("");

    async function getMaterias() {
        const response = await fetch("/api/materia");

        if (!response.ok) {
            const error = await response.json();
            throw new Error("Erro ao buscar níveis de aprendizado: " + error.message);
        }
        
        const data: Materia[] = await response.json();
        setMaterias(data);
    }

    async function postProfessor(event: React.SubmitEvent){
        event.preventDefault();
        
        const profRequest:ProfessorRequest = {
            Nome: nome,
            Email: email,
            Senha: senha,
            Descricao: descricao,
            IdMaterias: selectedMaterias.map((m) => m.id)
        };

        const response = await fetch("/api/professor",{
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            credentials: "include",
            body: JSON.stringify(
                profRequest
            )
        });

        if(response.ok){
            navigate("/login", {replace: true, state: {email: profRequest.Email}});
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

    function handleSelectMateria(id: number){
        const materiaSelecionada = materias.find((m) => m.id === id);

        if(!materiaSelecionada){
            return;
        }

        if(selectedMaterias.some((m) => m.id === id)){
            return;
        }
        
        setSelectedMaterias((prev) => [...prev,materiaSelecionada]);
    }

    function removeSelectMateria(id: number){
        const materiasFiltro = selectedMaterias.filter(materia => materia.id !== id);
        setSelectedMaterias(materiasFiltro);
    }

    useEffect(() => {
        getMaterias()
    },[])

    return (
        <>
            <div className={style.formGroup}>
                <form className={style.form} onSubmit={postProfessor}>
                    <h1 className={style.title}>Cadastro Professor</h1>

                    <div className={style.inputGroup}>
                        <label htmlFor="nomeProf">Nome:</label>
                        <input type="text" name="nomeProf" id="nomeProf" placeholder="Digite o nome completo" required onChange={(e) => setNome(e.target.value)} />
                    </div>

                    <div className={style.inputGroup}>
                        <label htmlFor="emailProf">Email:</label>
                        <input type="email" name="emailProf" id="emailProf" placeholder="prof@email.com" required onChange={(e) => setEmail(e.target.value)} />
                    </div>

                    <div className={style.inputGroup}>
                        <label htmlFor="descricaoProf">Descrição:</label>
                        <textarea name="descricaoProf" id="descricaoProf" placeholder="Descrição professor" required onChange={(e) => setDescricao(e.target.value)} />
                    </div>

                    <div className={style.row}>
                        <div className={style.inputGroup}>
                            <label htmlFor="selectMaterias">Materias</label>
                            <select id="selectMaterias" name="materias" defaultValue="" required onChange={(e) => handleSelectMateria(Number(e.target.value))}>
                                <option value="" disabled>Selecione...</option>
                                {materias.map((materia) =>(
                                    <option key={materia.id} value={materia.id}>
                                        {materia.nome}
                                    </option>
                                ))}
                            </select>
                            <div className={style.groupMaterias}>
                                {selectedMaterias.map((materia) => (
                                    <div key={materia.id} className={style.cardMateria}>
                                        <button type="button" className={style.removeButton} onClick={() => removeSelectMateria(materia.id)}>x</button>
                                        <p>{materia.nome}</p>
                                    </div>
                                ))}
                            </div>
                        </div>

                        <div className={style.inputGroup}>
                            <label htmlFor="senhaProf">Senha:</label>
                            <input type="password" name="senhaProf" id="senhaProf" placeholder="Digite sua senha" required onChange={(e) => setSenha(e.target.value)} />
                        </div>

                        <div className={style.inputGroup}>
                            <label htmlFor="confirmSenhaProf">Confirmar Senha:</label>
                            <input type="password" name="confirmSenhaProf" id="confirmSenhaProf" placeholder="Confirme a senha" required onChange={(e) =>
                                validatePassword(senha, e.target.value)
                            } />
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