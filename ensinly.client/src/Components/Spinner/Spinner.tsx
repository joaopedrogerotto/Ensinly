import style from "./Spinner.module.css";

interface SpinnerProps{
    loadingText: string;
}

export default function Spinner({loadingText}: SpinnerProps){
    return (
        <>
            <span className={style.loadingContent}>
                <span className={style.spinner}></span>
                <span>{loadingText}</span>
            </span>
        </>
    )
}