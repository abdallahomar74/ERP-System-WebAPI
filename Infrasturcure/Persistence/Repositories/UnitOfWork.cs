using DomainLayer.Contracts;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork(ErpDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<Type, Object> _repositories = [];
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            var typeName = typeof(TEntity);
            if (_repositories.TryGetValue(typeName, out object? value))
                return (IGenericRepository<TEntity>)value;
            else
            {
                var Repo = new GenericRepository<TEntity>(_dbContext);
                _repositories[typeName] = Repo;
                return Repo;
            }
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
