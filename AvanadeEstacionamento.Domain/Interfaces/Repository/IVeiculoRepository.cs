using AvanadeEstacionamento.Domain.Models;
using bank.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvanadeEstacionamento.Domain.Interfaces.Repository
{
    public interface IVeiculoRepository : IBaseRepository<VeiculoModel>
    {
        Task<IEnumerable<VeiculoModel>> GetByEstacionamentoId(Guid id);
    }
}
