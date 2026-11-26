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
import InformationalModal from "../../Components/InformationalModal/InformationalModal";

interface AlunoResponse{
    id: number;
    nome: string;
    email: string;
    observacao: string;
    telefone: string;
    dataNascimento: string;
    nivelAprendizado: string;
    status: boolean;
    vinculoStatus: string;
};

interface MaiorNivelAprendizadoResponse{
    totalAlunos: number;
    totalAlunosNivel: number;
    nivel: string;
};

export default function Students(){
    const [studentEmail, setStudentEmail] = useState("");
    const [student, setStudent] = useState<AlunoResponse>();
    const [amountStundets, setAmountStudents] = useState(0);
    const [highestLevelLearning, setHighestLevelLearning] = useState<MaiorNivelAprendizadoResponse>();
    const {user, loading} = useAuth();

    //Modal
    const [showModal, setShowModal] = useState(false);
    const [typeModal, setTypeModal] = useState("");
    const [titleModal, setTitleModal] = useState("");
    const [textModal, setTextModal] = useState("");

    function createStatNivel(){
        if (!highestLevelLearning) {
            return "Carregando...";
        }

        const {totalAlunos, totalAlunosNivel} = highestLevelLearning;
        
        const percentage = (totalAlunos > 0 ? (totalAlunosNivel * 100) / totalAlunos : 0);

         const percentageFormat = percentage.toLocaleString('pt-BR', {
            style: 'percent',
            minimumFractionDigits: 1,
            maximumFractionDigits: 2,
        });

        return `${highestLevelLearning.totalAlunosNivel} de ${highestLevelLearning.totalAlunos} alunos (${percentageFormat})`
    }

    async function getAlunoParaVinculo(){
        const response = await fetch(`/api/aluno/${studentEmail}/vinculo`);

        if(response.status === 404){
            setTypeModal("error");
            setTitleModal("Aluno não encontrado.");
            setTextModal(`Aluno com o e-mail "${studentEmail}" não encontrado`);
            setShowModal(true);
            return;
        }else if(!response.ok){
            const data = await response.json();
            setTypeModal("error");
            setTitleModal("Erro ao buscar aluno pelo e-mail.");
            setTextModal(data.message);
            setShowModal(true);
        }

        const data = await response.json();
        setStudent(data);
    }

    async function getAmoutAlunosByProfessor() {

        const response = await fetch(`/api/Professor/${user?.id}/alunos/quantidade`);

        if(!response.ok){
            const error = await response.json();
            throw new Error("Erro ao buscar quantidade de alunos:" + error.message);
        }

        const data = await response.json();
        setAmountStudents(data);
    }

    async function getMaiorNivelAprendizado() {
        const response = await fetch(`/api/Professor/${user?.id}/alunos/maior-nivel`);

        if(!response.ok){
            const error = await response.json();
            throw new Error("Erro ao buscar quantidade de alunos:" + error.message);
        }

        const data = await response.json();
        setHighestLevelLearning(data);
    }

    function handleCloseModal() {
        setShowModal(false);
    }


    useEffect(() =>{
        if(loading || !user?.id) return;

        getAmoutAlunosByProfessor();
        getMaiorNivelAprendizado();
    },[loading, user?.id]);

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
                            functionSreach={getAlunoParaVinculo} 
                            functionSetAttribute={setStudentEmail} 
                            textButton="Pesquisar"
                        />
                        <InfoStudent alunoResponse={student}/>
                    </div>

                    <div className={style.groupCardStats}>
                        <StatCard
                            IconStatReact={FaUserGroup}
                            titleStatCard="Total de alunos ativos"
                            amountStatCard={(amountStundets).toString()}
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
                            amountStatCard={highestLevelLearning?.nivel ?? "Sem alunos"}
                            classNameStatCard="cardOragen"
                            descriptionStatCard={createStatNivel()}
                        />
                    </div>
                </div>
            </div>

            {showModal && (
                <InformationalModal
                    typeModal={typeModal}
                    titleModal={titleModal}
                    textModal={textModal}
                    onClose={handleCloseModal}
                />
            )}
        </>
    )
}