
import { useEffect, useState } from "react";
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

            const msg = await response.json();

            if (response.ok) {
                setUser(msg);
                setResponseToken("Seu convite foi aceito com sucesso!");
                setSuccess(true);
            } else {
                setResponseToken(
                    typeof msg === "string"
                        ? msg
                        : msg.message || "Não foi possível validar seu convite."
                );
            }
        } catch {
            setResponseToken("Não foi possível conectar ao servidor. Tente novamente.");
        } finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        if (!token) {
            setResponseToken("Token não informado ou inválido.");
            setLoading(false);
            return;
        }

        validateToken();
    }, []);

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
