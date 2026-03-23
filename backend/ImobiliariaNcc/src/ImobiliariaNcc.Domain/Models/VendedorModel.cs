namespace ImobiliariaNcc.Domain.Models;

public sealed class VendedorModel : BaseModel
{
    public string Nome { get; private set; }

    public string Cpf { get; private set; }

    public string Senha { get; private set; }

    public DateTime DataNascimento { get; private set; }

    public string Email { get; private set; }

    public string Celular { get; private set; }

    public string Cep { get; private set; }

    public string Logradouro { get; private set; }

    public string Bairro { get; private set; }

    public string Numero { get; private set; }

    public string? Complemento { get; private set; }

    public string Setor { get; private set; }

    public int NumeroRegistro { get; private set; }

    public bool Ativo { get; private set; }

    private VendedorModel() { }

    public VendedorModel(
        string nome,
        string cpf,
        string senha,
        DateTime dataNascimento,
        string email,
        string celular,
        string cep,
        string logradouro,
        string bairro,
        string numero,
        string? complemento,
        string setor,
        int numeroRegistro)
    {
        Nome = nome;
        Cpf = cpf;
        Senha = senha;
        DataNascimento = dataNascimento;
        Email = email;
        Celular = celular;
        Cep = cep;
        Logradouro = logradouro;
        Bairro = bairro;
        Numero = numero;
        Complemento = complemento;
        Setor = setor;
        NumeroRegistro = numeroRegistro;

        Ativo = true;
    }

    public void Atualizar(
        string nome,
        string cpf,
        DateTime dataNascimento,
        string email,
        string celular,
        string cep,
        string logradouro,
        string bairro,
        string numero,
        string? complemento,
        string setor,
        int numeroRegistro,
        bool ativo
        )
    {
        Nome = nome;
        Cpf = cpf;
        DataNascimento = dataNascimento;
        Email = email;
        Celular = celular;
        Cep = cep;
        Logradouro = logradouro;
        Bairro = bairro;
        Numero = numero;
        Complemento = complemento;
        Setor = setor;
        NumeroRegistro = numeroRegistro;
        Ativo = ativo;
    }

    public void AtualizaSenha(string senha)
    {
        Senha = senha;
    }
}
