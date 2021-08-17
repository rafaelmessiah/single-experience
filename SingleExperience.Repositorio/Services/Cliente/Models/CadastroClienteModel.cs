using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SingleExperience.Services.Cliente.Models
{
    public class CadastroClienteModel
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }

        public void Validar()
        {
            if (Cpf == null)
                throw new Exception("O cpf é obrigatório");

            if (Cpf.Length != 11)
                throw new Exception("O cpf precisa ter 11 digitos");

            if (Nome == null)
                throw new Exception("O nome é obrigatorio");

            if (Nome.Length > 100)
                throw new Exception("O nome ultrapassou o limite de caracteres");

            if (Email == null)
                throw new Exception("O email é obrigatorio");

            if (!Email.Contains("@"))
                throw new Exception("O email digitado não é valido");

            if (Email.Length > 100)
                throw new Exception("O email digitado ultrapassou o limite de caracteres");

            if (Senha == null)
                throw new Exception("A senha é obrigatoria");

            if (Senha.Length > 100)
                throw new Exception("A senha ultrapassou o limete de caracteres");

            if (DataNascimento == null)
                throw new Exception("A Data de Nascimento é obrigatoria");

            if (Telefone == null)
                throw new Exception("O telefone é obrigatorio");

            if (Telefone.Length > 20)
                throw new Exception("O telefone ultrapassou o limite de caracteres");
        }
    }
}
