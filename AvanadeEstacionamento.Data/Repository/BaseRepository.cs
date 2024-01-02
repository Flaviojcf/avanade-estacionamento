using AvanadeEstacionamento.Data.Context;
using AvanadeEstacionamento.Domain.Interfaces.Repository;
using AvanadeEstacionamento.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace AvanadeEstacionamento.Data.Repository
{
    public abstract class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : BaseModel, new()
    {

        #region Dependency Injection

        protected readonly MyDbContext Context;
        protected readonly DbSet<TModel> DbSet;

        #endregion

        #region Constructor

        public BaseRepository(MyDbContext context)
        {
            Context = context;
            DbSet = context.Set<TModel>();
        }

        #endregion

        #region Public Methods

        public virtual async Task<bool> Create(TModel model)
        {
            using var transaction = await Context.Database.BeginTransactionAsync();
            try
            {
                DbSet.Add(model);
                await SaveChanges();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            using var transaction = await Context.Database.BeginTransactionAsync();
            try
            {
                DbSet.Remove(new TModel { Id = id });
                await SaveChanges();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public virtual async Task<bool> Update(TModel model)
        {
            using var transaction = await Context.Database.BeginTransactionAsync();
            try
            {
                DbSet.Update(model);
                await SaveChanges();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public virtual async Task<IEnumerable<TModel>> GetAll() => await DbSet.ToListAsync();

        public virtual async Task<TModel> GetById(Guid id)
        {
            var result = await DbSet.FindAsync(id);

            return result;
        }

        public async Task<int> SaveChanges() => await Context.SaveChangesAsync();

        public void Dispose() => Context?.Dispose();

        #endregion
    }
}
