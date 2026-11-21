import { useState } from "react";
import style from "./Calender.module.css"

export default function Calender(){
    const [currentDate, setCurrentDate] = useState(new Date());

    const year = currentDate.getFullYear();
    const month = currentDate.getMonth();

    const firstDayMonth = new Date(year, month,1).getDay();
    const lastDayMonth = new Date(year, month + 1, 0).getDate();
    
    const daysList = [];

    for(let i = 0; i < firstDayMonth; i++){
        daysList.push(null);
    }

    for(let day = 1; day <= lastDayMonth; day++){
        daysList.push(day);
    }

    const monthName = currentDate.toLocaleDateString("pt-BR", {
        month: "long",
        year: "numeric"
    });

    function changeMonth(value: number) {
        setCurrentDate(new Date(year, month + value, 1));
    }
    
    return(
        <div className={style.calendar}>
            <header className={style.header}>
                <button onClick={() => changeMonth(-1)}>❮</button>

                <h2>{monthName}</h2>

                <button onClick={() =>  changeMonth(+1)}>❯</button>
            </header>

            <div className={style.grid}>
                {["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb"].map(day =>(
                    <div key={day} className={style.weekday}>
                        {day}
                    </div>
                ))}

                {daysList.map((day, index) => {
                    const today = new Date();
                    
                    const isToday =
                        day === today.getDate() &&
                        month === today.getMonth() &&
                        year === today.getFullYear();

                    const isPast = day !== null &&
                        new Date(year, month, day) < new Date(today.getFullYear(), today.getMonth(), today.getDate());
                    return (
                        <div key={index} className={`${style.day} ${isToday ? style.today : ""} ${isPast ? style.past : ""}`}>
                            {day}
                        </div>
                    )
                })}
            </div>
        </div>
    );
}