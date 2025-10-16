using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly GymDbContext _dbContext;
        
        public GenericRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(TEntity entity)
        {
           // _dbContext.Add(entity);
           _dbContext.Set<TEntity>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(int Id)
        {
            var Element = _dbContext.Set<TEntity>().Find(Id);
            //Search Locally if Not Find Search Remote
            if (Element is null) return 0;
            _dbContext.Set<TEntity>().Remove(Element);
            return _dbContext.SaveChanges();
        }
        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
        {
            if (condition is null)
            {
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();
            }
            else 
                return _dbContext.Set<TEntity>().AsNoTracking().Where(condition).ToList();
        }

        //{
        //return _dbContext.Set<TEntity>().AsNoTracking().ToList();
        //}

        public TEntity? GetById(int Id) => _dbContext.Set<TEntity>().Find(Id);


        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
