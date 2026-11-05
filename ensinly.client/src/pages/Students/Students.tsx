import { useEffect, useState } from "react";
import MyStudents from "../../Components/MyStudents/MyStudents";
import Navbar from "../../Components/NavBar/Navbar";
import SreachBar from "../../Components/SreachBar/SreachBar";
import style from "./Students.module.css";
import InfoStudent from "../../Components/InfoStudent/InfoStudent";
import StatCard from "../../Components/StatCard/StatCard";
import { FaUserGroup } from "react-icons/fa6";
import { FaClipboard } from "react-icons/fa";
import { useAuth } from "../../Context/AuthContext";

interface AlunoResponse{
    id: number;
    nome: string;
    email: string;
    observacao: string;
    telefone: string;
    dataNascimento: string;
    nivelAprendizado: string;
    status: boolean;
};

interface MaiorNivelAprendizadoResponse{
    totalAlunos: number;
    totalAlunosNivel: number;
    nivel: string;
};

export default function Students(){
    const [emailAluno, setEmailAluno] = useState("");
    const [aluno, setAluno] = useState<AlunoResponse>();
    const [quantidadeAlunos, setQuantidadeAluno] = useState(0);
    const [maiorNivelAprendizado, setMaiorNivelAprendizado] = useState<MaiorNivelAprendizadoResponse>();
    const {user} = useAuth();

    function createStatNivel(){
        if (!maiorNivelAprendizado) {
            return "Carregando...";
        }

        const {totalAlunos, totalAlunosNivel} = maiorNivelAprendizado;
        
        const percentage = totalAlunos > 0 ? (totalAlunosNivel * 100) / totalAlunosNivel : 0;

        return `${maiorNivelAprendizado.totalAlunosNivel} de ${maiorNivelAprendizado.totalAlunos} alunos (${percentage}%)`
    }

    async function getAlunoByEmail(){
        const response = await fetch(`/api/aluno/${emailAluno}`);

        if(!response.ok){
            const error = await response.json();
            throw new Error(error.message);
        }

        const data = await response.json();
        setAluno(data);
    }

    async function getAmoutAlunosByProfessor() {

        const response = await fetch(`/api/Professor/${user?.id}/alunos/quantidade`);

        if(!response.ok){
            const error = await response.json();
            throw new Error("Erro ao buscar quantidade de alunos:" + error.message);
        }

        const data = await response.json();
        setQuantidadeAluno(data);
    }

    async function getMaiorNivelAprendizado() {
        const response = await fetch(`/api/Professor/${user?.id}/alunos/maior-nivel`);

        if(!response.ok){
            const error = await response.json();
            throw new Error("Erro ao buscar quantidade de alunos:" + error.message);
        }

        const data = await response.json();
        setMaiorNivelAprendizado(data);
    }

    useEffect(() =>{
        getAmoutAlunosByProfessor();
        getMaiorNivelAprendizado();
    },[]);

    return (
        <>
            <Navbar/>
            <div className={style.container}>
                <div>
                    <MyStudents/>
                </div>
                <div className={style.divInfoStudent}>

                    <div className={style.studentDetails}>
                        <SreachBar 
                            classStyleName="sreachBarStudents" 
                            labelText="Pesquisar aluno por e-mail" 
                            placeholder="exemplo@email.com" 
                            functionSreach={getAlunoByEmail} 
                            functionSetAttribute={setEmailAluno} 
                            textButton="Pesquisar"
                        />
                        <InfoStudent alunoResponse={aluno}/>
                    </div>

                    <div className={style.groupCardStats}>
                        <StatCard
                            IconStatReact={FaUserGroup}
                            titleStatCard="Total de alunos ativos"
                            amountStatCard={(quantidadeAlunos).toString()}
                            classNameStatCard="cardGreen"
                        />
                        <StatCard
                            IconStatReact={FaClipboard}
                            titleStatCard="Atividade pendentes"
                            amountStatCard={(6).toString()}
                            classNameStatCard="cardPurple"
                        />
                        <StatCard
                            IconStatReact={FaUserGroup}
                            titleStatCard="Nivel de aprendizado"
                            amountStatCard={maiorNivelAprendizado?.nivel ?? "Carregando..."}
                            classNameStatCard="cardOragen"
                            descriptionStatCard={createStatNivel()}
                        />
                    </div>
                </div>
            </div>
        </>
    )
}