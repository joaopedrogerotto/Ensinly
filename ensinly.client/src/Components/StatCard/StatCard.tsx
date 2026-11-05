import type { IconType } from "react-icons";
import style from "./StatCard.module.css"

interface StatCardProps{
    titleStatCard: string;
    IconStatReact: IconType;
    amountStatCard: string;
    classNameStatCard: string;
    descriptionStatCard?: string,

};

export default function StatCard({titleStatCard, IconStatReact, amountStatCard, classNameStatCard, descriptionStatCard} : StatCardProps){
    return(
        <>
            <div className={style[classNameStatCard]}>
                <IconStatReact/>
                <p className={style.titleCard}>{titleStatCard}</p>
                <p className={style.amoutCard}>{amountStatCard}</p>
                {
                    descriptionStatCard 
                    && 
                    <p className={style.descriptionCard}>{descriptionStatCard}</p>
                }
            </div>
        </>
    );
}