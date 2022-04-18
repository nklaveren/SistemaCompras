using MediatR;

using System.Collections.Generic;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommand : IRequest<bool>
    {
        public RegistrarCompraCommand() { }

        public IReadOnlyCollection<RegistrarCompraItem> Itens { get; set; } = new List<RegistrarCompraItem>();
        public string UsuarioSolicitante { get; set; }
        public string NomeFornecedor { get; set; }
    }
}
