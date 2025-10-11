﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    internal class MemberSession:BaseEntity
    {
        public bool IsAttended { get; set; }

        #region MemberSession - Member
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        #endregion
        #region MemberSession - Session
        public int SessionId { get; set; }
        public Session Sessions { get; set; } = null!;
        #endregion
    }
}
