using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SingleExperience.Repositorio.Services.Frete.Models
{
    [XmlRoot(ElementName ="cServico")]
    public class RespostaFreteCorreioModel
    {
        [XmlElement(ElementName = "Codigo")]
        public string Codigo { get; set; }
        [XmlElement(ElementName = "Valor")]
        public string Valor { get; set; }
        [XmlElement(ElementName = "PrazoEntrega")]
        public string PrazoEntrega { get; set; }
        [XmlElement(ElementName = "ValorSemAdicionais")]
        public string ValorSemAdicionais { get; set; }
        [XmlElement(ElementName = "ValorMaoPropria")]
        public string ValorMaoPropria { get; set; }
        [XmlElement(ElementName = "ValorAvisoRecebimento")]
        public string ValorAvisoRecebimento { get; set; }
        [XmlElement(ElementName = "ValorValorDeclarado")]
        public string ValorValorDeclarado { get; set; }
        [XmlElement(ElementName = "EntregaDomiciliar")]
        public string EntregaDomiciliar { get; set; }
        [XmlElement(ElementName = "EntregaSabado")]
        public string EntregaSabado { get; set; }
        [XmlElement(ElementName = "Erro")]
        public string Erro { get; set; }
    }
}
