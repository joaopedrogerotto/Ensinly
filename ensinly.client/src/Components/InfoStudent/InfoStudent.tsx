import { useEffect, useState } from "react";
import { useAuth } from "../../Context/AuthContext";
import Button from "../Button/Button";
import style from "./InfoStudent.module.css";
import InformationalModal from "../InformationalModal/InformationalModal";

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

interface InfoStudentProps{
    alunoResponse?: AlunoResponse
}

interface ConviteRequest{
    IdProfessorUsuario: number;
    IdAluno: number;
    EmailDestinatario: string;
}

export default function InfoStudent({alunoResponse}:InfoStudentProps){
    const {user} = useAuth();

    const [isLoading, setIsLoading] = useState(false);
    const [vinculo, setVinculo] = useState("");

    const conviteRequest: ConviteRequest = {
        IdProfessorUsuario: Number(user?.id),
        IdAluno: Number(alunoResponse?.id), 
        EmailDestinatario: alunoResponse?.email ?? ""
    };

    //Modal
    const [showModal, setShowModal] = useState(false);
    const [typeModal, setTypeModal] = useState("");
    const [titleModal, setTitleModal] = useState("");
    const [textModal, setTextModal] = useState("");

    async function generateInvite(){
        if(isLoading){
            return;
        }

        setIsLoading(true);
        
        try{
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
                setVinculo("PENDENTE");
            } else {
                const data = await response.json();
                setTypeModal("error");
                setTitleModal("Erro ao enviar convite");
                setTextModal(data.message);
                setShowModal(true);
            }

            } catch (error) {
            setTypeModal("error");
            setTitleModal("Erro de conexão");
            setTextModal("Não foi possível conectar ao servidor.");
            setShowModal(true);

        } finally {
            setIsLoading(false);
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
                {
                   (vinculo !== "SEM VINCULO" || alunoResponse?.vinculoStatus !== "SEM VINCULO") &&
                    (
                        <Button 
                            functionButton={generateInvite}
                            textButton={(vinculo === "PENDENTE" || alunoResponse?.vinculoStatus === "PENDENTE") ? "Convite Pendente":"Convidar"}
                            styleButton="buttonBlueForm"
                            isLoading={isLoading}
                            loadingText="Enviando..."
                            disabled={(vinculo  === "PENDENTE" || alunoResponse?.vinculoStatus === "PENDENTE")}
                        />
                    )
                }
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