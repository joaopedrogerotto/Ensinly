import React, { Component, useState } from "react";
import style from "./Registration.module.css";
import StudentRegistration from "./StudentRegistration/StudentRegistration";

export default function Cadastro(){
    const [form, setForm] = useState<"A" | "">("");

    const typesForm = {
        A: StudentRegistration
    }

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) =>{
        if(event.target.value === "student"){
            setForm('A')
        }else if(event.target.value === "teacher"){
            setForm('');//mudar para professor
        }
    }

    const FormComponent = form ? typesForm[form] : null;

    return(
        <main className={style.main}>
            <h1>Registro</h1>
            <p>Qual seu tipo de registro?</p>
            <div className={style.groupInputRadio} onChange={handleChange}>
                <div className={style.inputRadio}>
                    <input type="radio" name="typeRegister" id="typeStudent" value="student"/>
                    <label>Aluno</label>
                </div>
                <div className={style.inputRadio}>
                    <input type="radio" name="typeRegister" id="typeRegister" value="teacher"/>
                    <label>Professor</label>
                </div>
            </div>
            
            {FormComponent && <FormComponent />}
        </main>


    )
}