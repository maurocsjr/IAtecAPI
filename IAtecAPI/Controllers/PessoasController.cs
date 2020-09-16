using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IAtecAPI.Models;
using System.Security.Cryptography.X509Certificates;

namespace IAtecAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly IatecContext _context;

        public PessoasController(IatecContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoas>>> GetPessoas()
        {
            return await _context.Pessoas.ToListAsync();
        }

        [HttpGet("details")]
        public async Task<ActionResult<IEnumerable<Pessoas>>> GetPessoasDetails()
        {
            return await _context.Pessoas
                 .AsNoTracking()
                 .AsQueryable()
                 .Include(m => m.Telefones)
                 .ToListAsync();
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<Pessoas>> GetPessoasDetails(int id)
        {
            var pessoas = await _context.Pessoas.Include(m => m.Telefones).Where(pub => pub.Id == id).FirstOrDefaultAsync();

            if (pessoas == null)
            {
                return NotFound();
            }

            return pessoas;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoas>> GetPessoas(int id)
        {
            var pessoas = await _context.Pessoas.FindAsync(id);

            if (pessoas == null)
            {
                return NotFound();
            }

            return pessoas;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoas(int id, Pessoas pessoas)
        {
            if (id != pessoas.Id)
            {
                return BadRequest();
            }


            var pessoa = _context.Pessoas
                .Where(p => p.Id == pessoas.Id)
                .Include(p => p.Telefones)
                .SingleOrDefault();

            if (pessoa != null)
            {
                // Update parent
                _context.Entry(pessoa).CurrentValues.SetValues(pessoas);

                // Delete children
                foreach (var existingChild in pessoa.Telefones.ToList())
                {
                    if (!pessoas.Telefones.Any(c => c.Id == existingChild.Id))
                        _context.Telefones.Remove(existingChild);
                }

                foreach (var childModel in pessoas.Telefones)
                {
                    var existingChild = pessoa.Telefones
                        .Where(c => c.Id == childModel.Id)
                        .SingleOrDefault();

                    if (existingChild != null)
                        // Update child
                        _context.Entry(existingChild).CurrentValues.SetValues(childModel);
                    else
                    {
                        // Insert child
                        var newChild = new Telefones
                        {
                            IdPessoa = childModel.IdPessoa,
                            IdPessoaNavigation = childModel.IdPessoaNavigation,
                            Telefone = childModel.Telefone
                        };
                        pessoa.Telefones.Add(newChild);
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Pessoas>> PostPessoas(Pessoas pessoas)
        {
            _context.Pessoas.Add(pessoas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPessoas", new { id = pessoas.Id }, pessoas);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Pessoas>> DeletePessoas(int id)
        {
            var pessoas = await _context.Pessoas.FindAsync(id);
            if (pessoas == null)
            {
                return NotFound();
            }

            _context.Pessoas.Remove(pessoas);
            await _context.SaveChangesAsync();

            return pessoas;
        }

        private bool PessoasExists(int id)
        {
            return _context.Pessoas.Any(e => e.Id == id);
        }
    }
}
