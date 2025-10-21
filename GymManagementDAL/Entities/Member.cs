using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    public class Member:GymUser
    {
        // Join date == CreatedAt
        public string? Photo { get; set; }

        #region Member - HealthRecord 
        public HealthRecord HealthRecord { get; set; } = null!;
        #endregion

        #region Member - MemberPlan
        public ICollection<MemberPlan> MemberPlans { get; set; } = null!;
        #endregion

        #region Member - MemberSession
        public ICollection<MemberSession> MemberSessions { get; set; }= null!;
        #endregion
    }
}
