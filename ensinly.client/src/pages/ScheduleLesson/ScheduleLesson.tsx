import { useEffect, useState } from "react";
import Navbar from "../../Components/NavBar/Navbar";
import style from "./ScheduleLesson.module.css"
import { useAuth } from "../../Context/AuthContext";
import Select from "react-select";
import Button from "../../Components/Button/Button";
import InformationalModal from "../../Components/InformationalModal/InformationalModal";
import { useNavigate } from "react-router-dom";

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

interface OpcaoAluno {
    value: number;
    label: string;
}

interface AulaRequest{
    idAluno: number;
    dataHoraInicio: string;
    dataHoraFim: string;
    observacao?: string;
}

export default function ScheduleLesson(){
    const {user} = useAuth();
    const [alunos, setAlunos] = useState<AlunoResponse[]>([]);
    const [alunoSelecionado, setAlunoSelecionado] = useState<OpcaoAluno | null>(null);
    const opcoesAlunos: OpcaoAluno[] = alunos.map((aluno) => ({
        value: aluno.id,
        label: aluno.nome,
    }));

    const navigate = useNavigate();

    //campos para Aula request
    const [dataInicio, setDataInicio] = useState<string>();
    const [horaInicio, setHoraInicio] = useState<string>();
    const [dataFim, setDataFim] = useState<string>();
    const [horaFim, setHoraFim] = useState<string>();
    const [observacao, setObservacao] = useState<string>();

    //Modal
    const [showModal, setShowModal] = useState(false);
    const [typeModal, setTypeModal] = useState("");
    const [titleModal, setTitleModal] = useState("");
    const [textModal, setTextModal] = useState("");

    //Errors
    const [errorData, setErrorData] = useState("");

    async function getStudents() {
        const response = await fetch(`/api/Professor/${user!.id}/alunos`);

        if(!response.ok){
            const error = await response.json();
            throw new Error("Erro ao buscar alunos: " + error.message);
        }

        const data = await response.json();
        setAlunos(data);
    }

    async function postAula(){
        const dataHoraInicio = `${dataInicio}T${horaInicio}`;
        const dataHoraFim = `${dataFim}T${horaFim}`;

        if(Date.parse(dataHoraInicio) >= Date.parse(dataHoraFim) ){
            setErrorData("A data e hora de início não pode ser maior que a data e hora final.");
            return;
        }

        const AulaRequest: AulaRequest = {
            idAluno: Number(alunoSelecionado?.value),
            dataHoraInicio: dataHoraInicio,
            dataHoraFim: dataHoraFim
        };

        if(observacao !== "" || observacao !== null){
            AulaRequest.observacao = observacao;
        }

        const response = await fetch("/api/aula", {
            method: "POST",
            headers:{
                "Content-Type": "application/json",
            },
            credentials: "include",
            body: JSON.stringify(
                AulaRequest
            )
        });

        if(response.status === 409){
            const error = await response.json();
            setTypeModal("error");
            setTitleModal("Aula já agendada")
            setTextModal(error.message);
            setShowModal(true);
            return;
        }

        if(response.ok){
            setTypeModal("success");
            setTitleModal("Aula agendada")
            setTextModal("Aula agendada com sucesso.");
            setShowModal(true);
            return;
        }else{
            console.log(await response.text());
            return
        }
    }

    function handleCloseModal() {
        setShowModal(false);
    }

    function goBackPage(){
        navigate("/professor/agenda");
    }

    useEffect(() =>{
        getStudents();
    },[]);    

    return(
        <>
            <Navbar/>
            <div className={style.container}>
                <div className={style.form}>
                    <h1 className={style.title}>Agendar aula</h1>

                    <div className={style.inputGroup}>
                        <label htmlFor="nomeProf">Aluno:</label>
                        <Select
                            inputId="selectAlunoAgenda"
                            name="alunoAgenda"
                            options={opcoesAlunos}
                            value={alunoSelecionado}
                            onChange={setAlunoSelecionado}
                            placeholder="Digite para buscar..."
                            noOptionsMessage={() => "Nenhum aluno encontrado"}
                            isClearable
                            required
                        />
                    </div>
                    <div className={style.dateTimeGroup}>
                        <p>Início da aula:</p>
                        <div className={style.inputGroup}>
                            <label htmlFor="dtAulaInicio">Data:</label>
                            <input type="date" name="dtAulaInicio" min={new Date().toLocaleDateString("sv-SE")} onChange={(e) => setDataInicio(e.target.value)} id="dtAulaInicio" required/>
                        </div>
                        <div className={style.inputGroup}>
                            <label htmlFor="horaInicioAula">Horário:</label>
                            <input type="time" name="horaAula" id="horaInicioAula" onChange={(e) => setHoraInicio(e.target.value)} required/>
                        </div>
                    </div>
                    <div className={style.dateTimeGroup}>
                        <p>Fim da aula:</p>
                        <div className={style.inputGroup}>
                            <label htmlFor="dtAulaFim">Data:</label>
                            <input type="date" name="dtAulaFim" min={new Date().toLocaleDateString("sv-SE")} id="dtAulaFim" onChange={(e) => setDataFim(e.target.value)} required/>
                        </div>
                        <div className={style.inputGroup}>
                            <label htmlFor="horaFimAula">Horário:</label>
                            <input type="time" name="horaAula" id="horaFimAula" required onChange={(e) => setHoraFim(e.target.value)}/>
                        </div>
                    </div>
                    <div className={`${style.inputGroup} ${style.observacao}`}>
                        <label htmlFor="obsAula">Observação:</label>
                        <textarea name="obsAula" id="obsAula" onChange={(e) => setObservacao(e.target.value)}/>
                    </div>
                    <div className={style.btnGroup}>
                        <Button
                            textButton="Agendar aula"
                            styleButton="scheduleLessonButton"
                            disabled={false}
                            functionButton={postAula}
                            loadingText="Agendando aula..."
                        />

                        <Button
                            textButton="Voltar"
                            styleButton="backPageButton"
                            disabled={false}
                            functionButton={goBackPage}
                            loadingText="Voltando..."
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
    );
}