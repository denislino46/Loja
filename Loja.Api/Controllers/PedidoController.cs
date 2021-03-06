using Loja.Application.Contract.Pedido;
using Loja.Application.Contracts.Commom;
using Loja.Application.Contracts.Pedido;
using Loja.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Loja.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : Controller
    {
        private readonly IPedidoService _service;
        public PedidoController(IPedidoService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseModel<PedidoResponse>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ResponseModel<object>))]
        public async Task<IActionResult> PostAsync([FromBody] PedidoRequest request)
        {
            var resposta = await _service.InserirAsync(request).ConfigureAwait(false);
            if (resposta.IsValid)
                return Ok(resposta);
            else
                return BadRequest(resposta);
        }

        [HttpPut]
        [Route("")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseModel<bool>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ResponseModel<bool>))]
        public async Task<IActionResult> PutAsync([FromBody] PedidoRequest request)
        {
            var resposta = await _service.AtualizarAsync(request).ConfigureAwait(false);
            if (resposta.IsValid)
                return Ok(resposta);
            else
                return BadRequest(resposta);
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseModel<PedidoResponse>))]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
            => Ok(await _service.ObterAsync(id).ConfigureAwait(false));

        [HttpGet]
        [Route("")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseModel<PedidoResponse>))]
        public async Task<IActionResult> GetAsync()
            => Ok(await _service.ListarAsync().ConfigureAwait(false));

        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
            => Ok(await _service.DeletarAsync(id).ConfigureAwait(false));
    }
}
