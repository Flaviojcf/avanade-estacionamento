using AvanadeEstacionamento.Domain.Interfaces.Service;
using AvanadeEstacionamento.Domain.Models;
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

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById(Guid id) 
        {
            var result = await _veiculoService.GetById(id);
            return Ok(result);
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetByEstacionamentoId(Guid id)
        {
            var result = await _veiculoService.GetByEstacionamentoId(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(VeiculoModel veiculoModel)
        {
            var result = await _veiculoService.Create(veiculoModel);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _veiculoService.Delete(id);

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(VeiculoModel veiculoModel, Guid id)
        {
            var result = await _veiculoService.Update(veiculoModel, id);

            return Ok(result);
        }

        #endregion

    }
}
