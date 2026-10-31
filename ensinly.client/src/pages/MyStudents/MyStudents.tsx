import { useEffect, useState } from "react";
import { useAuth } from "../../Context/AuthContext";
import Navbar from "../../Components/NavBar/Navbar";
import style from "./MyStudents.module.css"

interface AlunoResponse{
    id: number;
    nome: string;
    email: string;
    observacao: string;
    telefone: string;
    dataNascimento: Date;
    nivelAprendizado: string;
    status: boolean;
};

export default function MyStudents(){
    const {user} = useAuth();

    const [alunos, setAlunos] = useState<AlunoResponse[]>([]);

    async function getStudents() {
        const response = await fetch(`/api/Professor/${user?.id}/alunos`);

        if(!response.ok){
            const error = await response.json();
            throw new Error("Erro ao buscar alunos: " + error.message);
        }

        const data = await response.json();
        setAlunos(data);
    }

    useEffect(() => {
        getStudents()
    },[])

    return(
        <>
            <Navbar/>   
            <div className={style.page}>
                <h1 className={style.title}>Meus Alunos</h1>
                <div className={style.tableWrapper}>
                    <table className={style.tableStudents}>
                        <thead className={style.tableHead}>
                            <tr>
                                <th>Nome</th>
                                <th>E-mail</th>
                                <th>Telefone</th>
                                <th>Nível Aprendizado</th>
                                <th>Status</th>
                            </tr>
                        </thead>

                        <tbody>
                            {alunos.map((a) => (
                                <tr key={a.id}>
                                    <td>{a.nome}</td>
                                    <td>{a.email}</td>
                                    <td>{
                                        a.telefone ? 
                                            (a.telefone) : 
                                            (<span className={style.notInformed}>Não informado</span>)
                                    }</td>
                                    <td>{a.nivelAprendizado}</td>
                                    <td>
                                        <span className={`${style.badge} ${a.status ? style.active : style.inactive}`}>{a.status ? "Ativo" : "Inativo"}</span></td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div>
        </>
    );
}