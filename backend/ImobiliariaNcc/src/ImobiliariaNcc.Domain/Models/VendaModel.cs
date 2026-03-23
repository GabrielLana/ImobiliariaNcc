namespace ImobiliariaNcc.Domain.Models;

public sealed class VendaModel : BaseModel
{
    public int IdCliente { get; private set; }

    public int IdApartamento { get; private set; }

    public int IdVendedor { get; private set; }

    private VendaModel() { }

    public VendaModel(int idCliente, int idApartamento, int idVendedor)
    {
        IdCliente = idCliente;
        IdApartamento = idApartamento;
        IdVendedor = idVendedor;
    }

    public void Atualizar(int idCliente, int idApartamento, int idVendedor)
    {
        IdCliente = idCliente;
        IdApartamento = idApartamento;
        IdVendedor = idVendedor;
    }
}
