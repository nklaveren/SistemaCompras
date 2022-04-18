using MediatR;
using SistemaCompra.Infra.Data.UoW;
using System.Threading;
using System.Threading.Tasks;

using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;
using System.Linq;
using SistemaCompra.Application.Produto.Query.ObterProduto;
using SistemaCompra.Domain.Core;
using AutoMapper;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly SolicitacaoCompraAgg.ISolicitacaoCompraRepository solicitacaoCompraRepository;
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        public RegistrarCompraCommandHandler(
            SolicitacaoCompraAgg.ISolicitacaoCompraRepository solicitacaoCompraRepository,
            IMapper mapper,
            IUnitOfWork uow,
            IMediator mediator) : base(uow, mediator)
        {
            this.solicitacaoCompraRepository = solicitacaoCompraRepository;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public async Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            var solicitacaoCompra = new SolicitacaoCompraAgg.SolicitacaoCompra(request.UsuarioSolicitante, request.NomeFornecedor);
            //TODO: chunk list
            var itens = await Task.WhenAll(request.Itens.Select(async x => await ObterItemValidandoProduto(x)));
            solicitacaoCompra.RegistrarCompra(itens);
            solicitacaoCompraRepository.Adicionar(solicitacaoCompra);
            PublishEvents(solicitacaoCompra.Events);
            Commit();
            return true;
        }

        private async Task<SolicitacaoCompraAgg.Item> ObterItemValidandoProduto(RegistrarCompraItem item)
        {
            var produtoViewModel = await mediator.Send(new ObterProdutoQuery { Id = item.Produto });
            if (produtoViewModel is null) throw new BusinessRuleException($"Produto { item.Produto} não existe");
            var produto = this.mapper.Map<ProdutoAgg.Produto>(produtoViewModel);
            return new SolicitacaoCompraAgg.Item(produto, item.Quantidade);
        }
    }
}
