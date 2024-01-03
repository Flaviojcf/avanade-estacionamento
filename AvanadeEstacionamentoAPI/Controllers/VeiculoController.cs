using AvanadeEstacionamento.Domain.DTO.Veiculo;
using AvanadeEstacionamento.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace AvanadeEstacionamento.API.Controllers
{
    [Route("api/veiculo")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {

        #region Dependency Injection

        private readonly IVeiculoService _veiculoService;

        #endregion

        #region Constructor

        public VeiculoController(IVeiculoService veiculoService)
        {
            _veiculoService = veiculoService;
        }

        #endregion

        #region HTTP Requests

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _veiculoService.GetAll();
            return Ok(result);
        }

        [HttpGet("ById/{id:guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _veiculoService.GetById(id);
            return Ok(result);
        }

        [HttpGet("ByEstacionamentoId/{id:guid}")]
        public async Task<ActionResult> GetByEstacionamentoId(Guid id)
        {
            var result = await _veiculoService.GetByEstacionamentoId(id);
            return Ok(result);
        }

        [HttpGet("GetDebt/{id:guid}")]
        public async Task<ActionResult> GetDebt(Guid id)
        {
            var result = new { Debt = await _veiculoService.GetDebt(id) };

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] RequestVeiculoDTO veiculoDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _veiculoService.Create(veiculoDTO);
            return Created("/api/veiculo", result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _veiculoService.Delete(id);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(RequestUpdateVeiculoDTO veiculoDTO, Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _veiculoService.Update(veiculoDTO, id);
            return Ok(result);
        }

        [HttpPost("Checkout/{id:guid}")]
        public async Task<ActionResult> Checkout(Guid id)
        {
            ResponseCheckoutVeiculoDTO veiculoCheckoutDTO = await _veiculoService.Checkout(id);
            return Ok(new { EntranceDate = veiculoCheckoutDTO.DataCriacao.ToString("yyyy-MM-dd HH:mm:ss"), ExitDate = veiculoCheckoutDTO.DataCheckout?.ToString("yyyy-MM-dd HH:mm:ss"), veiculoCheckoutDTO.TotalDebt });
        }


        #endregion

    }
}
