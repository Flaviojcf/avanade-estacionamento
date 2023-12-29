using AvanadeEstacionamento.Data.Context;
using AvanadeEstacionamento.Domain.Interfaces.Repository;
using AvanadeEstacionamento.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AvanadeEstacionamento.Data.Repository
{
    public class VeiculoRepository : BaseRepository<VeiculoModel>, IVeiculoRepository
    {
        #region Constructor

        public VeiculoRepository(MyDbContext context) : base(context) { }

        #endregion

        public async Task<IEnumerable<VeiculoModel>> GetByEstacionamentoId(Guid id)
        {
            return await Context.VeiculoModel.AsNoTrackingWithIdentityResolution()
                                             .Where(veiculo => veiculo.EstacionamentoId.Equals(id))
                                             .ToListAsync();
        }

        public async Task<VeiculoModel?> GetByPlaca(string placa)
        {
            return await Context.VeiculoModel.AsNoTrackingWithIdentityResolution()
                                             .FirstOrDefaultAsync(veiculo => veiculo.Placa.Equals(placa));
        }

    }
}
