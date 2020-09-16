using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IAtecAPI.Models;

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

        // GET: api/Pessoas
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

        // GET: api/Pessoas/5
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

        // PUT: api/Pessoas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoas(int id, Pessoas pessoas)
        {
            if (id != pessoas.Id)
            {
                return BadRequest();
            }

            _context.Entry(pessoas).State = EntityState.Modified;

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

        // POST: api/Pessoas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Pessoas>> PostPessoas(Pessoas pessoas)
        {
            _context.Pessoas.Add(pessoas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPessoas", new { id = pessoas.Id }, pessoas);
        }

        // DELETE: api/Pessoas/5
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
