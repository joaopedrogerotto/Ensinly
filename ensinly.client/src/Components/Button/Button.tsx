import Spinner from "../Spinner/Spinner";
import style from "./Button.module.css"

interface ButtonProps{
    functionButton: () => void;
    textButton: string;
    styleButton: string;
    isLoading?: boolean;
    loadingText?: string;
}

export default function Button({functionButton, textButton, styleButton, isLoading = false, loadingText = "Carregando..."} : ButtonProps){
    return(
        <>
            <button onClick={() => functionButton()} className={style[styleButton]} disabled={isLoading}>
                {isLoading ? 
                    (
                        <Spinner
                            loadingText={loadingText}
                        />
                    ) : (
                        textButton
                    )
                }
            </button>
        </>
    )
}