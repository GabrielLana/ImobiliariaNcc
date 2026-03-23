import { useEffect, useState } from "react"
import { api } from "../api/api"
import { useNavigate } from "react-router-dom"

export default function Apartamentos() {
    const navigate = useNavigate()

    const [apartamentos, setApartamentos] = useState<any[]>([])

    const [editingId, setEditingId] = useState<number | null>(null)

    const [metragem, setMetragem] = useState("")
    const [quartos, setQuartos] = useState("")
    const [banheiros, setBanheiros] = useState("")
    const [vagas, setVagas] = useState("")
    const [andar, setAndar] = useState("")
    const [bloco, setBloco] = useState("")
    const [valorVenda, setValorVenda] = useState("")
    const [cidade, setCidade] = useState("")
    const [bairro, setBairro] = useState("")
    const [logradouro, setLogradouro] = useState("")
    const [numero, setNumero] = useState("")
    const [estado, setEstado] = useState("")
    const [cep, setCep] = useState("")
    const [detalhesApartamento, setDetalhesApartamento] = useState("")
    const [detalhesCondominio, setDetalhesCondominio] = useState("")
    const [valorCondominio, setValorCondominio] = useState("")
    const [valorIptu, setValorIptu] = useState("")

    async function loadApartamentos() {

        const response = await api.get("/apartamentos/disponiveis")

        setApartamentos(response.data)
    }

    function editApartamento(a: any) {

        setEditingId(a.id)

        setMetragem(a.metragem)
        setQuartos(a.quartos)
        setBanheiros(a.banheiros)
        setVagas(a.vagas)
        setAndar(a.andar)
        setBloco(a.bloco)

        setValorVenda(a.valorVenda)
        setValorCondominio(a.valorCondominio)
        setValorIptu(a.valorIptu)

        setCidade(a.cidade)
        setEstado(a.estado)
        setBairro(a.bairro)
        setLogradouro(a.logradouro)
        setNumero(a.numero)
        setCep(a.cep)

        setDetalhesApartamento(a.detalhesApartamento)
        setDetalhesCondominio(a.detalhesCondominio)
    }

    async function saveApartamento(e: any) {

        e.preventDefault()

        const payload = {

            id: editingId,

            metragem,
            quartos,
            banheiros,
            vagas,
            andar,
            bloco,

            valorVenda,
            valorCondominio,
            valorIptu,

            cidade,
            estado,
            bairro,
            logradouro,
            numero,
            cep,

            detalhesApartamento,
            detalhesCondominio,

            ocupado: false
        }

        if (editingId)
            await api.put("/apartamentos", payload)
        else
            await api.post("/apartamentos", payload)

        setEditingId(null)

        loadApartamentos()
    }

    function criarReserva(apartamento: any) {

        navigate("/reservas", {
            state: {
                idApartamento: apartamento.id
            }
        })

    }

    useEffect(() => {

        loadApartamentos()

    }, [])

    return (

        <div>

            <h1 className="text-2xl font-bold mb-6">
                Apartamentos
            </h1>

            <form
                onSubmit={saveApartamento}
                className="grid grid-cols-4 gap-3 mb-10"
            >

                <input className="border p-2" placeholder="Metragem"
                    value={metragem}
                    onChange={e => setMetragem(e.target.value)}
                />

                <input className="border p-2" placeholder="Quartos"
                    value={quartos}
                    onChange={e => setQuartos(e.target.value)}
                />

                <input className="border p-2" placeholder="Banheiros"
                    value={banheiros}
                    onChange={e => setBanheiros(e.target.value)}
                />

                <input className="border p-2" placeholder="Vagas"
                    value={vagas}
                    onChange={e => setVagas(e.target.value)}
                />

                <input className="border p-2" placeholder="Andar"
                    value={andar}
                    onChange={e => setAndar(e.target.value)}
                />

                <input className="border p-2" placeholder="Bloco"
                    value={bloco}
                    onChange={e => setBloco(e.target.value)}
                />

                <input className="border p-2" placeholder="Valor Venda"
                    value={valorVenda}
                    onChange={e => setValorVenda(e.target.value)}
                />

                <input className="border p-2" placeholder="Cidade"
                    value={cidade}
                    onChange={e => setCidade(e.target.value)}
                />

                <input className="border p-2" placeholder="Estado"
                    value={estado}
                    onChange={e => setEstado(e.target.value)}
                />

                <input className="border p-2" placeholder="Bairro"
                    value={bairro}
                    onChange={e => setBairro(e.target.value)}
                />

                <input className="border p-2" placeholder="Logradouro"
                    value={logradouro}
                    onChange={e => setLogradouro(e.target.value)}
                />

                <input className="border p-2" placeholder="Número"
                    value={numero}
                    onChange={e => setNumero(e.target.value)}
                />

                <button className="bg-blue-500 text-white p-2 rounded col-span-4">
                    {editingId ? "Salvar Alterações" : "Cadastrar Apartamento"}
                </button>

            </form>

            <table className="w-full bg-white shadow rounded">

                <thead className="bg-gray-200">

                    <tr>

                        <th className="p-3"></th>
                        <th className="p-3 text-left">ID</th>
                        <th className="p-3 text-left">Metragem</th>
                        <th className="p-3 text-left">Quartos</th>
                        <th className="p-3 text-left">Andar</th>
                        <th className="p-3 text-left">Cidade</th>
                        <th className="p-3 text-left">Valor</th>
                        <th className="p-3 text-left">Ações</th>

                    </tr>

                </thead>

                <tbody>

                    {apartamentos.map((a: any) => (

                        <tr key={a.id} className="border-t">

                            <td className="p-3 text-red-600 font-bold text-lg">
                                {a.reservado ? "RESERVADO" : ""}
                            </td>

                            <td className="p-3">{a.id}</td>
                            <td className="p-3">{a.metragem}</td>
                            <td className="p-3">{a.quartos}</td>
                            <td className="p-3">{a.andar}</td>
                            <td className="p-3">{a.cidade}</td>
                            <td className="p-3">{a.valorVenda}</td>

                            <td className="p-3 flex gap-2">

                                <button
                                    className="bg-blue-500 text-white px-3 py-1 rounded"
                                    onClick={() => editApartamento(a)}
                                >
                                    Editar
                                </button>

                                {!a.reservado && (

                                    <button
                                        className="bg-green-600 text-white px-3 py-1 rounded"
                                        onClick={() => criarReserva(a)}
                                    >
                                        Criar Reserva
                                    </button>

                                )}

                            </td>

                        </tr>

                    ))}

                </tbody>

            </table>

        </div>
    )
}