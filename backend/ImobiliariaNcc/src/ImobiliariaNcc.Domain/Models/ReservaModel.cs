namespace ImobiliariaNcc.Domain.Models;

public sealed class ReservaModel : BaseModel
{
    public int IdCliente { get; private set; }

    public int IdApartamento { get; private set; }

    public bool Ativo { get; private set; }

    private ReservaModel() { }

    public ReservaModel(int clienteId, int apartamentoId)
    {
        IdCliente = clienteId;
        IdApartamento = apartamentoId;

        Ativo = true;
    }

    public void Atualizar(bool ativo, int idCliente, int idApartamento)
    {
        Ativo = ativo;
        IdCliente = idCliente;
        IdApartamento = idApartamento;
    }

    public void Desativar()
        => Ativo = false;
}
