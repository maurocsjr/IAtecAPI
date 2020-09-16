using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IAtecAPI.Models
{
    [Table("pessoas")]
    public partial class Pessoas
    {
        public Pessoas()
        {
            Telefones = new HashSet<Telefones>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("nome", TypeName = "varchar(100)")]
        public string Nome { get; set; }
        [Required]
        [Column("sobrenome", TypeName = "varchar(250)")]
        public string Sobrenome { get; set; }
        [Column("data_nascimento", TypeName = "datetime")]
        public DateTime DataNascimento { get; set; }
        [Required]
        [Column("cpf", TypeName = "varchar(11)")]
        public string Cpf { get; set; }
        [Required]
        [Column("email", TypeName = "varchar(255)")]
        public string Email { get; set; }
        [Required]
        [Column("sexo", TypeName = "varchar(1)")]
        public string Sexo { get; set; }

        [InverseProperty("IdPessoaNavigation")]
        public virtual ICollection<Telefones> Telefones { get; set; }
    }
}
