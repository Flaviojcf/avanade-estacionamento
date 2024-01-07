using AutoMapper;
using AvanadeEstacionamento.Domain.DTO.Estacionamento;
using AvanadeEstacionamento.Domain.Exceptions;
using AvanadeEstacionamento.Domain.Interfaces.Repository;
using AvanadeEstacionamento.Domain.Models;
using AvanadeEstacionamento.Domain.Services;
using Moq;

namespace AvanadeEstacionamento.Tests
{
    public class EstacionamentoServiceUnitTest
    {
        #region Dependency Injection

        private readonly EstacionamentoService _estacionamentoService;
        private readonly Mock<IEstacionamentoRepository> _mockEstacionamentoRepository;
        private readonly Mock<IMapper> _mockMapper;

        #endregion

        #region Constructor

        public EstacionamentoServiceUnitTest()
        {
            _mockEstacionamentoRepository = new Mock<IEstacionamentoRepository>();
            _mockMapper = new Mock<IMapper>();
            ConfigureMocks();
            _estacionamentoService = new EstacionamentoService(_mockEstacionamentoRepository.Object, _mockMapper.Object);
        }

        private void ConfigureMocks()
        {
            _mockMapper.Setup(mapper => mapper.Map<EstacionamentoModel>(It.IsAny<RequestEstacionamentoDTO>()))
                       .Returns((RequestEstacionamentoDTO dto) => new EstacionamentoModel
                       {
                           Nome = dto.Nome,
                           PrecoHora = dto.PrecoHora,
                           PrecoInicial = dto.PrecoInicial
                       });
            _mockMapper.Setup(mapper => mapper.Map<EstacionamentoModel>(It.IsAny<RequestUpdateEstacionamentoDTO>()))
                     .Returns((RequestUpdateEstacionamentoDTO dto) => new EstacionamentoModel
                     {
                         Id = dto.Id,
                         Nome = dto.Nome,
                         PrecoHora = dto.PrecoHora,
                         PrecoInicial = dto.PrecoInicial
                     });
        }

        #endregion

        #region Create Method
        [Fact(DisplayName = "Deve ser possível criar um estacionamento.")]
        public async Task Create_WithValidParameters_ReturnsEstacionamentoModel()
        {
            // Arrange
            var estacionamentoDTO = new RequestEstacionamentoDTO
            {
                Nome = "Teste",
                PrecoHora = 10,
                PrecoInicial = 10
            };

            // Act

            _mockEstacionamentoRepository.Setup(repo => repo.Create(It.IsAny<EstacionamentoModel>()))
                                         .ReturnsAsync(true);
            var result = await _estacionamentoService.Create(estacionamentoDTO);

            // Assert
            Assert.IsType<EstacionamentoModel>(result);
        }

        [Fact(DisplayName = "Não deve ser possível criar um estacionamento com o nome já existente")]
        public async Task Create_WithSameName_ThrowResourceAlreadyExistsException()
        {
            // Arrange
            var estacionamentoDTO = new RequestEstacionamentoDTO
            {
                Nome = "Teste",
                PrecoHora = 10,
                PrecoInicial = 10
            };

            // Act
            _mockEstacionamentoRepository.Setup(repo => repo.Create(It.IsAny<EstacionamentoModel>()))
                                   .ReturnsAsync(true);
            var result = await _estacionamentoService.Create(estacionamentoDTO);
            _mockEstacionamentoRepository.Setup(repo => repo.GetByName("Teste")).Returns(Task.FromResult(result));

            // Assert
            await Assert.ThrowsAsync<ResourceAlreadyExistsException>(() => _estacionamentoService.Create(estacionamentoDTO));
        }

        #endregion

        #region GetAll Method

        [Fact(DisplayName = "Deve ser possível listar todos os estacionamentos existentes.")]
        public async Task GetAll_ReturnsEstacionamentoModelList()
        {
            // Arrange
            var estacionamento = new EstacionamentoModel
            {
                DataAlteracao = DateTime.Now,
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                IsAtivo = true,
                Nome = "Teste",
                PrecoHora = 10,
                PrecoInicial = 10
            };

            var estacionamentoL = new List<EstacionamentoModel> { estacionamento };

            // Act
            _mockEstacionamentoRepository.Setup(repo => repo.GetAll()).Returns(Task.FromResult<IEnumerable<EstacionamentoModel>>(estacionamentoL));
            var result = await _estacionamentoService.GetAll();

            // Assert
            Assert.Equal(estacionamentoL, result);
        }

        [Fact(DisplayName = "Não deve ser possível listar caso não exista nenhum estacionamento cadastrado.")]
        public async Task GetAll_WithNoRegisterEstacionamento_ReturnsNotFoundException()
        {
            // Arrange
            var estacionamentoL = new List<EstacionamentoModel>();

            // Act
            _mockEstacionamentoRepository.Setup(repo => repo.GetAll()).Returns(Task.FromResult<IEnumerable<EstacionamentoModel>>(estacionamentoL));

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await _estacionamentoService.GetAll());
        }

        #endregion

        #region GetById Method

        [Fact(DisplayName = "Deve ser possível listar um estacionamento pelo seu Id")]
        public async Task GetById_WithValidId_ReturnsEstacionamentoModel()
        {
            // Arrange
            var estacionamentoModel = new EstacionamentoModel
            {
                DataAlteracao = DateTime.Now,
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                IsAtivo = true,
                Nome = "Teste",
                PrecoHora = 10,
                PrecoInicial = 10
            };

            // Act
            _mockEstacionamentoRepository.Setup(repo => repo.GetById(estacionamentoModel.Id))
                                         .Returns(Task.FromResult(estacionamentoModel));
            var result = await _estacionamentoService.GetById(estacionamentoModel.Id);

            // Assert
            Assert.Equal(estacionamentoModel, result);
        }

        [Fact(DisplayName = "Não deve ser possível listar um estacionamento caso não exista o Id informado")]
        public async Task GetById_WithInValidId_ReturnsNotFoundException()
        {
            // Arrange
            var estacionamentoModel = new EstacionamentoModel();

            // Act
            _mockEstacionamentoRepository.Setup(repo => repo.GetById(estacionamentoModel.Id))
                                         .Returns(Task.FromResult(estacionamentoModel));

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await _estacionamentoService.GetById(new Guid()));
        }

        #endregion

        #region GetByName Method

        [Fact(DisplayName = "Deve ser possível listar um estacionamento pelo seu nome")]
        public async Task GetByName_ReturnsEstacionamentoModel()
        {
            // Arrange
            var estacionamentoModel = new EstacionamentoModel
            {
                DataAlteracao = DateTime.Now,
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                IsAtivo = true,
                Nome = "Teste",
                PrecoHora = 10,
                PrecoInicial = 10
            };

            // Act
            _mockEstacionamentoRepository.Setup(repo => repo.GetByName(estacionamentoModel.Nome))
                                         .Returns(Task.FromResult(estacionamentoModel));
            var result = await _estacionamentoService.GetByName(estacionamentoModel.Nome);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region Delete Method

        [Fact(DisplayName = "Deve ser possível deletar um estacionamento pelo seu Id")]
        public async Task Delete_ReturnsOk()
        {
            // Arrange
            var estacionamentoModel = new EstacionamentoModel
            {
                DataAlteracao = DateTime.Now,
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                IsAtivo = true,
                Nome = "Teste",
                PrecoHora = 10,
                PrecoInicial = 10
            };

            // Act
            _mockEstacionamentoRepository.Setup(repo => repo.Delete(estacionamentoModel.Id))
                                         .Returns(Task.FromResult(true));
            var result = await _estacionamentoService.Delete(estacionamentoModel.Id);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region Update Method

        [Fact(DisplayName = "Deve ser possível alterar um estacionamento pelo seu Id")]
        public async Task Update_ReturnsOk()
        {
            // Arrange
            var estacionamentoModel = new EstacionamentoModel
            {
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                IsAtivo = true,
                Nome = "Teste",
                PrecoHora = 10,
                PrecoInicial = 10
            };
            var requestUpdateEstacionamentoDTO = new RequestUpdateEstacionamentoDTO
            {
                Id = estacionamentoModel.Id,
                Nome = "Teste",
                PrecoHora = 10,
                PrecoInicial = 10
            };

            // Act
            _mockEstacionamentoRepository.Setup(repo => repo.Update(It.Is<EstacionamentoModel>(e => e.Id == estacionamentoModel.Id)))
                                         .Returns(Task.FromResult(true));
            var result = await _estacionamentoService.Update(requestUpdateEstacionamentoDTO, estacionamentoModel.Id);

            // Assert
            Assert.True(result);
        }

        #endregion

    }



}