import { Link } from "react-router-dom";
import Calender from "../../Components/Calender/Calender";
import Navbar from "../../Components/NavBar/Navbar";
import style from "./Schedule.module.css";

export default function Schedule(){
    return(
        <>
            <Navbar/>
            <div className={style.container}>
                <div className={style.header}>
                    <Link to="/professor/agenda/agendar-aula" className={style.btnAgendar}>
                        + Agendar aula
                    </Link>
                </div>

                <Calender/>
            </div>
        </>
    )
}