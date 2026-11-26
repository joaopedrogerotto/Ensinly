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
    const [confirmPassword, setConfirmPassword] = useState("");

    const navigate = useNavigate();

    const [subjects, setSubjects] = useState<Materia[]>([]);

    const [selectedSubjects, setSelectedSubjects] = useState<Materia[]>([]);

    //Dados Professor
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [description, setDescription] = useState("");

    async function getMaterias() {
        const response = await fetch("/api/materia");

        if (!response.ok) {
            const error = await response.json();
            throw new Error("Erro ao buscar níveis de aprendizado: " + error.message);
        }
        
        const data: Materia[] = await response.json();
        setSubjects(data);
    }

    async function postProfessor(event: React.SubmitEvent){
        event.preventDefault();
        
        const profRequest:ProfessorRequest = {
            Nome: name,
            Email: email,
            Senha: password,
            Descricao: description,
            IdMaterias: selectedSubjects.map((m) => m.id)
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
            setPassword(password);
            setConfirmPassword("");
        }else{
            setConfirmPassword("As senhas não coincidem");
        }
    }

    function handleSelectMateria(id: number){
        const materiaSelecionada = subjects.find((s) => s.id === id);

        if(!materiaSelecionada){
            return;
        }

        if(selectedSubjects.some((s) => s.id === id)){
            return;
        }
        
        setSelectedSubjects((prev) => [...prev,materiaSelecionada]);
    }

    function removeSelectMateria(id: number){
        const materiasFiltro = selectedSubjects.filter(materia => materia.id !== id);
        setSelectedSubjects(materiasFiltro);
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
                        <input type="text" name="nomeProf" id="nomeProf" placeholder="Digite o nome completo" required onChange={(e) => setName(e.target.value)} />
                    </div>

                    <div className={style.inputGroup}>
                        <label htmlFor="emailProf">Email:</label>
                        <input type="email" name="emailProf" id="emailProf" placeholder="prof@email.com" required onChange={(e) => setEmail(e.target.value)} />
                    </div>

                    <div className={style.inputGroup}>
                        <label htmlFor="descricaoProf">Descrição:</label>
                        <textarea name="descricaoProf" id="descricaoProf" placeholder="Descrição professor" required onChange={(e) => setDescription(e.target.value)} />
                    </div>

                    <div className={style.row}>
                        <div className={style.inputGroup}>
                            <label htmlFor="selectMaterias">Materias</label>
                            <select id="selectMaterias" name="materias" defaultValue="" required onChange={(e) => handleSelectMateria(Number(e.target.value))}>
                                <option value="" disabled>Selecione...</option>
                                {subjects.map((subject) =>(
                                    <option key={subject.id} value={subject.id}>
                                        {subject.nome}
                                    </option>
                                ))}
                            </select>
                            <div className={style.groupMaterias}>
                                {selectedSubjects.map((subject) => (
                                    <div key={subject.id} className={style.cardMateria}>
                                        <button type="button" className={style.removeButton} onClick={() => removeSelectMateria(subject.id)}>x</button>
                                        <p>{subject.nome}</p>
                                    </div>
                                ))}
                            </div>
                        </div>

                        <div className={style.inputGroup}>
                            <label htmlFor="senhaProf">Senha:</label>
                            <input type="password" name="senhaProf" id="senhaProf" placeholder="Digite sua senha" required onChange={(e) => setPassword(e.target.value)} />
                        </div>

                        <div className={style.inputGroup}>
                            <label htmlFor="confirmSenhaProf">Confirmar Senha:</label>
                            <input type="password" name="confirmSenhaProf" id="confirmSenhaProf" placeholder="Confirme a senha" required onChange={(e) =>
                                validatePassword(password, e.target.value)
                            } />
                            {confirmPassword && (
                                <span className={style.errorMessage}>
                                    {confirmPassword}
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