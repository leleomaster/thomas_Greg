using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThomasGreg.Application.Interfaces;
using ThomasGreg.Domain.Models;

namespace ThomasGreg.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var result = await _clienteService.ObterPorId(id);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ClienteViewModel>>> ObterTodos()
        {
            var products = await _clienteService.ObterTodos();

            return Ok(products);
        }
        [HttpPost()]
        [Authorize]
        public async Task<ActionResult> Create([FromBody] ClienteViewModel entity)
        {
            if (entity == null) return BadRequest();

            var cliente = _clienteService.EmialExiste(entity.Email);

            if (cliente) return BadRequest();

            await _clienteService.Adicionar(entity);

            return Ok();
        }

        [HttpPut()]
        [Authorize]
        public async Task<ActionResult> Atualizar([FromBody] ClienteViewModel entity)
        {
            if (entity == null) return BadRequest();

            await _clienteService.Atualizar(entity);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            await _clienteService.Deletar(id);

            return Ok();
        }
    }
}
