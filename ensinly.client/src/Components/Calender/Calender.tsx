import { useEffect, useState } from "react";
import style from "./Calender.module.css"
import { Link } from "react-router-dom";

interface Aluno{
    id: number;
    nome: string;
}

interface Professor{
    id: number;
    nome: string;
    email: string;
    descricao: string;
    status: boolean;
}

interface Aula{
    aluno: Aluno;
    status: string;
    observacao?: string;
    dataHoraInicio: string;
    dataHoraFim: string;
    professor: Professor;
}

export default function Calender(){
    const [currentDate, setCurrentDate] = useState(new Date());
    
    const [lessons, setLesson] = useState<Aula[]>(); 

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

    const lessonDates = lessons?.map(lesson =>{
        const date =  new Date(lesson.dataHoraFim)
        return new Date(
            date.getFullYear(),
            date.getMonth(),
            date.getDate()
        ).toISOString();
    } 
    ) ?? [];

    function changeMonth(value: number) {
        setCurrentDate(new Date(year, month + value, 1));
    }

    async function  getAulas() {
        const response = await fetch("/api/aula");
        
        if(!response.ok){
            const error = await response.json();
            throw new Error("Erro ao buscar aulas: " + error.message);
        }

        const data = await response.json();
        setLesson(data);
    }

    useEffect(() => {
        getAulas()
    }, []);
    
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

                    let todayLessons;
                    if(lessonDates?.includes(new Date(year, month, day!!).toISOString())){
                         todayLessons = lessons?.filter(lesson => {
                            const lessonDate = new Date(lesson.dataHoraInicio);

                            return (
                                lessonDate.getDate() === day &&
                                lessonDate.getMonth() === month &&
                                lessonDate.getFullYear() === year
                            );
                        });

                    }

                    return (
                        <>                        
                            <Link 
                                key={index} 
                                 to={`/professor/agenda/agendar-aula?day=${day}&month=${month+1}&year=${year}`}
                                className={`${style.day} ${isToday ? style.today : ""} ${isPast ? style.past : ""}`}>
                                <span className={style.dayNumber}>{day}</span>
                                <div className={style.lessonsList}>
                                    {todayLessons?.map((lesson, lessonIndex) => {
                                        const date = new Date(lesson.dataHoraInicio);

                                        const lessonHour = String(date.getHours()).padStart(2, "0");
                                        const lessonMinutes = String(date.getMinutes()).padStart(2, "0");

                                        return (
                                            <div key={lessonIndex} className={style.lessonCard}>
                                                <span>{lesson.aluno.nome}</span>
                                                <span>{lessonHour}:{lessonMinutes}</span>
                                            </div>
                                        );
                                    })}
                                </div>
                            </Link>
                        </>

                    )
                })}
            </div>
        </div>
    );
}