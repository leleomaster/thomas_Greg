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
            try
            {
                var result = await _LogradouroService.ObterPorId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<LogradouroViewModel>>> ObterTodos()
        {
            try
            {
                var products = await _LogradouroService.ObterTodos();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost()]
        [Authorize]
        public async Task<ActionResult> Create([FromBody] LogradouroViewModel entity)
        {
            try
            {
                if (entity == null) return BadRequest();

                await _LogradouroService.Adicionar(entity);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut()]
        [Authorize]
        public async Task<ActionResult> Atualizar([FromBody] LogradouroViewModel entity)
        {
            try
            {
                if (entity == null) return BadRequest();

                await _LogradouroService.Atualizar(entity);

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

                await _LogradouroService.Deletar(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
