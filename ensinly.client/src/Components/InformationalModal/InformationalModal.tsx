import style from "./InformationalModal.module.css"

interface InformationalModalProps{
    typeModal: string;
    titleModal: string;
    textModal: string;
    onClose: () => void;
}

export default function InformationalModal({typeModal, titleModal, textModal, onClose}: InformationalModalProps){
    return (
        <div className={style.overlay}>
            <div className={style[typeModal]}>
                <div className={style.headerModal}>
                    <div className={style.titleModal}>
                        <p>{titleModal}</p>
                    </div>
                </div>

                <div className={style.bodyModal}>
                    <div className={style.textModal}>
                        <p>{textModal}</p>
                    </div>

                    <button onClick={onClose} className={style.buttonModal}>
                        Fechar
                    </button>
                </div>
            </div>
        </div>
    );
}