namespace CIEE_processo_seletivo_BACKEND.Models;

public class Curriculo
{
    public int Id { get; set; }

    public string NomeCompleto { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? Telefone { get; set; }

    public string? AreaInteresse { get; set; }

    public string? ResumoProfissional { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataAtualizacao { get; set; }
}