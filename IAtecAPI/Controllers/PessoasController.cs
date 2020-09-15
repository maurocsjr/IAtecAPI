using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IAtecAPI.Context;
using IAtecAPI.Models;
using Microsoft.AspNetCore.Cors;

namespace IAtecAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors()]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly PessoaContext _context;

        public PessoasController(PessoaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> GetPessoas()
        {
            //Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return await _context.Pessoas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetPessoa(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return pessoa;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoa(int id, [FromBody] Pessoa pessoa)
        {

            if (id != pessoa.id)
            {
                return BadRequest();
            }

            _context.Entry(pessoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(id))
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
        public async Task<ActionResult<Pessoa>> PostPessoa(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPessoa", new { id = pessoa.id }, pessoa);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Pessoa>> DeletePessoa(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();

            return pessoa;
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoas.Any(e => e.id == id);
        }
    }
}
