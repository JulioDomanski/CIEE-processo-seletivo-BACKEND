using System.ComponentModel.DataAnnotations;

namespace CIEE_processo_seletivo_BACKEND.Models;

public class Curriculo
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome completo é obrigatório.")]
    [MaxLength(90, ErrorMessage = "O nome completo deve ter no máximo 90 caracteres.")]
    public string NomeCompleto { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O e-mail informado é inválido.")]
    public string Email { get; set; } = string.Empty;

    public string? Telefone { get; set; }

    public string? AreaInteresse { get; set; }

    public string? ResumoProfissional { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataAtualizacao { get; set; }
}