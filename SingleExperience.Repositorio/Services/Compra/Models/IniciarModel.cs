using SingleExperience.Enums;

namespace SingleExperience.Services.Compra.Models
{
    public class IniciarModel
    {
        public int ClienteId { get; set; }
        public int EnderecoId { get; set; }
        public FormaPagamentoEnum FormaPagamentoEnum { get; set; }
    }
}
