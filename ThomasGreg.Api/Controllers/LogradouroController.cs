using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThomasGreg.Application.Interfaces;
using ThomasGreg.Domain.Models;

namespace ThomasGreg.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LogradouroController : ControllerBase
    {
        private readonly ILogradouroService _LogradouroService;

        public LogradouroController(ILogradouroService LogradouroService)
        {
            _LogradouroService = LogradouroService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var result = await _LogradouroService.ObterPorId(id);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<LogradouroViewModel>>> ObterTodos()
        {
            var products = await _LogradouroService.ObterTodos();

            return Ok(products);
        }
        [HttpPost()]
        [Authorize]
        public async Task<ActionResult> Create([FromBody] LogradouroViewModel entity)
        {
            if (entity == null) return BadRequest();

            await _LogradouroService.Adicionar(entity);

            return Ok();
        }

        [HttpPut()]
        [Authorize]
        public async Task<ActionResult> Atualizar([FromBody] LogradouroViewModel entity)
        {
            if (entity == null) return BadRequest();

            await _LogradouroService.Atualizar(entity);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            await _LogradouroService.Deletar(id);

            return Ok();
        }
    }
}
