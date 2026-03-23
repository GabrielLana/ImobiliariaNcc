namespace ImobiliariaNcc.Application.Common.DTO;

public class VendedorDto
{
    public int Id { get; set; }
    public bool Ativo { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Email { get; set; }
    public string Celular { get; set; }
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public string Numero { get; set; }
    public string? Complemento { get; set; }
    public string Setor { get; set; }
    public int NumeroRegistro { get; set; }
}
