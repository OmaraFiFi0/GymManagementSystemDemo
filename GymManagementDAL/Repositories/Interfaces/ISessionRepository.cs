using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface ISessionRepository
    {
        // Get All Session
        IEnumerable<Session> GetAll();
        // GetById
        Session? GetById(int id);
        // Add Session
        int Add(Session session);
        // Update Session
        int Update(Session session);
        // Delete Session
        int Delete(int id );
    }
}
