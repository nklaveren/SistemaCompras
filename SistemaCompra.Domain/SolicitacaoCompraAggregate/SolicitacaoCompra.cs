using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate.Events;

using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        private const int VALOR_OBRIGA_PAGAMENTO_EM_TRINTA_DIAS = 50000;

        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }
        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public Money TotalGeral { get; private set; }
        public Situacao Situacao { get; private set; }
        public CondicaoPagamento CondicaoPagamento { get; private set; }
        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            Itens.Add(new Item(produto, qtde));
        }

        public void RegistrarCompra(IEnumerable<Item> itens)
        {
            if (itens.Count() == 0)
            {
                throw new BusinessRuleException("A solicitação de compra deve possuir itens!");
            }

            this.Itens = itens.ToList();
            this.TotalGeral = new Money(Itens.Sum(x => x.Subtotal.Value));

            RegistrarCondicaoPagamentoQuandoObrigatorio();

            this.AddEvent(new CompraRegistradaEvent(this.Id, this.Itens, this.TotalGeral.Value));
        }

        private void RegistrarCondicaoPagamentoQuandoObrigatorio()
        {
            if (this.TotalGeral.Value >= VALOR_OBRIGA_PAGAMENTO_EM_TRINTA_DIAS)
            {
                this.CondicaoPagamento = new TrintaDiasCondicaoPagamento();
            }
        }
    }
}
