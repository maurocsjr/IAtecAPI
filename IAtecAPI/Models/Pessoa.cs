using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace IAtecAPI.Models
{
    public class Pessoa
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public DateTime data_nascimento { get; set; }
        public string cpf { get; set; }
        public string telwhats { get; set; }
        public string telcel { get; set; }
        public string telfixo { get; set; }
        public string telcom { get; set; }
        public string email { get; set; }
        public char sexo { get; set; }
    }
}
