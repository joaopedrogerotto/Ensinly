import React, {useState } from "react";
import style from "./Registration.module.css";
import StudentRegistration from "./StudentRegistration/StudentRegistration";
import TeacherRegistration from "./TeacherRegistration/TeacherRegistration";
import { useNavigate } from "react-router-dom";

export default function Cadastro(){
    const [form, setForm] = useState<"A" | "B" | "">("");

    const navigate = useNavigate();

    const typesForm = {
        A: StudentRegistration,
        B: TeacherRegistration
    }

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) =>{
        if(event.target.value === "student"){
            setForm('A')
        }else if(event.target.value === "teacher"){
            setForm('B');        
        }
    }

    const FormComponent = form ? typesForm[form] : null;

    return(
        <div className={style.page}>
            <main className={style.main}>
                <header className={style.brand}>
                    <img src="/Ensinly_Logo.png" alt="Ensinly Logo" />
                </header>

                <div className={style.content}>
                    <button type="button" className={style.backButton} aria-label="Voltar para seleção de cadastro" onClick={() => navigate("/login")}>
                        &#8592; Voltar
                    </button>

                    <h1>Registro</h1>
                    
                    <fieldset className={style.fieldset}>
                        <legend>Qual seu tipo de registro?</legend>
                        <div className={style.groupInputRadio} onChange={handleChange}>
                            <div className={style.inputRadio}>
                                <label className={style.option}>
                                    <input type="radio" name="typeRegister" id="typeStudent" value="student"/>
                                    <span>Aluno</span>
                            </label>
                                
                            </div>
                            <div className={style.inputRadio}>
                                <label className={style.option}>
                                    <input type="radio" name="typeRegister" id="typeRegister" value="teacher"/>
                                    <span>Professor</span>
                                </label>
                                
                            </div>
                        </div>
                    </fieldset>
                    {FormComponent && (
                        <div className={style.modalAnimation} key={form}>
                            <FormComponent />
                        </div>
                    )}
                </div>
            </main>
        </div>


    )
}