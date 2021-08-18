using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.Cliente.Models
{
    public class EdicaoSenhaModel
    {
        public int ClienteId { get; set; }
        public string NovaSenha { get; set; }

        public void Validar()
        {
            if (ClienteId < 1)
                throw new Exception("O clienteId é necessário para esse cadastro");

            if (NovaSenha == null)
                throw new Exception("A senha é obrigatoria");

            if (NovaSenha.Length > 100)
                throw new Exception("A senha ultrapassou o limete de caracteres");
        }
    }
}
