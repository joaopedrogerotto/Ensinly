import React, {useState } from "react";
import style from "./Registration.module.css";
import StudentRegistration from "./StudentRegistration/StudentRegistration";
import TeacherRegistration from "./TeacherRegistration/TeacherRegistration";

export default function Cadastro(){
    const [form, setForm] = useState<"A" | "B" | "">("");

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
                <img src="/Ensinly_Logo.png" alt="Ensinly Logo" />
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
                
            </main>
        </div>


    )
}