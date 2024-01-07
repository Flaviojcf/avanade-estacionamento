using AutoMapper;
using AvanadeEstacionamento.Domain.DTO.Veiculo;
using AvanadeEstacionamento.Domain.Exceptions;
using AvanadeEstacionamento.Domain.Interfaces.Repository;
using AvanadeEstacionamento.Domain.Models;
using AvanadeEstacionamento.Domain.Services;
using Moq;

namespace AvanadeEstacionamento.Tests
{
    public class VeiculoServiceUnitTest
    {
        #region Dependency Injection

        private readonly EstacionamentoService _estacionamentoService;
        private readonly VeiculoService _veiculoService;
        private readonly Mock<IEstacionamentoRepository> _mockEstacionamentoRepository;
        private readonly Mock<IVeiculoRepository> _mockVeiculoRepository;
        private readonly Mock<IMapper> _mockMapper;

        #endregion

        #region Constructor

        public VeiculoServiceUnitTest()
        {
            _mockEstacionamentoRepository = new Mock<IEstacionamentoRepository>();
            _mockVeiculoRepository = new Mock<IVeiculoRepository>();
            _mockMapper = new Mock<IMapper>();
            ConfigureMocks();
            _veiculoService = new VeiculoService(_mockVeiculoRepository.Object, _mockEstacionamentoRepository.Object, _mockMapper.Object);
            _estacionamentoService = new EstacionamentoService(_mockEstacionamentoRepository.Object, _mockMapper.Object);
        }

        #endregion

        private void ConfigureMocks()
        {
            _mockMapper.Setup(mapper => mapper.Map<VeiculoModel>(It.IsAny<RequestVeiculoDTO>()))
                       .Returns((RequestVeiculoDTO dto) => new VeiculoModel
                       {
                           Placa = dto.Placa,
                           EstacionamentoId = dto.EstacionamentoId,
                       });
            _mockMapper.Setup(mapper => mapper.Map<VeiculoModel>(It.IsAny<RequestUpdateVeiculoDTO>()))
                     .Returns((RequestUpdateVeiculoDTO dto) => new VeiculoModel
                     {
                         Id = dto.Id,
                         Placa = dto.Placa
                     });
        }

        #region GetAll Method

        [Fact(DisplayName = "Deve ser possível listar todos os veiculos existentes.")]
        public async Task GetAll_ReturnsVeiculoModelList()
        {
            // Arrange
            var veiculo = new VeiculoModel
            {
                DataAlteracao = DateTime.Now,
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                IsAtivo = true,
                Placa = "ABC-2440",
                EstacionamentoId = Guid.NewGuid(),
            };

            var veiculoL = new List<VeiculoModel> { veiculo };

            // Act
            _mockVeiculoRepository.Setup(repo => repo.GetAll()).Returns(Task.FromResult<IEnumerable<VeiculoModel>>(veiculoL));
            var result = await _veiculoService.GetAll();

            // Assert
            Assert.Equal(veiculoL, result);
        }

        [Fact(DisplayName = "Não deve ser possível listar caso não exista nenhum veiculo cadastrado.")]
        public async Task GetAll_WithNoRegisterEstacionamento_ReturnsNotFoundException()
        {
            // Arrange
            var veiculoL = new List<VeiculoModel>();

            // Act
            _mockVeiculoRepository.Setup(repo => repo.GetAll()).Returns(Task.FromResult<IEnumerable<VeiculoModel>>(veiculoL));

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await _veiculoService.GetAll());
        }

        #endregion

        #region GetByEstacionamentoId Method

        [Fact(DisplayName = "Deve ser possível listar um veiculo pelo Id do seu estacionamento")]
        public async Task GetByEstacionamentoId_WithValidId_ReturnsVeiculoModel()
        {
            // Arrange
            var veiculoModel = new VeiculoModel
            {
                DataAlteracao = DateTime.Now,
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                IsAtivo = true,
                Placa = "ABC-2440",
                EstacionamentoId = Guid.NewGuid(),
            };
            var veiculoL = new List<VeiculoModel> { veiculoModel };

            // Act
            _mockVeiculoRepository.Setup(repo => repo.GetByEstacionamentoId(veiculoModel.EstacionamentoId))
                                         .Returns(Task.FromResult<IEnumerable<VeiculoModel>>(veiculoL));
            var result = await _veiculoService.GetByEstacionamentoId(veiculoModel.EstacionamentoId);

            // Assert
            Assert.Equal(veiculoL, result);
        }

        [Fact(DisplayName = "Não deve ser possível listar um veiculo caso não exista o Id do estacionamento informado")]
        public async Task GetByEstacionamentoId_WithInValidId_ReturnsNotFoundException()
        {
            // Arrange
            var veiculoModel = new VeiculoModel();

            var veiculoL = new List<VeiculoModel> { veiculoModel };

            // Act
            _mockVeiculoRepository.Setup(repo => repo.GetByEstacionamentoId(veiculoModel.EstacionamentoId))
                                      .Returns(Task.FromResult<IEnumerable<VeiculoModel>>(veiculoL));

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await _veiculoService.GetById(new Guid()));
        }

        #endregion

        #region GetById Method

        [Fact(DisplayName = "Deve ser possível listar um veiculo pelo seu Id")]
        public async Task GetById_WithValidId_ReturnsVeiculoModel()
        {
            // Arrange
            var veiculoModel = new VeiculoModel
            {
                DataAlteracao = DateTime.Now,
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                IsAtivo = true,
                Placa = "ABC-2440",
                EstacionamentoId = Guid.NewGuid(),
            };

            // Act
            _mockVeiculoRepository.Setup(repo => repo.GetById(veiculoModel.Id))
                                         .Returns(Task.FromResult(veiculoModel));
            var result = await _veiculoService.GetById(veiculoModel.Id);

            // Assert
            Assert.Equal(veiculoModel, result);
        }

        [Fact(DisplayName = "Não deve ser possível listar um veiculo caso não exista o Id informado")]
        public async Task GetById_WithInValidId_ReturnsNotFoundException()
        {
            // Arrange
            var veiculoModel = new VeiculoModel();

            // Act
            _mockVeiculoRepository.Setup(repo => repo.GetById(veiculoModel.Id))
                                         .Returns(Task.FromResult(veiculoModel));

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await _veiculoService.GetById(new Guid()));
        }

        #endregion

        #region GetDebt Method

        [Fact(DisplayName = "Deve ser possível listar o débito de um veículo pelo seu Id")]
        public async Task GetDebt_WithValidId_ReturnsDecimalDebt()
        {
            // Arrange
            var veiculoDTO = new RequestVeiculoDTO
            {
                Placa = "ABC-2024",
                EstacionamentoId = Guid.NewGuid()
            };

            var veiculoModel = new VeiculoModel
            {
                DataAlteracao = DateTime.Now,
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                IsAtivo = true,
                Placa = "ABC-2440",
                EstacionamentoId = veiculoDTO.EstacionamentoId,
            };

            var estacionamentoModel = new EstacionamentoModel
            {
                DataAlteracao = DateTime.Now,
                DataCriacao = DateTime.Now,
                Id = veiculoDTO.EstacionamentoId,
                IsAtivo = true,
                Nome = "Teste",
                PrecoHora = 10,
                PrecoInicial = 10
            };
            var debt = 10.006;


            // Act
            _mockVeiculoRepository.Setup(repo => repo.GetById(veiculoModel.Id))
                     .Returns(Task.FromResult(veiculoModel));

            _mockEstacionamentoRepository.Setup(repo => repo.GetById(estacionamentoModel.Id))
                                         .Returns(Task.FromResult(estacionamentoModel));

            await _estacionamentoService.GetById(estacionamentoModel.Id);
            await Task.Delay(2000);
            var result = await _veiculoService.GetDebt(veiculoModel.Id);



            // Assert
            Assert.Equal(debt, (double)result, 3);
        }


        #endregion

        #region GetByPlaca Method

        [Fact(DisplayName = "Deve ser possível listar um veiculo pela sua placa")]
        public async Task GetByPlaca_ReturnsVeiculoModel()
        {
            // Arrange
            var veiculoModel = new VeiculoModel
            {
                DataAlteracao = DateTime.Now,
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                IsAtivo = true,
                Placa = "ABC-2440",
                EstacionamentoId = Guid.NewGuid(),
            };

            // Act
            _mockVeiculoRepository.Setup(repo => repo.GetByPlaca(veiculoModel.Placa))
                                         .Returns(Task.FromResult(veiculoModel));
            var result = await _veiculoService.GetByPlaca(veiculoModel.Placa);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region Create Method

        [Fact(DisplayName = "Deve ser possível criar um veiculo.")]
        public async Task Create_WithValidParameters_ReturnsVeiculoModel()
        {
            // Arrange
            var veiculoDTO = new RequestVeiculoDTO
            {
                Placa = "ABC-2024",
                EstacionamentoId = Guid.NewGuid()
            };

            var estacionamentoModel = new EstacionamentoModel
            {
                DataAlteracao = DateTime.Now,
                DataCriacao = DateTime.Now,
                Id = veiculoDTO.EstacionamentoId,
                IsAtivo = true,
                Nome = "Teste",
                PrecoHora = 10,
                PrecoInicial = 10
            };

            // Act
            _mockVeiculoRepository.Setup(repo => repo.Create(It.IsAny<VeiculoModel>()))
                                         .ReturnsAsync(true);
            _mockEstacionamentoRepository.Setup(repo => repo.GetById(veiculoDTO.EstacionamentoId))
                                         .Returns(Task.FromResult(estacionamentoModel));

            var result = await _veiculoService.Create(veiculoDTO);

            // Assert
            Assert.IsType<VeiculoModel>(result);
        }

        [Fact(DisplayName = "Não deve ser possível criar um veiculo com uma placa já existente")]
        public async Task Create_WithSamePlaca_ThrowResourceAlreadyExistsException()
        {
            // Arrange
            var veiculoDTO = new RequestVeiculoDTO
            {
                Placa = "ABC-2024",
                EstacionamentoId = Guid.NewGuid()
            };

            var estacionamentoModel = new EstacionamentoModel
            {
                DataAlteracao = DateTime.Now,
                DataCriacao = DateTime.Now,
                Id = veiculoDTO.EstacionamentoId,
                IsAtivo = true,
                Nome = "Teste",
                PrecoHora = 10,
                PrecoInicial = 10
            };


            // Act
            _mockVeiculoRepository.Setup(repo => repo.Create(It.IsAny<VeiculoModel>()))
                                   .ReturnsAsync(true);
            _mockEstacionamentoRepository.Setup(repo => repo.GetById(veiculoDTO.EstacionamentoId))
                                    .Returns(Task.FromResult(estacionamentoModel));
            var result = await _veiculoService.Create(veiculoDTO);
            _mockVeiculoRepository.Setup(repo => repo.GetByPlaca("ABC-2024")).Returns(Task.FromResult(result));

            // Assert
            await Assert.ThrowsAsync<ResourceAlreadyExistsException>(() => _veiculoService.Create(veiculoDTO));
        }

        #endregion

        #region Delete Method

        [Fact(DisplayName = "Deve ser possível deletar um veiculo pelo seu Id")]
        public async Task Delete_ReturnsOk()
        {
            // Arrange
            var veiculoModel = new VeiculoModel
            {
                DataAlteracao = DateTime.Now,
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                IsAtivo = true,
                Placa = "ABC-2440",
                EstacionamentoId = Guid.NewGuid(),
            };

            // Act
            _mockVeiculoRepository.Setup(repo => repo.Delete(veiculoModel.Id))
                                         .Returns(Task.FromResult(true));
            var result = await _veiculoService.Delete(veiculoModel.Id);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region Update Method

        [Fact(DisplayName = "Deve ser possível alterar um veiculo pelo seu Id")]
        public async Task Update_ReturnsOk()
        {
            // Arrange
            var veiculoModel = new VeiculoModel
            {
                DataAlteracao = DateTime.Now,
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                IsAtivo = true,
                Placa = "ABC-2440",
                EstacionamentoId = Guid.NewGuid(),
            };

            var requestUpdateVeiculoDTO = new RequestUpdateVeiculoDTO
            {
                Id = veiculoModel.Id,
                Placa = "ABD-4040"
            };

            // Act
            _mockVeiculoRepository.Setup(repo => repo.Update(It.Is<VeiculoModel>(e => e.Id == veiculoModel.Id)))
                                         .Returns(Task.FromResult(true));
            _mockVeiculoRepository.Setup(repo => repo.GetById(veiculoModel.Id))
                                         .Returns(Task.FromResult(veiculoModel));
            var result = await _veiculoService.Update(requestUpdateVeiculoDTO, veiculoModel.Id);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region Checkout Method

        [Fact(DisplayName = "Deve ser possível realizar o checkout de um veículo pelo seu Id")]
        public async Task Checkout_WithValidId_ResponseCheckoutVeiculoDTO()
        {
            // Arrange
            var veiculoModel = new VeiculoModel
            {
                DataAlteracao = DateTime.Now,
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                IsAtivo = true,
                Placa = "ABC-2440",
                EstacionamentoId = Guid.NewGuid(),
            };
            var estacionamentoModel = new EstacionamentoModel
            {
                DataAlteracao = DateTime.Now,
                DataCriacao = DateTime.Now,
                Id = veiculoModel.EstacionamentoId,
                IsAtivo = true,
                Nome = "Teste",
                PrecoHora = 10,
                PrecoInicial = 10
            };
            var debt = 10.01;
            _mockVeiculoRepository.Setup(repo => repo.GetById(veiculoModel.Id))
                                  .Returns(Task.FromResult(veiculoModel));
            _mockEstacionamentoRepository.Setup(repo => repo.GetById(estacionamentoModel.Id))
                                      .Returns(Task.FromResult(estacionamentoModel));
            _mockVeiculoRepository.Setup(repo => repo.Update(It.Is<VeiculoModel>(e => e.Id == veiculoModel.Id)))
                                       .Returns(Task.FromResult(true));
            await Task.Delay(2000);

            // Act
            var result = await _veiculoService.Checkout(veiculoModel.Id);

            // Assert
            Assert.NotNull(result);
            Assert.False(veiculoModel.IsAtivo);
            Assert.NotNull(veiculoModel.DataCheckout);
            Assert.Equal(debt, (double)result.TotalDebt, 2);
        }


        #endregion

    }
}
