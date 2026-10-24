import { FaCalendar, FaHome, FaSchool, FaStickyNote, FaUser, FaUserGraduate, FaVideo } from "react-icons/fa";
import style from "./Navbar.module.css"
import { useAuth } from "../../Context/AuthContext";

export default function Navbar(){
    const {user} = useAuth();

    let menu;

    if(user?.tipoUsuario === "ALUNO"){
        menu = (
            <>
                <li className={style.active}>
                    <FaHome />
                    Home
                </li>
                <li>
                    <FaSchool />
                    Atividades
                </li>
                <li>
                    <FaVideo/>
                    Aula
                </li>
            </>
        );
    }else if(user?.tipoUsuario === "PROFESSOR"){
        menu = (
            <>
                <li className={style.active}>
                    <FaHome />
                    Home
                </li>
                <li>
                    <FaSchool />
                    Atividades
                </li>
                <li>
                    <FaStickyNote/>
                    Anotações
                </li>
                <li>
                    <FaCalendar/>
                    Agenda
                </li>
                <li>
                    <FaVideo/>
                    Aula
                </li>
                <li>
                    <FaUserGraduate/>
                    Alunos
                </li>
            </>
        );
    }

    return(
        <nav className={style.navbar}>
            <div className={style.brand}>
                <img src="/Ensinly_Logo.png" alt="Ensily Logo" />
                <span>Ensily</span>
            </div>

            <ul>
                {menu}
            </ul>
            <div className={style.brandBotton}>
                <FaUser/>
                <span>Meu Perfil</span>
            </div>
        </nav>
    );
}