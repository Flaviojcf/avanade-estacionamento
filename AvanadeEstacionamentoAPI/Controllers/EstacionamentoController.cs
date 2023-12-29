using AvanadeEstacionamento.Domain.Interfaces.Service;
using AvanadeEstacionamento.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AvanadeEstacionamento.API.Controllers
{
    [Route("api/estacionamento")]
    [ApiController]
    public class EstacionamentoController : ControllerBase
    {

        #region Dependency Injection

        private readonly IEstacionamentoService _estacionamentoService;

        #endregion

        #region Constructor

        public EstacionamentoController(IEstacionamentoService estacionamentoService)
        {
            _estacionamentoService = estacionamentoService;
        }

        #endregion

        #region HTTP Requests

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _estacionamentoService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _estacionamentoService.GetById(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult> Create(EstacionamentoModel estacionamentoModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _estacionamentoService.Create(estacionamentoModel);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _estacionamentoService.Delete(id);

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(EstacionamentoModel estacionamentoModel, Guid id)
        {
            var result = await _estacionamentoService.Update(estacionamentoModel, id);

            return Ok(result);
        }

        #endregion
    }
}
