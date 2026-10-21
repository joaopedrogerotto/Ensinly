import { FaHome, FaSchool, FaStickyNote } from "react-icons/fa";
import style from "./Navbar.module.css"

export default function Navbar(){
    return(
        <nav className={style.navbar}>
            <div className={style.brand}>
                <img src="/Ensinly_Logo.png" alt="Ensily Logo" />
                <span>Ensily</span>
            </div>
            <ul>
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
            </ul>
        </nav>
    );
}