using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface IMemberService
    {
        //IEnumerable<Member> ==> This The Entity 
        IEnumerable<MemberViewModel> GetAllMembers();

        bool CreateMember(CreateMemberViewModel CreatedMember);

        MemberViewModel? GetMemberDetails(int memberId);

       HealthRecordViewModel? GetMemberHealthRecord(int memberId);

        MemberDataToUpdate? GetMemberToUpdate (int memberId);

        bool UpdateMember(int Id, MemberDataToUpdate updateMember); 

        bool RemoveMember(int MemberId);
    }
}
