using AutoMapper;
using AvanadeEstacionamento.Domain.DTO.Estacionamento;
using AvanadeEstacionamento.Domain.DTO.Veiculo;
using AvanadeEstacionamento.Domain.Models;

namespace AvanadeEstacionamento.Domain.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VeiculoModel, RequestVeiculoDTO>().ReverseMap();
            CreateMap<VeiculoModel, RequestUpdateVeiculoDTO>().ReverseMap();


            CreateMap<EstacionamentoModel, RequestEstacionamentoDTO>().ReverseMap();
            CreateMap<EstacionamentoModel, RequestUpdateEstacionamentoDTO>().ReverseMap();
        }
    }
}
