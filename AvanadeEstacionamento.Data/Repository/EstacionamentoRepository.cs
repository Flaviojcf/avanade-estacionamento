using AvanadeEstacionamento.Data.Context;
using AvanadeEstacionamento.Domain.Interfaces.Repository;
using AvanadeEstacionamento.Domain.Models;

namespace AvanadeEstacionamento.Data.Repository
{
    public class EstacionamentoRepository : BaseRepository<EstacionamentoModel>, IEstacionamentoRepository
    {

        #region Constructor

        public EstacionamentoRepository(MyDbContext context) : base(context) { }

        #endregion

    }
}
