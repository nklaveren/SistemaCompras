using SistemaCompra.Domain.Core;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate.Exceptions
{
    public class SolicitacaoDevePossuirItensBusinessRuleException : BusinessRuleException
    {
        public SolicitacaoDevePossuirItensBusinessRuleException() : base("A solicitação de compra deve possuir itens!")
        {
        }
    }
}
