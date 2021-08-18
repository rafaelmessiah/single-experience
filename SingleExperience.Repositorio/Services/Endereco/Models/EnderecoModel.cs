using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.Endereco.Models
{
    public class EnderecoModel
    {
        public int EnderecoId { get; set; }
        public int ClienteId { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }

        public void Validar()
        {
            if (ClienteId < 1)
                throw new Exception("O ClienteId é obrigatorio para esse cadastro");

            if (EnderecoId < 1)
                throw new Exception("O EnderecoId é obrigatorio para esse cadastro");

            if (Rua == null)
                throw new Exception("O nome da rua é obritatorio");

            if (Rua.Length > 100)
                throw new Exception("A rua ultrapassou o limite de Caracteres");

            if (Numero == null)
                throw new Exception("O numero é obritatorio");

            if (Numero.Length > 30)
                throw new Exception("O numero passou o limite de Caracteres");

            if (Complemento == null)
                throw new Exception("O complemento é obritatorio");

            if (Complemento.Length > 30)
                throw new Exception("O complemento passou o limite de Caracteres");

            if (Cep == null)
                throw new Exception("O Cep é obritatorio");

            if (Cep.Length > 8)
                throw new Exception("O Cep passou o limite de Caracteres");
        }

    }
}
