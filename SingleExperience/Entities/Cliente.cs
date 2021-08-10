using System;
using System.ComponentModel.DataAnnotations;

namespace SingleExperience.Entities
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
    }
}
