import { createBrowserRouter } from "react-router-dom"
import Login from "../pages/Login"
import Dashboard from "../pages/Dashboard"
import Clientes from "../pages/Clientes"
import Layout from "../components/Layout"
import Apartamentos from "../pages/Apartamentos"
import Reservas from "../pages/Reservas"
import Vendas from "../pages/Vendas"

export const router = createBrowserRouter([

    {
        path: "/",
        element: <Login />
    },

    {
        element: <Layout />,
        children: [

            {
                path: "/dashboard",
                element: <Dashboard />
            },

            {
                path: "/clientes",
                element: <Clientes />
            },

            {
                path: "/apartamentos",
                element: <Apartamentos />
            },

            {
                path: "/reservas",
                element: <Reservas />
            },

            {
                path: "/vendas",
                element: <Vendas />
            }
        ]

    }

])