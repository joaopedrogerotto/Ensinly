import style from "./SreachBar.module.css"

interface SreachBarProps{
    labelText: string
    placeholder:string,
    classStyleName: string,

}

export default function SreachBar({labelText, placeholder, classStyleName}: SreachBarProps){
    return(
        <div className={`${style.sreachBar} ${style[classStyleName]}`}> 
            <label htmlFor={classStyleName}>{labelText}</label>
            <input type="text" id={classStyleName} placeholder={placeholder} className={style.input}/>
        </div>
    );
}