import style from "./InfoStudent.module.css";

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

interface InfoStudentProps{
    alunoResponse?: AlunoResponse
}


export default function InfoStudent({alunoResponse}:InfoStudentProps){
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
            </div>
        </>
    );
}