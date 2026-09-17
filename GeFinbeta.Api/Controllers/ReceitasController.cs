using GeFinbeta.Api.DTOs;
using GeFinbeta.Data;
using GeFinbeta.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeFinbeta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceitasController : ControllerBase
    {
        private readonly GefinContext _context;

        public ReceitasController(GefinContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetReceitas()
        {
            var receitas = await _context.Receitas
                .OrderByDescending(r => r.Data)
                .ToListAsync();

            return Ok(receitas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReceitaPorId(int id)
        {
            var receita = await _context.Receitas.FindAsync(id);

            if (receita == null)
            {
                return NotFound("Receita não encontrada.");
            }

            return Ok(receita);
        }

        [HttpPost]
        public async Task<IActionResult> CriarReceita(CriarReceitaDTO dto)
        {
            var receita = new Receita
            {
                Descricao = dto.Descricao,
                Valor = dto.Valor,
                Data = dto.Data
            };

            _context.Receitas.Add(receita);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetReceitaPorId),
                new { id = receita.Id },
                receita
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarReceita(
            int id,
            CriarReceitaDTO dto)
        {
            var receita = await _context.Receitas.FindAsync(id);

            if (receita == null)
            {
                return NotFound("Receita não encontrada.");
            }

            receita.Descricao = dto.Descricao;
            receita.Valor = dto.Valor;
            receita.Data = dto.Data;

            await _context.SaveChangesAsync();

            return Ok(receita);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirReceita(int id)
        {
            var receita = await _context.Receitas.FindAsync(id);

            if (receita == null)
            {
                return NotFound("Receita não encontrada.");
            }

            _context.Receitas.Remove(receita);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}