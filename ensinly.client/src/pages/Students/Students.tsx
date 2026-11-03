import MyStudents from "../../Components/MyStudents/MyStudents";
import Navbar from "../../Components/NavBar/Navbar";
import SreachBar from "../../Components/SreachBar/SreachBar";
import style from "./Students.module.css";

export default function Students(){
    return (
        <>
            <Navbar/>
            <div className={style.container}>
                <MyStudents/>
                <SreachBar classStyleName="sreachBarStudents" labelText="Pesquisar e-mail aluno" placeholder="exemplo@email.com"/>
            </div>
        </>
    )
}