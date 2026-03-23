import { useEffect, useState } from "react"
import { api } from "../api/api"

export default function Vendas() {

    const [vendas, setVendas] = useState([])
    const [clientes, setClientes] = useState<any[]>([])
    const [apartamentos, setApartamentos] = useState<any[]>([])
    const [vendedores, setVendedores] = useState<any[]>([])

    useEffect(() => {

        load()

    }, [])

    async function load() {

        const vendasRes = await api.get("/vendas")
        const clientesRes = await api.get("/clientes")
        const apRes = await api.get("/apartamentos")
        const vendedoresRes = await api.get("/vendedores")

        setVendas(vendasRes.data)
        setClientes(clientesRes.data)
        setApartamentos(apRes.data)
        setVendedores(vendedoresRes.data)
    }

    return (

        <div>

            <h1 className="text-2xl font-bold mb-6">
                Vendas
            </h1>

            <table className="w-full bg-white shadow rounded">

                <thead className="bg-gray-200">

                    <tr>

                        <th className="p-3">Id</th>
                        <th className="p-3">Cliente</th>
                        <th className="p-3">Apartamento</th>
                        <th className="p-3">Vendedor</th>

                    </tr>

                </thead>

                <tbody>

                    {vendas.map((v: any) => {

                        const cliente = clientes.find((c: any) => c.id === v.idCliente)
                        const ap = apartamentos.find((a: any) => a.id === v.idApartamento)
                        const vendedor = vendedores.find((ven: any) => ven.id === v.idVendedor)

                        return (

                            <tr key={v.id} className="border-t">

                                <td className="p-3">{v.id}</td>

                                <td className="p-3">
                                    {cliente?.nome} ({cliente?.cpf})
                                </td>

                                <td className="p-3">
                                    AP {ap?.id} - {ap?.cidade}
                                </td>

                                <td className="p-3">
                                    {vendedor
                                        ? `${vendedor.nome} (${vendedor.numeroRegistro})`
                                        : `ID ${v.idVendedor}`
                                    }
                                </td>

                            </tr>

                        )

                    })}

                </tbody>

            </table>

        </div>

    )

}