import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import Login from "./pages/Login/Login";
import Home from "./pages/Home/Home";
import { AuthProvider } from "./Context/AuthContext";
import CadastroAluno from "./pages/Registration/StudentRegistration/StudentRegistration";
import Registration from "./pages/Registration/Registration";
import Students from "./pages/Students/Students";
import Accept from "./pages/Invite/Accept/Accept";
import Schedule from "./pages/Schedule/Schedule";
import ProtectedRoute from "./Routes/ProtectedRoute";

export default function App() { 
    return(
        <>
            <BrowserRouter>
                <AuthProvider>
                    <Routes>
                        <Route path="/login" element={<Login/>}/>
                        <Route path="/aluno/cadastrar" element={<CadastroAluno/>}/>
                        <Route path="/registration/registration" element={<Registration/>}/>
                        <Route path="/convite/aceitar" element={<Accept/>} />
                        <Route path="/professor/agenda" element={<Schedule/>} />

                        <Route element={<ProtectedRoute/>}>
                            <Route path="/home" element={<Home/>}/>
                            <Route path="/professor/alunos" element={<Students/>}/>
                        </Route>
                        
                        <Route path="*" element={<Navigate to="/login" replace />} />
                        
                    </Routes>
                </AuthProvider>
            </BrowserRouter>
        </>

    )
}