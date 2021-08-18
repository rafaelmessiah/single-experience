using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.Cliente.Models
{
    public class EdicaoEmailModel
    {
        public int ClienteId { get; set; }
        public string NovoEmail { get; set; }

        public void Validar()
        {
            if (ClienteId < 1)
                throw new Exception("O clienteId é necessário para esse cadastro");

            if (NovoEmail == null)
                throw new Exception("O Email é obrigatório");

            if (!NovoEmail.Contains("@"))
                throw new Exception("O Email digitado é invalido");

            if (NovoEmail.Length > 100)
                throw new Exception("O email digitado ultrapassou o limite de caracteres");
        }
    }
}
