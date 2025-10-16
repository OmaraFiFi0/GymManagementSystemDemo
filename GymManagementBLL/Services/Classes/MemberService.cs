using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    internal class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> _memberRepository;
        // Not Ask CLR Creating Object From Service Without Make Register in Program.cs 
        // CLR Will Inject Address of Object In Constructor
        public MemberService(IGenericRepository<Member> memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public bool CreateMember(CreateMemberViewModel CreatedMember)
        {
            try
            {
                // IF Email Already Exist 
                var EmailExist = _memberRepository.GetAll(X => X.Email == CreatedMember.Email).Any();
                // IF Phone Already Exist
                var PhoneExist = _memberRepository.GetAll(X => X.Phone == CreatedMember.Phone).Any();
                // IF One of Them Exist Return False
                if (EmailExist || PhoneExist) return false;
                // IF Not Exist Create Member and Return True if Added Successfully
                //  _memberRepository.Add(); 
                // Take Entity Must Make Mapping From ViewModel To Entity
                var member = new Member
                {
                    Name = CreatedMember.Name,
                    Email = CreatedMember.Email,
                    Phone = CreatedMember.Phone,
                    Gender = CreatedMember.Gender,
                    DateOfBirth = CreatedMember.DateOfBirth,
                    Address = new Address
                    {
                        BulidingNumber = CreatedMember.BuildingNumber,
                        Street = CreatedMember.Street,
                        City = CreatedMember.City
                    },
                    HealthRecord = new HealthRecord
                    {
                        Height = CreatedMember.HealthRecordViewModel.Height,
                        Weight = CreatedMember.HealthRecordViewModel.Weight,
                        BloodType = CreatedMember.HealthRecordViewModel.BloodType,
                        Note = CreatedMember.HealthRecordViewModel.Note
                    }
                };
                return _memberRepository.Add(member) > 0;
            }
            catch(Exception )
            {
                return false;
            }
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members= _memberRepository.GetAll();
            // if (Members is null || !Members.Any()) return Enumerable.Empty<MemberViewModel>();
            if (Members is null || !Members.Any()) return [];
            var MemberViews = Members.Select(M => new MemberViewModel
            {
                Id = M.Id,
                Name = M.Name,
                Email = M.Email,
                Phone = M.Phone, 
                Photo = M.Photo,
                Gender = M.Gender.ToString()
            });
            return MemberViews;
        }
    }
}
