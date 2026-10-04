using CIEE_processo_seletivo_BACKEND.Data;
using CIEE_processo_seletivo_BACKEND.Models;
using CIEE_processo_seletivo_BACKEND.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UglyToad.PdfPig;



namespace CIEE_processo_seletivo_BACKEND.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CurriculosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly CurriculoParserService _parser;


    public CurriculosController(
        AppDbContext context,
        CurriculoParserService parser)
    {
        _context = context;
        _parser = parser;
    }

    [HttpGet]
    public async Task<ActionResult> GetCurriculos([FromQuery] int? id)
    {
        if (id.HasValue)
        {
            var curriculo = await _context.Curriculos.FindAsync(id.Value);

            if (curriculo == null)
            {
                return NotFound(new
                {
                    mensagem = $"Nenhum currículo encontrado com o ID {id.Value}."
                });
            }

            return Ok(curriculo);
        }

        var curriculos = await _context.Curriculos.ToListAsync();

        return Ok(curriculos);
    }

    [HttpPost]
    public async Task<ActionResult<Curriculo>> CadastrarCurriculo(Curriculo curriculo)
    {
        curriculo.DataCriacao = DateTime.Now;

        _context.Curriculos.Add(curriculo);

        await _context.SaveChangesAsync();

        return Created("", curriculo);
    }
    [HttpPost("ler-pdf")]
    public async Task<IActionResult> LerPdf(IFormFile arquivo)
    {
        const long tamanhoMaximo = 5 * 1024 * 1024;

        if (arquivo == null || arquivo.Length == 0)
        {
            return BadRequest("Nenhum arquivo foi enviado.");
        }

        if (arquivo.Length > tamanhoMaximo)
        {
            return BadRequest("O arquivo PDF deve ter no máximo 5 MB.");
        }

        if (arquivo.ContentType != "application/pdf")
        {
            return BadRequest("O arquivo precisa ser um PDF.");
        }

        try
        {
            using var memoryStream = new MemoryStream();

            await arquivo.CopyToAsync(memoryStream);

            memoryStream.Position = 0;

            using var documento = PdfDocument.Open(memoryStream);

            var textoCompleto = new System.Text.StringBuilder();

            foreach (var pagina in documento.GetPages())
            {
                var palavras = pagina.GetWords();

                textoCompleto.AppendLine(
                    string.Join(" ", palavras.Select(p => p.Text))
                );
            }

            var texto = textoCompleto.ToString();
            var email = _parser.ExtrairEmail(texto);
            var telefone = _parser.ExtrairTelefone(texto);
            var nome = _parser.ExtrairNome(texto);

            return Ok(new
            {
                nomeCompleto = nome,
                email = email,
                telefone = telefone
            });
        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                $"Erro ao processar o PDF: {ex.Message}"
            );
        }
    }
}

