namespace ImobiliariaNcc.Domain.Models;

public sealed class ClienteModel : BaseModel
{
    public string Nome { get; private set; }

    public string Cpf { get; private set; }

    public DateTime DataNascimento { get; private set; }

    public string Email { get; private set; }

    public string Celular { get; private set; }

    public string EstadoCivil { get; private set; }

    public bool Ativo { get; private set; }

    private ClienteModel() { }

    public ClienteModel(
        string nome,
        string cpf,
        DateTime dataNascimento,
        string email,
        string celular,
        string estadoCivil)
    {
        Nome = nome;
        Cpf = cpf;
        DataNascimento = dataNascimento;
        Email = email;
        Celular = celular;
        EstadoCivil = estadoCivil;

        Ativo = true;
    }

    public void Atualizar(
        string nome,
        string cpf,
        DateTime dataNascimento,
        string email,
        string celular,
        string estadoCivil,
        bool ativo)
    {
        Nome = nome;
        Cpf = cpf;
        DataNascimento = dataNascimento;
        Email = email;
        Celular = celular;
        EstadoCivil = estadoCivil;
        Ativo = ativo;
    }
    public void Desativar()
        => Ativo = false;
}
