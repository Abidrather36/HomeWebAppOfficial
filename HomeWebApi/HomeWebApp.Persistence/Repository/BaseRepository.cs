using HomeWebApp.Application.Abstraction.IRepositoryService;
using HomeWebApp.Domain.Entities;
using HomeWebApp.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HomeWebApp.Persistence.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel, new()
    {
        private readonly HomeWebApiDbContext context;

        public BaseRepository(HomeWebApiDbContext context)
        {
            this.context = context;
        }

        public Task<int> AddRangeAsync(List<T> models)
        {
            throw new NotImplementedException();
        }

        /*  public async Task<int> DeleteAsync(Guid Id)
          {
              *//*var model=GetByIdAsync(Id);  only problems here is calling database again 
            await  Task.Run(()=> context.Set<T>().Remove(model));*//*
              var res = new T();
              res.Id = Id;
              await Task.Run(()=> context.Set<T>().Remove(res));
             return await context.SaveChangesAsync();
          }*/
        public async Task<int> DeleteAsync(Guid Id)
        {
            // Find the entity by its Id
            var entity = await context.Set<T>().FindAsync(Id);

            if (entity == null)
            {
                // Entity not found, nothing to delete
                return 0;
            }

            // Remove the entity
            context.Set<T>().Remove(entity);

            // Save changes to the database
            return await context.SaveChangesAsync();
        }

        public Task<int> DeleteRangeAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteRangeAsync(List<T> models)
        {
            throw new NotImplementedException();
        }

        /*  public async Task<int> DeleteAsync(Guid Id)
          {
              var entity = new T { Id = Id };

              context.Entry(entity).State = EntityState.Deleted;

              return await context.SaveChangesAsync();
          }*/


        public async  Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> expression)
        {
           return await Task.Run(() => context.Set<T>().Where(expression));
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
           return await context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.Run(() => context.Set<T>());
        }

        public Task<IEnumerable<T>> GetAllAsync(int pageNo, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async  Task<T?> GetByIdAsync(Guid Id)
        {
          return  await  context.Set<T>().FindAsync(Id);
        }

        public async  Task<int> InsertAsync(T model)
        {
             context.Set<T>().Add(model);
             return  await context.SaveChangesAsync();
            
        }

        public async  Task<bool> IsExistsAsync(Expression<Func<T, bool>> expression)
        {
          return  await context.Set<T>().AnyAsync(expression);
        }

        public async  Task<int> UpdateAsync(T model)
        {
           await Task.Run(() => context.Set<T>().Update(model));
           return await context.SaveChangesAsync();
        }
    }
}
