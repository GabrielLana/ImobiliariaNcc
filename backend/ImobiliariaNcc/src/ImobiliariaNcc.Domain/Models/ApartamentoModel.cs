using ImobiliariaNcc.Domain.Exceptions;

namespace ImobiliariaNcc.Domain.Models;

public sealed class ApartamentoModel : BaseModel
{
    public bool Ocupado { get; private set; }

    public int Metragem { get; private set; }

    public int Quartos { get; private set; }

    public int Banheiros { get; private set; }

    public int Vagas { get; private set; }

    public string DetalhesApartamento { get; private set; }

    public string DetalhesCondominio { get; private set; }

    public int Andar { get; private set; }

    public int Bloco { get; private set; }

    public decimal ValorVenda { get; private set; }

    public decimal ValorCondominio { get; private set; }

    public decimal ValorIptu { get; private set; }

    public string Cep { get; private set; }

    public string Logradouro { get; private set; }

    public string Bairro { get; private set; }

    public string Numero { get; private set; }

    public string? Complemento { get; private set; }

    public string Estado { get; set; }

    public string Cidade { get; set; }

    private ApartamentoModel() { }

    public ApartamentoModel(int metragem, int quartos, int banheiros, int vagas, string detalhesApartamento, string detalhesCondominio, 
        int andar, int bloco, decimal valorVenda, decimal valorCondominio, decimal valorIptu, string cep, string logradouro, string bairro,
        string numero, string estado, string cidade, string? complemento)
    {
        Metragem = metragem;
        Quartos = quartos;
        Banheiros = banheiros;
        Vagas = vagas;
        DetalhesApartamento = detalhesApartamento;
        DetalhesCondominio = detalhesCondominio;
        Andar = andar;
        Bloco = bloco;
        ValorVenda = valorVenda;
        ValorCondominio = valorCondominio;
        ValorIptu = valorIptu;
        Cep = cep;
        Logradouro = logradouro;
        Bairro = bairro;
        Numero = numero;
        Complemento = complemento;
        Estado = estado;
        Cidade = cidade;

        Ocupado = false;
    }

    public void Atualizar(bool ocupado, int metragem, int quartos, int banheiros, int vagas, string detalhesApartamento, string detalhesCondominio,
        int andar, int bloco, decimal valorVenda, decimal valorCondominio, decimal valorIptu, string cep, string logradouro, string bairro,
        string numero, string estado, string cidade, string? complemento)
    {
        Ocupado = ocupado;
        Metragem = metragem;
        Quartos = quartos;
        Banheiros = banheiros;
        Vagas = vagas;
        DetalhesApartamento = detalhesApartamento;
        DetalhesCondominio = detalhesCondominio;
        Andar = andar;
        Bloco = bloco;
        ValorVenda = valorVenda;
        ValorCondominio = valorCondominio;
        ValorIptu = valorIptu;
        Cep = cep;
        Logradouro = logradouro;
        Bairro = bairro;
        Numero = numero;
        Complemento = complemento;
        Estado = estado;
        Cidade = cidade;
    }

    public bool EstaDisponivel()
        => !Ocupado;

    public void MarcarComoOcupado()
    {
        if (Ocupado)
            throw new BadRequestException("Apartamento já está ocupado.");

        Ocupado = true;
    }
}
