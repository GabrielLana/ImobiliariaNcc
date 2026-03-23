import { useEffect, useState } from "react"
import { api } from "../api/api"

export default function Clientes() {

    const [clientes, setClientes] = useState<any[]>([])

    const [nome, setNome] = useState("")
    const [cpf, setCpf] = useState("")
    const [dataNascimento, setDataNascimento] = useState("")
    const [email, setEmail] = useState("")
    const [celular, setCelular] = useState("")
    const [estadoCivil, setEstadoCivil] = useState("")
    const [ativo, setAtivo] = useState(true)

    const [editingId, setEditingId] = useState<number | null>(null)

    async function loadClientes() {
        const response = await api.get("/clientes")
        setClientes(response.data)
    }

    function editCliente(cliente: any) {

        setEditingId(cliente.id)

        setNome(cliente.nome)
        setCpf(cliente.cpf)
        setEmail(cliente.email)
        setCelular(cliente.celular)
        setEstadoCivil(cliente.estadoCivil)
        setAtivo(cliente.ativo)

        if (cliente.dataNascimento)
            setDataNascimento(cliente.dataNascimento.split("T")[0])
    }

    function clearForm() {

        setEditingId(null)

        setNome("")
        setCpf("")
        setDataNascimento("")
        setEmail("")
        setCelular("")
        setEstadoCivil("")
        setAtivo(true)
    }

    async function saveCliente(e: any) {

        e.preventDefault()

        const payload = {
            id: editingId,
            nome,
            cpf,
            dataNascimento,
            email,
            celular,
            estadoCivil,
            ativo
        }

        if (editingId)
            await api.put("/clientes", payload)
        else
            await api.post("/clientes", payload)

        clearForm()
        loadClientes()
    }

    async function deleteCliente(id: number) {

        if (!confirm("Deseja deletar este cliente?"))
            return

        await api.delete(`/clientes/${id}`)

        loadClientes()
    }

    useEffect(() => {
        loadClientes()
    }, [])

    return (

        <div>

            <h1 className="text-2xl font-bold mb-6">
                Clientes
            </h1>

            <form
                onSubmit={saveCliente}
                className="grid grid-cols-3 gap-3 mb-8"
            >

                <input className="border p-2" placeholder="Nome"
                    value={nome}
                    onChange={e => setNome(e.target.value)}
                />

                <input className="border p-2" placeholder="CPF"
                    value={cpf}
                    onChange={e => setCpf(e.target.value)}
                />

                <input type="date" className="border p-2"
                    value={dataNascimento}
                    onChange={e => setDataNascimento(e.target.value)}
                />

                <input className="border p-2" placeholder="Email"
                    value={email}
                    onChange={e => setEmail(e.target.value)}
                />

                <input className="border p-2" placeholder="Celular"
                    value={celular}
                    onChange={e => setCelular(e.target.value)}
                />

                <input className="border p-2" placeholder="Estado Civil"
                    value={estadoCivil}
                    onChange={e => setEstadoCivil(e.target.value)}
                />

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
                    {editingId ? "Salvar Alterações" : "Cadastrar Cliente"}
                </button>

                {editingId && (
                    <button
                        type="button"
                        onClick={clearForm}
                        className="bg-gray-500 text-white p-2 rounded col-span-3"
                    >
                        Cancelar edição
                    </button>
                )}

            </form>

            <table className="w-full bg-white shadow rounded">

                <thead className="bg-gray-200">

                    <tr>
                        <th className="p-3 text-left">ID</th>
                        <th className="p-3 text-left">Nome</th>
                        <th className="p-3 text-left">CPF</th>
                        <th className="p-3 text-left">Email</th>
                        <th className="p-3 text-left">Celular</th>
                        <th className="p-3 text-left">Ativo</th>
                        <th className="p-3 text-left">Ações</th>
                    </tr>

                </thead>

                <tbody>

                    {clientes.map((c: any) => (

                        <tr key={c.id} className="border-t">
                            
                            <td className="p-3">{c.id}</td>
                            <td className="p-3">{c.nome}</td>
                            <td className="p-3">{c.cpf}</td>
                            <td className="p-3">{c.email}</td>
                            <td className="p-3">{c.celular}</td>
                            <td className="p-3">{c.ativo ? "Sim" : "Não"}</td>

                            <td className="p-3 flex gap-2">

                                <button
                                    className="bg-blue-500 text-white px-3 py-1 rounded"
                                    onClick={() => editCliente(c)}
                                >
                                    Editar
                                </button>

                                <button
                                    className="bg-red-500 text-white px-3 py-1 rounded"
                                    onClick={() => deleteCliente(c.id)}
                                >
                                    Deletar
                                </button>

                            </td>

                        </tr>

                    ))}

                </tbody>

            </table>

        </div>
    )
}