import { useState } from "react"
import { api } from "../api/api"
import { useNavigate } from "react-router-dom"
import { useEffect } from "react"

export default function Login() {

    const navigate = useNavigate()

    const [cpf, setCpf] = useState("")
    const [senha, setSenha] = useState("")

    useEffect(() => {

        const token = localStorage.getItem("token")

        if (token) {
            navigate("/dashboard")
        }

    }, [])

    async function handleLogin(e: any) {
        e.preventDefault()

        const response = await api.post("/login", {
            cpf,
            senha
        })

        localStorage.setItem("token", response.data.token)
        localStorage.setItem("user", response.data.nome)

        navigate("/dashboard")
    }

    return (
        <div className="h-screen flex items-center justify-center bg-gray-100">

            <form
                onSubmit={handleLogin}
                className="bg-white p-8 rounded shadow w-80"
            >

                <h2 className="text-xl font-bold mb-4">
                    Login
                </h2>

                <input
                    className="border p-2 w-full mb-3"
                    placeholder="CPF"
                    value={cpf}
                    onChange={e => setCpf(e.target.value)}
                />

                <input
                    type="password"
                    className="border p-2 w-full mb-4"
                    placeholder="Senha"
                    value={senha}
                    onChange={e => setSenha(e.target.value)}
                />

                <button
                    className="bg-blue-500 text-white w-full py-2 rounded"
                >
                    Entrar
                </button>

            </form>

        </div>
    )
}