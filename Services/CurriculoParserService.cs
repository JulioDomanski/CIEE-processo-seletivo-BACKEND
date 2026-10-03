using System.Text.RegularExpressions;

namespace CIEE_processo_seletivo_BACKEND.Services;

public class CurriculoParserService
{
    public string? ExtrairEmail(string texto)
    {
        var match = Regex.Match(
            texto,
            @"[\w\.-]+@[\w\.-]+\.\w+"
        );

        return match.Success ? match.Value : null;
    }

    public string? ExtrairTelefone(string texto)
    {
        var regex = new Regex(
            @"(?:\+?55\s*)?" +
            @"(?:\(?(" +
            @"1[1-9]|" +
            @"2[12478]|" +
            @"3[1-5]|3[78]|" +
            @"4[1-9]|" +
            @"5[1345]|" +
            @"6[1-9]|" +
            @"7[134579]|" +
            @"8[1-9]|" +
            @"9[1-9]" +
            @")\)?\s*)" +
            @"(?:9\d{4}|\d{4})[-\s]?\d{4}"
        );

        var match = regex.Match(texto);

        return match.Success ? match.Value.Trim() : null;
    }

    public string? ExtrairNome(string texto)
    {
        var emailMatch = Regex.Match(
            texto,
            @"[\w\.-]+@[\w\.-]+\.\w+"
        );

        var telefoneMatch = Regex.Match(
            texto,
            @"(?:\+?55\s*)?(?:\(?\d{2}\)?\s*)?(?:9\d{4}|\d{4})[-\s]?\d{4}"
        );

        int fim = texto.Length;

        if (emailMatch.Success)
            fim = Math.Min(fim, emailMatch.Index);

        if (telefoneMatch.Success)
            fim = Math.Min(fim, telefoneMatch.Index);

        var inicio = texto[..fim];

        inicio = Regex.Replace(inicio, @"^[^\p{L}]+", "");
        inicio = Regex.Replace(inicio, @"\s+", " ").Trim();

        var regexNome = new Regex(
            @"\b[\p{L}][\p{L}'’-]+" +
            @"(?:\s+(?:(?:da|de|do|das|dos|e)\s+)?[\p{L}][\p{L}'’-]+){1,5}\b",
            RegexOptions.IgnoreCase
        );

        var match = regexNome.Match(inicio);

        return match.Success
            ? match.Value.Trim()
            : null;
    }
}