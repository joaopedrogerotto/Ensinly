import { useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom";

export default function Accept(){
    const [sreachParams, setSreachParams] = useSearchParams();

    const token = sreachParams.get('token');
    
    const [responseToken, setResponseToken] = useState("");
    const [user, setUsuario] = useState();

    async function validateToken() {
        const response = await fetch("/api/token/validar",{
            method:  "POST",
            headers:{
                "Content-Type" : "application/json",
            },
            credentials: "include",
            body: JSON.stringify(
                token
            ) 
        });

        if(response.ok){
            const usuario = await response.json();
            setUsuario(usuario);
            setResponseToken("Convite aceito");
        }else if(response.status === 401){
            const msg = await response.json();
            setResponseToken(msg);
        }else{
            const msg = await response.json();
            setResponseToken(msg);
        }
    }

    useEffect(() => {
        validateToken()
    },[]);
    
    return(
        <> 
            <p>{responseToken}</p>
        </>
    );
}