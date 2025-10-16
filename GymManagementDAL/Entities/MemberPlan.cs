using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    public class MemberPlan:BaseEntity
    {
        // IF Member Will Pay And Will Start After Week For Example
        // Will Create Two Attrubute In Operations
        // IF Not Used CreatedAt That was Inherit From BaseEntity
        public DateTime EndDate { get; set; }
        // Read Only Property
        public string Status
        {
            get
            {
                if (EndDate >= DateTime.Now)
                    return "Expired";
                else
                    return "Active";
            }
        }


        #region MemberPlan - Member
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        #endregion

        #region MemberPlan - Plan
        public int PlanId { get; set; }
        public Plan Plan { get; set; } = null!;
        #endregion

    }
}
