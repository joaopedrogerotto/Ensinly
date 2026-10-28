import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import Login from "./pages/Login/Login";
import Home from "./pages/Home/Home";
import { AuthProvider } from "./Context/AuthContext";
import CadastroAluno from "./pages/Registration/StudentRegistration/StudentRegistration";
import Registration from "./pages/Registration/Registration";

export default function App() { 
    return(
        <>
            <BrowserRouter>
                <AuthProvider>
                    <Routes>
                        <Route path="/login" element={<Login/>}/>
                        <Route path="/home" element={<Home/>}/>
                        <Route path="/aluno/cadastrar" element={<CadastroAluno/>}/>
                        <Route path="/registration/registration" element={<Registration/>}/>
                        <Route path="*" element={<Navigate to="/login" replace />} />
                    </Routes>
                </AuthProvider>
            </BrowserRouter>
        </>

    )
}