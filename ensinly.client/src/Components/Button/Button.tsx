import style from "./Button.module.css"

interface ButtonProps{
    functionButton: () => void;
    textButton: string;
    styleButton: string;
}

export default function Button({functionButton, textButton, styleButton} : ButtonProps){
    return(
        <>
            <button onClick={() => functionButton()} className={style[styleButton]}>{textButton}</button>
        </>
    )
}