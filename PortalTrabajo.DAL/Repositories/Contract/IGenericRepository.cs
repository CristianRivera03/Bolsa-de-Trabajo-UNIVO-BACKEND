using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace PortalTrabajo.DAL.Repositories.Contract
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<TModel> Get(Expression<Func<TModel, bool>> filter);
        Task<TModel> Get(Expression<Func<TModel, bool>> filter, params Expression<Func<TModel, object>>[] includes);
        Task<TModel> GetById(object id);
        Task<bool> Exists(Expression<Func<TModel, bool>> filter);
        Task<IEnumerable<TModel>> GetAll(Expression<Func<TModel, bool>> filter = null);
        Task<IEnumerable<TModel>> GetAll(Expression<Func<TModel, bool>> filter = null, params Expression<Func<TModel, object>>[] includes);
        Task<TModel> Create(TModel model);
        Task<bool> Update(TModel model);
        Task<bool> SoftDelete(TModel model);
        Task<bool> HardDelete(TModel model);
        Task<(IEnumerable<TModel> Data, int TotalRecords)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<TModel, bool>> filter = null);
        Task<(IEnumerable<TModel> Data, int TotalRecords)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<TModel, bool>> filter = null,
            params Expression<Func<TModel, object>>[] includes);
        Task<bool> RemoveRange(IEnumerable<TModel> entities);
        Task<bool> AddRange(IEnumerable<TModel> entities);
        IQueryable<TModel> Query(Expression<Func<TModel, bool>> filter = null);
    }
}
