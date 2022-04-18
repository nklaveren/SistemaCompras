using MediatR;

using Microsoft.AspNetCore.Mvc;

using SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra;

using System.Threading.Tasks;

namespace SistemaCompra.API.Produto
{
    public class SolicitacaoCompraController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SolicitacaoCompraController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost, Route("solicitacaoCompra/RegistrarCompra")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RegistrarCompra([FromBody] RegistrarCompraCommand command)
        {
            return StatusCode(201, await _mediator.Send(command));
        }
    }
}
