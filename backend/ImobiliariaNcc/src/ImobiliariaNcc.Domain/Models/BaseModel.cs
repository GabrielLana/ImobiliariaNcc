namespace ImobiliariaNcc.Domain.Models;

public class BaseModel
{
    public int Id { get; protected set; }

    public DateTime DataCriacao { get; protected set; }

    public DateTime? DataAlteracao { get; protected set; }

    public void AtualizarDataAlteracao()
    {
        DataAlteracao = DateTime.UtcNow;
    }
}
