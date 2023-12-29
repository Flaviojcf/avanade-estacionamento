using AvanadeEstacionamento.Domain.Models;

namespace AvanadeEstacionamento.Domain.Interfaces.Repository
{
    public interface IBaseRepository<TModel> : IDisposable where TModel : BaseModel
    {
        Task<bool> Create(TModel model);
        Task<bool> Update(TModel model);
        Task<bool> Delete(Guid id);
        Task<TModel> GetById(Guid id);
        Task<IEnumerable<TModel>> GetAll();
        Task<int> SaveChanges();
    }
}
