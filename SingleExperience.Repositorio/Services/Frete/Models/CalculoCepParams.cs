using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Repositorio.Services.Frete.Models
{
    public class CalculoCepParams
    {
        public string nCdEmpresa { get; set; }
        public string sDsSenha { get; set; }
        public string sCepOrigem { get; set; }
        public string sCepDestino { get; set; }
        public string nVlPeso { get; set; }
        public string nCdFormato { get; set; }
        public string nVlComprimento { get; set; }
        public string nVlAltura { get; set; }
        public string nVlLargura { get; set; }
        public string sCdMaoPropria { get; set; }
        public string nVlValorDeclarado { get; set; }
        public string sCdAvisoRecebimento { get; set; }
        public string nCdServico { get; set; }
        public string nVlDiametro { get; set; }
        public string StrRetorno { get; set; }
        public string nIndicaCalculo { get; set; }
    }
}
