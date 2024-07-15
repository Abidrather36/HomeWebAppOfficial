using HomeWebApp.Domain.Entities;
using System.Linq.Expressions;

namespace HomeWebApp.Application.Abstraction.IRepositoryService
{
    public  interface IBaseRepository<T> where T :BaseModel,new()
    {
        #region sync versions

      /*  int Add<T>(T model) where T : class;

        int Update<T>(T model) where T : class;

        int Delete<T>(T model)where T : class;


        IEnumerable<T> GetAll<T>() where T : class;
        

        T GetById<T>(Guid Id) where T : class;


        IEnumerable<T> FindBy<T>(Expression <Func<T,bool>> expression) where T : class;*/

        #endregion


        #region Async versions

        Task<int> InsertAsync(T model);


        Task<int> UpdateAsync(T model);


        Task<int> DeleteAsync(Guid Id);


        Task<T> GetByIdAsync(Guid Id);


        Task<IEnumerable<T>> GetAllAsync();


        Task<IEnumerable<T>> GetAllAsync(int pageNo,int pageSize);


        Task<IEnumerable<T>> FindByAsync(Expression<Func<T,bool>> expression) ;


        Task<int> AddRangeAsync(List<T> models);


        Task<int> DeleteRangeAsync(List<Guid> ids);


        Task<int> DeleteRangeAsync(List<T> models);


        Task<T> FirstOrDefaultAsync(Expression<Func<T,bool>> expression);


        Task<bool> IsExistsAsync(Expression<Func<T,bool>> expression);  

        #endregion
    }
}
