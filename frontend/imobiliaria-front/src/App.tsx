import { BrowserRouter, Routes, Route } from "react-router-dom"

import Login from "./pages/Login"
import Dashboard from "./pages/Dashboard"
import Clientes from "./pages/Clientes"
import Apartamentos from "./pages/Apartamentos"
import Reservas from "./pages/Reservas"
import Vendas from "./pages/Vendas"

import Layout from "./components/Layout"
import ProtectedRoute from "./routes/ProtectedRoute"

function App() {

  return (

    <BrowserRouter>

      <Routes>
        <Route path="/" element={<Login />} />

        <Route element={
          <ProtectedRoute>
            <Layout />
          </ProtectedRoute>
        }>

          <Route path="/dashboard" element={<Dashboard />} />
          <Route path="/clientes" element={<Clientes />} />
          <Route path="/apartamentos" element={<Apartamentos />} />
          <Route path="/reservas" element={<Reservas />} />
          <Route path="/vendas" element={<Vendas />} />

        </Route>

      </Routes>

    </BrowserRouter>

  )
}

export default App