using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PortalTrabajo.DAL.Repositories.Contract;
using PortalTrabajo.DAL.DBConext;

namespace PortalTrabajo.DAL.Repositories.Implementation
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly PortalTrabajoDbContext _dbContext;
        private readonly DbSet<TModel> _dbSet;

        public GenericRepository(PortalTrabajoDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TModel>();
        }

        public async Task<TModel> Get(Expression<Func<TModel, bool>> filter)
        {
            return await _dbSet.FirstOrDefaultAsync(filter);
        }

        public async Task<TModel> Get(Expression<Func<TModel, bool>> filter, params Expression<Func<TModel, object>>[] includes)
        {
            IQueryable<TModel> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task<TModel> GetById(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> Exists(Expression<Func<TModel, bool>> filter)
        {
            return await _dbSet.AnyAsync(filter);
        }

        public async Task<IEnumerable<TModel>> GetAll(Expression<Func<TModel, bool>> filter = null)
        {
            IQueryable<TModel> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TModel>> GetAll(Expression<Func<TModel, bool>> filter = null, params Expression<Func<TModel, object>>[] includes)
        {
            IQueryable<TModel> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<TModel> Create(TModel model)
        {
            await _dbSet.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<bool> Update(TModel model)
        {
            _dbSet.Update(model);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> SoftDelete(TModel model)
        {
            // Requiere que el modelo tenga una propiedad "Activo" u otra similar
            var property = model.GetType().GetProperty("Activo");
            if (property != null && property.CanWrite)
            {
                property.SetValue(model, false);
                _dbSet.Update(model);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            
            // Si no tiene la propiedad, fallback a hard delete
            return await HardDelete(model);
        }

        public async Task<bool> HardDelete(TModel model)
        {
            _dbSet.Remove(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<(IEnumerable<TModel> Data, int TotalRecords)> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<TModel, bool>> filter = null)
        {
            IQueryable<TModel> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            int totalRecords = await query.CountAsync();
            var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (data, totalRecords);
        }

        public async Task<(IEnumerable<TModel> Data, int TotalRecords)> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<TModel, bool>> filter = null, params Expression<Func<TModel, object>>[] includes)
        {
            IQueryable<TModel> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            int totalRecords = await query.CountAsync();
            var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (data, totalRecords);
        }

        public async Task<bool> RemoveRange(IEnumerable<TModel> entities)
        {
            _dbSet.RemoveRange(entities);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddRange(IEnumerable<TModel> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public IQueryable<TModel> Query(Expression<Func<TModel, bool>> filter = null)
        {
            IQueryable<TModel> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }
    }
}
