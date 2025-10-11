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
    internal class MemberRepository : IMemberRepository
    {
        //private DbContext dbContext { get; set; } = new GymDbContext();
        private readonly GymDbContext _dbContext = new GymDbContext();

        public int Add(Member member)
        {
            _dbContext.Members.Add(member); // Added Local
            return _dbContext.SaveChanges(); // Return Row affected
        }

        public int Delete(int id)
        {
            var Member = _dbContext.Members.Find(id); 
            //Search Locally if Not Find Search Remote
            if (Member is null) return 0;
            _dbContext.Members.Remove(Member);
            return _dbContext.SaveChanges();
        }

        public Member? GetById(int id)
        {
            return _dbContext.Members.Find(id);
        }

        // public IEnumerable<Member> GetAll() =>  _dbContext.Members.ToList();
        public IEnumerable<Member> GetAll()
        {
            return _dbContext.Members.ToList();
        }
        public int Update(Member member)
        {
            _dbContext.Members.Update(member);
            return _dbContext.SaveChanges();
        }
    }
}
