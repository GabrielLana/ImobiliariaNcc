import { useEffect, useState } from "react"
import { api } from "../api/api"
import { useLocation, useNavigate } from "react-router-dom"
import { getUserFromToken } from "../utils/auth"

export default function Reservas() {

    const location = useLocation()
    const navigator = useNavigate()

    const [reservas, setReservas] = useState<any[]>([])
    const [clientes, setClientes] = useState<any[]>([])
    const [apartamentos, setApartamentos] = useState<any[]>([])

    const [editingId, setEditingId] = useState<number | null>(null)

    const [idCliente, setIdCliente] = useState("")
    const [idApartamento, setIdApartamento] = useState("")
    const [ativo, setAtivo] = useState(true)

    useEffect(() => {

        loadReservas()
        loadClientes()
        loadApartamentos()

        if (location.state?.idApartamento)
            setIdApartamento(location.state.idApartamento)

    }, [])

    async function loadReservas() {

        const res = await api.get("/reservas")

        setReservas(res.data)
    }

    async function loadClientes() {

        const res = await api.get("/clientes")

        setClientes(res.data)
    }

    async function loadApartamentos() {

        const res = await api.get("/apartamentos")

        setApartamentos(res.data)
    }

    function editReserva(reserva: any) {

        setEditingId(reserva.id)

        setIdCliente(reserva.idCliente)
        setIdApartamento(reserva.idApartamento)
        setAtivo(reserva.ativo)
    }

    function clearForm() {

        setEditingId(null)
        setIdCliente("")
        setIdApartamento("")
        setAtivo(true)
    }

    async function saveReserva(e: any) {

        e.preventDefault()

        const payload = {

            id: editingId,
            idCliente: Number(idCliente),
            idApartamento: Number(idApartamento),
            ativo
        }

        if (editingId)
            await api.put("/reservas", payload)
        else
            await api.post("/reservas", payload)

        clearForm()

        loadReservas()
    }

    async function confirmarPagamento(reserva: any) {

        const confirmar = confirm("Pagamento da entrada foi confirmado?")

        if (!confirmar) return

        alert("Pagamento confirmado!")
        debugger;
        const user = getUserFromToken()

        await api.post("/vendas", {
            idCliente: reserva.idCliente,
            idApartamento: reserva.idApartamento,
            idVendedor: Number(user?.id)
        })

        navigator("/vendas")
    }

    return (

        <div>

            <h1 className="text-2xl font-bold mb-6">
                Reservas
            </h1>

            <form
                onSubmit={saveReserva}
                className="grid grid-cols-3 gap-3 mb-8"
            >

                <select
                    className="border p-2"
                    value={idCliente}
                    onChange={e => setIdCliente(e.target.value)}
                >

                    <option value="">Selecione Cliente</option>

                    {clientes.map((c: any) => (

                        <option key={c.id} value={c.id}>
                            {c.nome} - {c.cpf}
                        </option>

                    ))}

                </select>

                <select
                    className="border p-2"
                    value={idApartamento}
                    onChange={e => setIdApartamento(e.target.value)}
                >

                    <option value="">Selecione Apartamento</option>

                    {apartamentos.map((a: any) => (

                        <option key={a.id} value={a.id}>
                            AP {a.id} - {a.cidade} - {a.andar}
                        </option>

                    ))}

                </select>

                {editingId && (

                    <label className="flex items-center gap-2">
                        <input
                            type="checkbox"
                            checked={ativo}
                            onChange={e => setAtivo(e.target.checked)}
                        />
                        Ativo
                    </label>

                )}

                <button className="bg-blue-500 text-white p-2 rounded col-span-3">

                    {editingId ? "Salvar Alterações" : "Criar Reserva"}

                </button>

            </form>

            <table className="w-full bg-white shadow rounded">

                <thead className="bg-gray-200">

                    <tr>

                        <th className="p-3 text-left">Id</th>
                        <th className="p-3 text-left">Cliente</th>
                        <th className="p-3 text-left">Apartamento</th>
                        <th className="p-3 text-left">Ativo</th>
                        <th className="p-3 text-left">Ações</th>

                    </tr>

                </thead>

                <tbody>

                    {reservas.map((r: any) => {

                        const cliente = clientes.find(c => c.id === r.idCliente)
                        const ap = apartamentos.find(a => a.id === r.idApartamento)

                        return (

                            <tr key={r.id} className="border-t">

                                <td className="p-3">{r.id}</td>

                                <td className="p-3">
                                    {cliente?.nome} ({cliente?.cpf})
                                </td>

                                <td className="p-3">
                                    AP {ap?.id} - {ap?.cidade}
                                </td>

                                <td className="p-3">
                                    {r.ativo ? "Sim" : "Não"}
                                </td>

                                <td className="p-3">

                                    <button
                                        className="bg-blue-500 text-white px-3 py-1 rounded"
                                        onClick={() => editReserva(r)}
                                    >
                                        Editar
                                    </button>
                                    {r.ativo && (

                                        <button
                                            className="bg-green-600 text-white px-3 py-1 rounded"
                                            onClick={() => confirmarPagamento(r)}
                                        >
                                            Verificar Pagamento
                                        </button>

                                    )}
                                </td>

                            </tr>

                        )

                    })}

                </tbody>

            </table>

        </div>
    )
}