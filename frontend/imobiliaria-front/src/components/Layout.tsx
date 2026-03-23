import { Link, Outlet } from "react-router-dom"

export default function Layout() {

    const nome = localStorage.getItem("user")

    function logout() {
        localStorage.clear()
        window.location.href = "/"
    }

    return (

        <div className="flex h-screen">

            {/* sidebar */}

            <div className="w-60 bg-gray-900 text-white p-6 flex flex-col">

                <h2 className="text-xl font-bold mb-8">
                    Imobiliária
                </h2>

                <nav className="flex flex-col gap-3">

                    <Link to="/dashboard" className="hover:text-gray-300">
                        Dashboard
                    </Link>

                    <Link to="/clientes" className="hover:text-gray-300">
                        Clientes
                    </Link>

                    <Link to="/apartamentos" className="hover:text-gray-300">
                        Apartamentos
                    </Link>

                    <Link to="/reservas" className="hover:text-gray-300">
                        Reservas
                    </Link>

                    <Link to="/vendas" className="hover:text-gray-300">
                        Vendas
                    </Link>
                </nav>

                <div className="mt-auto text-sm">

                    <div>
                        Logado como
                    </div>

                    <div className="font-bold mb-4">
                        {nome}
                    </div>

                    <button
                        onClick={logout}
                        className="bg-red-500 px-3 py-1 rounded w-full"
                    >
                        Sair
                    </button>

                </div>

            </div>

            {/* content */}

            <div className="flex-1 bg-gray-100 p-10 overflow-auto">
                <Outlet />
            </div>

        </div>
    )
}