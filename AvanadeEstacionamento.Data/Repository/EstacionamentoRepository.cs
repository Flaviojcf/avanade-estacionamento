using AvanadeEstacionamento.Data.Context;
using AvanadeEstacionamento.Domain.Interfaces.Repository;
using AvanadeEstacionamento.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AvanadeEstacionamento.Data.Repository
{
    public class EstacionamentoRepository : BaseRepository<EstacionamentoModel>, IEstacionamentoRepository
    {

        #region Constructor

        public EstacionamentoRepository(MyDbContext context) : base(context) { }


        public async Task<EstacionamentoModel?> GetByName(string name)
        {
            return await Context.EstacionamentoModel.AsNoTrackingWithIdentityResolution()
                                             .FirstOrDefaultAsync(estacionamento => estacionamento.Nome.Equals(name));
        }

        #endregion

    }
}
