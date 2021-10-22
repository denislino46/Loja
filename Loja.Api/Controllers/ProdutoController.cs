using Loja.Application.Contract.Produto;
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
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _service;
        public ProdutoController(IProdutoService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Produto))]
        public async Task<IActionResult> PostAsync([FromBody] ProdutoRequest request)
            => Ok(await _service.InserirAsync(request).ConfigureAwait(false));

        [HttpPut]
        [Route("")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(bool))]
        public async Task<IActionResult> PutAsync([FromBody] ProdutoRequest request)
            => Ok(await _service.AtualizarAsync(request).ConfigureAwait(false));

        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Produto))]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
            => Ok(await _service.ObterAsync(id).ConfigureAwait(false));

        [HttpGet]
        [Route("")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Produto))]
        public async Task<IActionResult> GetAsync()
            => Ok(await _service.ListarAsync().ConfigureAwait(false));

        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
            => Ok(await _service.DeletarAsync(id).ConfigureAwait(false));
    }
}
