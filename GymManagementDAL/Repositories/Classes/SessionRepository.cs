﻿using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    internal class SessionRepository : ISessionRepository
    {
        private readonly GymDbContext _dbContext = new GymDbContext();
        public int Add(Session session)
        {
            _dbContext.Sessions.Add(session);   
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var session = _dbContext.Sessions.Find(id);
            if (session is null) return 0; 
            _dbContext.Sessions.Remove(session);
            return _dbContext.SaveChanges();   
        }

        public IEnumerable<Session> GetAll()
        {
            return _dbContext.Sessions.ToList();
        }

        public Session? GetById(int id)
        {
            return _dbContext.Sessions.Find(id);
        }

        public int Update(Session session)
        {
            _dbContext.Sessions.Update(session);
            return _dbContext.SaveChanges();
        }
    }
}
