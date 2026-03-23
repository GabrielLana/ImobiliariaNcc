import { Link } from "react-router-dom"

export default function Dashboard() {
    const nome = localStorage.getItem("user");

    function logout() {
        localStorage.removeItem("token")
        localStorage.removeItem("user")

        window.location.href = "/"
    }

    return (

        <div className="p-10">

            <h1 className="text-2xl font-bold mb-6">
                Dashboard
            </h1>

            <p className="mb-6">
                Bem vindo, <b>{nome}</b>
            </p>

            <div className="flex gap-4">

                <Link
                    className="bg-blue-500 text-white px-4 py-2 rounded"
                    to="/clientes"
                >
                    Clientes
                </Link>

                <Link
                    className="bg-green-500 text-white px-4 py-2 rounded"
                    to="/apartamentos"
                >
                    Apartamentos
                </Link>

                <Link
                    className="bg-purple-500 text-white px-4 py-2 rounded"
                    to="/reservas"
                >
                    Reservas
                </Link>

                <Link
                    className="bg-orange-500 text-white px-4 py-2 rounded"
                    to="/vendas"
                >
                    Vendas
                </Link>

            </div>

            <div className="float-right">
                <button
                    onClick={logout}
                    className="bg-red-500 text-white px-4 py-2 rounded mb-6"
                >
                    Sair
                </button>
            </div>

        </div>
    )
}