import Button from "../Button/Button";
import style from "./SreachBar.module.css"

interface SreachBarProps{
    labelText: string
    placeholder:string,
    classStyleName: string,
    textButton: string,
    functionSreach: () => void,
    functionSetAttribute: (termSreach: string) => void
}

export default function SreachBar({labelText, placeholder, classStyleName, textButton, functionSreach, functionSetAttribute}: SreachBarProps){
    return(
        <div className={`${style.sreachBar} ${style[classStyleName]}`}> 
            <label htmlFor={classStyleName}>{labelText}</label>
            <input type="text" id={classStyleName} placeholder={placeholder} className={style.input} onChange={(e) => functionSetAttribute(e.target.value)}/>
            <Button functionButton={functionSreach} textButton={textButton} styleButton="buttonBlue"/>
        </div>
    );
}