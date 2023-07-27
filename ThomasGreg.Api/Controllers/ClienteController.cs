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
            try
            {
                var result = await _clienteService.ObterPorId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ClienteViewModel>>> ObterTodos()
        {
            try
            {
                var products = await _clienteService.ObterTodos();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost()]
        [Authorize]
        public async Task<ActionResult> Create([FromBody] ClienteViewModel entity)
        {
            try
            {
                if (entity == null) return BadRequest();

                var cliente = _clienteService.EmialExiste(entity.Email);

                if (cliente) return BadRequest();

                await _clienteService.Adicionar(entity);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut()]
        [Authorize]
        public async Task<ActionResult> Atualizar([FromBody] ClienteViewModel entity)
        {
            try
            {
                if (entity == null) return BadRequest();

                await _clienteService.Atualizar(entity);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0) return BadRequest();

                await _clienteService.Deletar(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
