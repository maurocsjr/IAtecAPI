using IAtecAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAtecAPI.Context
{
    public class PessoaContext : DbContext
    {
        public PessoaContext(DbContextOptions<PessoaContext> options) : base(options)
        {
        }
        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
