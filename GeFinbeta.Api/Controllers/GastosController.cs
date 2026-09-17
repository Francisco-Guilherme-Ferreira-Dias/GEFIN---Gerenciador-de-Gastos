using GeFinbeta.Data;
using GeFinbeta.Api.DTOs;
using GeFinbeta.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeFinbeta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GastosController : ControllerBase
    {
        private readonly GefinContext _context;

        public GastosController(GefinContext context)
        {
            _context = context;
        }


        // GET: api/Gastos
        [HttpGet]
        public async Task<IActionResult> GetGastos()
        {
            var gastos = await _context.Gastos.ToListAsync();

            return Ok(gastos);
        }


        // GET: api/Gastos/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGastoPorId(int id)
        {
            var gasto = await _context.Gastos.FindAsync(id);

            if (gasto == null)
            {
                return NotFound("Gasto não encontrado.");
            }

            return Ok(gasto);
        }

        [HttpGet("resumo")]
        public async Task<IActionResult> GetResumoMensal(int mes, int ano)
        {
            if (mes < 1 || mes > 12)
            {
                return BadRequest("O mês deve estar entre 1 e 12.");
            }

            if (ano < 2000 || ano > DateTime.Now.Year)
            {
                return BadRequest($"O ano deve estar entre 2000 e {DateTime.Now.Year}.");
            }

            // Busca os gastos do mês
            var gastosDoMes = await _context.Gastos
                .Where(g => g.Data.Month == mes && g.Data.Year == ano)
                .ToListAsync();

            // Busca as receitas do mês
            var receitasDoMes = await _context.Receitas
                .Where(r => r.Data.Month == mes && r.Data.Year == ano)
                .ToListAsync();

            // Calcula os totais
            var totalGasto = gastosDoMes.Sum(g => g.Valor);

            var totalReceitas = receitasDoMes.Sum(r => r.Valor);

            // Saldo = dinheiro que entrou - dinheiro que saiu
            var saldo = totalReceitas - totalGasto;

            string? maiorCategoria = null;
            decimal valorMaiorCategoria = 0;

            // Só calcula a maior categoria se existirem gastos
            if (gastosDoMes.Count > 0)
            {
                var categoriaComMaiorGasto = gastosDoMes
                    .GroupBy(g => g.Categoria)
                    .Select(grupo => new
                    {
                        Categoria = grupo.Key,
                        Total = grupo.Sum(g => g.Valor)
                    })
                    .OrderByDescending(x => x.Total)
                    .First();

                maiorCategoria = categoriaComMaiorGasto.Categoria.ToString();
                valorMaiorCategoria = categoriaComMaiorGasto.Total;
            }

            var resumo = new ResumoMensalDTO
            {
                Mes = mes,
                Ano = ano,

                TotalGasto = totalGasto,
                QuantidadeGastos = gastosDoMes.Count,

                TotalReceitas = totalReceitas,
                QuantidadeReceitas = receitasDoMes.Count,

                Saldo = saldo,

                MaiorCategoria = maiorCategoria,
                ValorMaiorCategoria = valorMaiorCategoria
            };

            return Ok(resumo);
        }

        [HttpGet("por-categoria")]
        public async Task<IActionResult> GetGastosPorCategoria(int mes, int ano)
        {
            if (mes < 1 || mes > 12)
            {
                return BadRequest("O mês deve estar entre 1 e 12.");
            }

            if (ano < 2000 || ano > DateTime.Now.Year)
            {
                return BadRequest($"O ano deve estar entre 2000 e {DateTime.Now.Year}.");
            }

            var gastosPorCategoria = await _context.Gastos
                .Where(g => g.Data.Month == mes && g.Data.Year == ano)
                .GroupBy(g => g.Categoria)
                .Select(grupo => new GastoPorCategoriaDTO
                {
                    Categoria = grupo.Key.ToString(),
                    Total = grupo.Sum(g => g.Valor)
                })
                .ToListAsync();

            gastosPorCategoria = gastosPorCategoria
                .OrderByDescending(x => x.Total)
                .ToList();

            return Ok(gastosPorCategoria);
        }

        [HttpPost]
        public async Task<IActionResult> CriarGasto(CriarGastoDto dto)
        {
            var gasto = new Gastos
            {
                Descricao = dto.Descricao,
                Valor = dto.Valor,
                Data = dto.Data,
                Categoria = dto.Categoria
            };

            _context.Gastos.Add(gasto);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetGastoPorId),
                new { id = gasto.Id },
                gasto
            );
        }


        // PUT: api/Gastos/1
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarGasto(
            int id,
            Gastos gastoAtualizado)
        {
            var gasto = await _context.Gastos.FindAsync(id);

            if (gasto == null)
            {
                return NotFound("Gasto não encontrado.");
            }

            gasto.Descricao = gastoAtualizado.Descricao;
            gasto.Valor = gastoAtualizado.Valor;
            gasto.Data = gastoAtualizado.Data;
            gasto.Categoria = gastoAtualizado.Categoria;

            await _context.SaveChangesAsync();

            return Ok(gasto);
        }


        // DELETE: api/Gastos/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirGasto(int id)
        {
            var gasto = await _context.Gastos.FindAsync(id);

            if (gasto == null)
            {
                return NotFound("Gasto não encontrado.");
            }

            _context.Gastos.Remove(gasto);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}