using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SingleExperience.Repositorio.Services.Frete.Models
{
    public class CalcularFreteModel
    {
        [XmlElement(Namespace = "https://www.w3.org/XML")]
        public string Codigo { get; set; }

        [XmlElement(Namespace = "https://www.w3.org/XML")]
        public string Valor { get; set; }

        [XmlElement(Namespace = "https://www.w3.org/XML")]
        public string PrazoEntrega { get; set; }

        [XmlElement(Namespace = "https://www.w3.org/XML")]
        public string ValorSemAdicionais { get; set; }
        
        [XmlElement(Namespace = "https://www.w3.org/XML")]
        public string ValorMaoPropria { get; set; }

        [XmlElement(Namespace = "https://www.w3.org/XML")]
        public string ValorAvisoRecebimento { get; set; }

        [XmlElement(Namespace = "https://www.w3.org/XML")]
        public string ValorValorDeclarado { get; set; }

        [XmlElement(Namespace = "https://www.w3.org/XML")]

        [XmlElement(Namespace = "https://www.w3.org/XML")]
        public string EntregaDomiciliar { get; set; }

        [XmlElement(Namespace = "https://www.w3.org/XML")]
        public string EntregaSabado { get; set; }

        [XmlElement(Namespace = "https://www.w3.org/XML")]
        public string ObsFim { get; set; }

        [XmlElement(Namespace = "https://www.w3.org/XML")]
        public string Erro { get; set; }

        [XmlElement(Namespace = "https://www.w3.org/XML")]
        public string MsgErro { get; set; }
    }
}
