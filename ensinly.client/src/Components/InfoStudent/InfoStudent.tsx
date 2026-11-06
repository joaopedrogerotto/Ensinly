import { useEffect, useState } from "react";
import { useAuth } from "../../Context/AuthContext";
import Button from "../Button/Button";
import style from "./InfoStudent.module.css";
import InformationalModal from "../InformationalModal/InformationalModal";

interface AlunoResponse{
    idUsuario: number;
    nome: string;
    email: string;
    observacao: string;
    telefone: string;
    dataNascimento: string;
    nivelAprendizado: string;
    status: boolean;
};

interface InfoStudentProps{
    alunoResponse?: AlunoResponse
}

interface ConviteRequest{
    IdProfessorUsuario: number;
    IdAlunoUsuario: number;
    EmailDestinatario: string;
}

export default function InfoStudent({alunoResponse}:InfoStudentProps){
    const {user} = useAuth();

    const conviteRequest: ConviteRequest = {
        IdProfessorUsuario: Number(user?.id),
        IdAlunoUsuario: Number(alunoResponse?.idUsuario), 
        EmailDestinatario: alunoResponse?.email ?? ""
    };

    //Modal
    const [showModal, setShowModal] = useState(false);
    const [typeModal, setTypeModal] = useState("");
    const [titleModal, setTitleModal] = useState("");
    const [textModal, setTextModal] = useState("");

    async function generateInvite(){
        const response = await fetch("/api/convite/aluno-professor-vinculo",{
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            credentials: "include",
            body: JSON.stringify(
                conviteRequest
            )
        });

         if (response.ok) {
            setTypeModal("success");
            setTitleModal("Convite enviado!");
            setTextModal("O convite foi enviado com sucesso para o email do aluno.");
            setShowModal(true);
        } else {
            setTypeModal("error");
            setTitleModal("Erro ao enviar convite");
            setTextModal("Não foi possível enviar o convite. Tente novamente.");
            setShowModal(true);
        }
    }

    function handleCloseModal() {
        setShowModal(false);
    }


    return(
        <>
            <div className={style.formGroup}>
                <div className={style.inputGroup}>
                    <label>Nome:</label>
                    <input type="text" name="nomeAluno" id="nomeAluno" value={alunoResponse?.nome} readOnly/>
                </div>
                <div className={style.inputGroup}>
                    <label>Email:</label>
                    <input type="email" name="emailAluno" id="emailAluno" value={alunoResponse?.email} readOnly/>
                </div>
                <div className={style.inputGroup}>
                    <label>Telefone:</label>
                    <input type="text" name="telefoneAluno" id="telefoneAluno" value={alunoResponse?.telefone} readOnly/>
                </div><div className={style.inputGroup}>
                    <label>Data Nascimento:</label>
                    <input type="date" name="dtNascimentoAluno" id="dtNascimentoAluno" value={alunoResponse?.dataNascimento.split("T")[0]} readOnly/>
                </div><div className={style.inputGroup}>
                    <label>Nivel de Aprendizado:</label>
                    <input type="text" name="nvlAprendizado" id="nvlAprendizado" value={alunoResponse?.nivelAprendizado} readOnly/>
                </div><div className={style.inputGroup}>
                    <label>Observação Aluno:</label>
                    <input type="text" name="obsAluno" id="obsAluno" value={alunoResponse?.observacao} readOnly/>
                </div>
                <Button 
                    functionButton={generateInvite}
                    textButton="Convidar"
                    styleButton="buttonBlueForm"    
                />
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