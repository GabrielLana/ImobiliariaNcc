namespace ImobiliariaNcc.Application.Common.DTO;

public class ApartamentoDto
{
    public int Id { get; set; }
    public bool Ocupado { get; set; }

    public int Metragem { get; set; }

    public int Quartos { get; set; }

    public int Banheiros { get; set; }

    public int Vagas { get; set; }

    public string DetalhesApartamento { get; set; }

    public string DetalhesCondominio { get; set; }

    public int Andar { get; set; }

    public int Bloco { get; set; }

    public decimal ValorVenda { get; set; }

    public decimal ValorCondominio { get; set; }

    public decimal ValorIptu { get; set; }

    public string Cep { get; set; }

    public string Logradouro { get; set; }

    public string Bairro { get; set; }

    public string Numero { get; set; }

    public string Estado { get; set; }

    public string Cidade { get; set; }

    public string? Complemento { get; set; }
}