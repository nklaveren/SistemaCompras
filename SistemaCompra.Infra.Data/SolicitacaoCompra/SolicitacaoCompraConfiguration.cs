using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.Produto
{
    public class SolicitacaoCompraConfiguration : IEntityTypeConfiguration<SolicitacaoAgg.SolicitacaoCompra>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoAgg.SolicitacaoCompra> builder)
        {
            builder.ToTable("SolicitacaoCompras");

            builder.OwnsOne(
                c => c.CondicaoPagamento,
                b => b.Property(x => x.Valor)
                .HasColumnName("CondicaoPagamento"));

            builder.OwnsOne(
                c => c.UsuarioSolicitante,
                b => b.Property(x => x.Nome)
                .HasColumnName("UsuarioSolicitante"));

            builder.OwnsOne(
                c => c.NomeFornecedor,
                b => b.Property(x => x.Nome)
                .HasColumnName("NomeFornecedor"));

            builder.OwnsOne(
                c => c.TotalGeral,
                b => b.Property(x => x.Value)
                .HasColumnName("TotalGeral"));
        }
    }
}
