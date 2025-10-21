using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementDAL.UnitOFWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.UnitOFWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repository = new();
        private readonly GymDbContext _dbContext;

        public UnitOfWork(GymDbContext dbContext)
        {
            _dbContext =dbContext;
        }
        public IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : BaseEntity, new()
        {
           var EntityType = typeof(TEntity);
            if (_repository.TryGetValue(EntityType, out var Repo))
                 // Try Get Value of Key EntityType   
                 // Want To Return Repo
                 // Repo Define By var Must Make Casting
                return (IGenericRepository<TEntity>)Repo;
            // var NewRepo = new IGenericRepository<TEntity>(GymDbContext dbContext); ××
            var newRepo = new GenericRepository<TEntity>(_dbContext);
            _repository[EntityType] = newRepo;
            return newRepo;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
  
