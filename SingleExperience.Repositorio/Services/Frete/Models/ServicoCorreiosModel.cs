using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SingleExperience.Repositorio.Services.Frete.Models
{
    [XmlRoot(ElementName= "Servicos")]
    public class ServicoCorreiosModel
    {
        [XmlElement(ElementName = "cServico")]
        public RespostaFreteCorreioModel RespostaFreteCorreioModel { get; set; }
    }
}
