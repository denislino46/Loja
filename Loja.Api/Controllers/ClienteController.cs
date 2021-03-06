using Loja.Application.Contract.Cliente;
using Loja.Application.Contracts.Commom;
using Loja.Application.Entities;
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
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseModel<Cliente>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ResponseModel<object>))]
        public async Task<IActionResult> PostAsync([FromBody] ClienteRequest request)
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
        public async Task<IActionResult> PutAsync([FromBody] ClienteRequest request)
        {
            var resposta = await _service.AtualizarAsync(request).ConfigureAwait(false);
            if (resposta.IsValid)
                return Ok(resposta);
            else
                return BadRequest(resposta);
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseModel<Cliente>))]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
            => Ok(await _service.ObterAsync(id).ConfigureAwait(false));

        [HttpGet]
        [Route("")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseModel<Cliente>))]
        public async Task<IActionResult> GetAsync()
            => Ok(await _service.ListarAsync().ConfigureAwait(false));

        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseModel<bool>))]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
            => Ok(await _service.DeletarAsync(id).ConfigureAwait(false));
    }
}
