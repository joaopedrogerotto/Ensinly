
import { useEffect, useState, useRef, useReducer } from "react";
import { useSearchParams } from "react-router-dom";
import Navbar from "../../../Components/NavBar/Navbar";
import { useAuth } from "../../../Context/AuthContext";
import style from "./Accept.module.css";

export default function Accept() {
    const [searchParams] = useSearchParams();
    const token = searchParams.get("token");

    const [responseToken, setResponseToken] = useState("");
    const [loading, setLoading] = useState(true);
    const [success, setSuccess] = useState(false);

    const { setUser } = useAuth();

    const validationStarted = useRef(false);

    async function validateToken() {
        try {
            const response = await fetch("/api/token/validar", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                credentials: "include",
                body: JSON.stringify(token)
            });

            if (response.status === 401) {
                const msg = await response.text();

                setResponseToken(msg || "Convite inválido ou expirado.");
                return;
            }

            if (response.ok) {
                const usuario = await response.json();
                setUser(usuario);
                setResponseToken("Seu convite foi aceito com sucesso!");
                setSuccess(true);
            } 
        } catch {
            setResponseToken("Não foi possível conectar ao servidor. Tente novamente.");
        } finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        if (validationStarted.current) return;

        validationStarted.current = true;

        if (!token) {
            setResponseToken("Token não informado ou inválido.");
            setLoading(false);
            return;
        }

        validateToken();
    }, [token]);

    return (
        <>
            <Navbar />

            <div className={style.acceptContainer}>
                <div className={`${style.acceptCard} ${success ? style.success : style.error}`}>

                    {loading ? (
                        <>
                            <div className={style.acceptSpinner}></div>
                            <h2>Validando convite...</h2>
                            <p>Aguarde enquanto verificamos seu convite.</p>
                        </>
                    ) : (
                        <>
                            <div className={style.acceptIcon}>
                                {success ? "✓" : "!"}
                            </div>

                            <h2>
                                {success ? "Convite aceito!" : "Ops! Algo deu errado"}
                            </h2>

                            <p>{responseToken}</p>
                        </>
                    )}

                </div>
            </div>
        </>
    );
}
