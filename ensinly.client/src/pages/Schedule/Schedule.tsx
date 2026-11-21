import Calender from "../../Components/Calender/Calender";
import Navbar from "../../Components/NavBar/Navbar";
import style from  "./Schedule.module.css";

export default function Schedule(){
    return(
        <>
            <Navbar/>
            <div className={style.container}>
                <Calender/>
            </div>
        </>
    )
}