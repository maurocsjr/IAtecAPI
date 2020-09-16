using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IAtecAPI.Models
{
    public partial class IatecContext : DbContext
    {
        public IatecContext()
        {
        }

        public IatecContext(DbContextOptions<IatecContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pessoas> Pessoas { get; set; }
        public virtual DbSet<Telefones> Telefones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoas>(entity =>
            {
                entity.Property(e => e.Cpf)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Email)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Nome)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Sexo)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Sobrenome)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Telefones>(entity =>
            {
                entity.HasIndex(e => e.IdPessoa)
                    .HasName("id_pessoa_telefones_fk_idx");

                entity.Property(e => e.Telefone)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdPessoaNavigation)
                    .WithMany(p => p.Telefones)
                    .HasForeignKey(d => d.IdPessoa)
                    .HasConstraintName("id_pessoa_telefones_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
