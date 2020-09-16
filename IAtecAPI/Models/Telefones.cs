using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IAtecAPI.Models
{
    [Table("telefones")]
    public partial class Telefones
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("id_pessoa")]
        public int IdPessoa { get; set; }
        [Column("telefone", TypeName = "varchar(11)")]
        public string Telefone { get; set; }

        [ForeignKey(nameof(IdPessoa))]
        [InverseProperty(nameof(Pessoas.Telefones))]
        public virtual Pessoas IdPessoaNavigation { get; set; }
    }
}
