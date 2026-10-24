import { createContext, useContext, useEffect, useState, type ReactNode } from "react";

export interface Usuario{
    id: string;
    nome: string;
    email: string;
    tipoUsuario: "ALUNO" | "PROFESSOR";
};

interface AuthContextType{
    user: Usuario | null;
    loading: boolean;
    setUser:  (u:Usuario | null) => void;
}

export const AuthContext = createContext<AuthContextType | null>(null);

interface AuthProvidadesProps{
    children: ReactNode;
};

export function AuthProvider({children}: AuthProvidadesProps){
    const [user, setUser] = useState<Usuario|null>(null);
    const [loading, setLoading] = useState(true);


    useEffect(() =>{
        fetch("api/me",{credentials: "include"})
            .then((r) => (r.ok ? r.json() : null))
            .then(setUser)
            .catch(() => setUser(null))
            .finally(() => setLoading(false))
    },[]);

    return (
        <AuthContext.Provider value={{user,loading,setUser}}>
            {children}
        </AuthContext.Provider>
    )
}


export function useAuth() {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error("useAuth deve ser usado dentro de AuthProvider");
  return ctx;
}