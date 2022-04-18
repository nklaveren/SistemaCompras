
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.Produto
{
    public class SolicitacaoCompraRepository : SolicitacaoAgg.ISolicitacaoCompraRepository
    {
        private readonly SistemaCompraContext context;

        public SolicitacaoCompraRepository(SistemaCompraContext context)
        {
            this.context = context;
        }

        public void Adicionar(SolicitacaoAgg.SolicitacaoCompra entity)
        {
            context.Set<SolicitacaoAgg.SolicitacaoCompra>().Add(entity);
        }
    }
}
