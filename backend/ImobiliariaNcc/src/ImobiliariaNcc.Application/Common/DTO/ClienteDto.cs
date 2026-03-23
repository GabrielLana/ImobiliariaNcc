namespace ImobiliariaNcc.Application.Common.DTO;

public class ClienteDto
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public string Cpf { get; set; }

    public DateTime DataNascimento { get; set; }

    public string Email { get; set; }

    public string Celular { get; set; }

    public string EstadoCivil { get; set; }

    public bool Ativo { get; set; }
}