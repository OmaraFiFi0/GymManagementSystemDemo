using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementDAL.UnitOFWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    internal class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateMember(CreateMemberViewModel CreatedMember)
        {
            try
            {
                if (IsEmailExist (CreatedMember.Email) || IsPhoneExist(CreatedMember.Phone)) return false;
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
                 _unitOfWork.GenericRepository<Member>().Add(member) ;// Added Local
                 return _unitOfWork.SaveChanges()>0;
            }
            catch(Exception )
            {
                return false;
            }
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members= _unitOfWork.GenericRepository<Member>().GetAll();
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

        public MemberViewModel? GetMemberDetails(int memberId)
        {
            var member = _unitOfWork.GenericRepository<Member>().GetById(memberId);
            if (member is null) return null;
            var ViewModels = new MemberViewModel()
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Photo = member.Photo,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Address = $"{member.Address.BulidingNumber}-{member.Address.Street}-{member.Address.City}"
                // PlanName = ????
                // MemberShipStartDate = ????
                // MemberShip End Date = ???? 
            };
            var ActiveMemberShip = _unitOfWork.GenericRepository<MemberPlan>()
                .GetAll(X => X.MemberId == memberId && X.Status == "Active")
                .FirstOrDefault();
            if (ActiveMemberShip is not null)
            {
                ViewModels.MemberShipStartDate = ActiveMemberShip.CreatedAt.ToShortDateString();
                ViewModels.MemberShipEndDate= ActiveMemberShip.EndDate.ToShortDateString();
                // Plan Name = ????
                var Plan = _unitOfWork.GenericRepository<Plan>().GetById(ActiveMemberShip.PlanId);
                ViewModels.PlanName = Plan?.Name;
            }
            return ViewModels;
        }
            
        public HealthRecordViewModel? GetMemberHealthRecord(int memberId)
        {
            var MemberHealthRecord = _unitOfWork.GenericRepository<HealthRecord>().GetById(memberId);
            if (MemberHealthRecord is null) return null;
            return new HealthRecordViewModel
            {
                Height = MemberHealthRecord.Height,
                Weight = MemberHealthRecord.Weight,
                BloodType = MemberHealthRecord.BloodType,
                Note = MemberHealthRecord.Note
            };

        }

        public MemberDataToUpdate? GetMemberToUpdate(int memberId)
        {
            var Member = _unitOfWork.GenericRepository<Member>().GetById(memberId);
            if (Member is null) return null;
            return new MemberDataToUpdate
            {
                Email = Member.Email,
                Name = Member.Name,
                Phone = Member.Phone,
                BuildingNumber = Member.Address.BulidingNumber,
                City = Member.Address.City,
                Street = Member.Address.Street,
            };
        }

        
        public bool UpdateMember(int Id, MemberDataToUpdate updateMember)
        {
            try
            {
                var Repo = _unitOfWork.GenericRepository<Member>();
                var MemberData = Repo.GetById(Id);
                if (MemberData is null) return false;
                if(IsEmailExist(updateMember.Email) || IsPhoneExist(updateMember.Phone)) return false;
                MemberData.Email = updateMember.Email;
                MemberData.Name = updateMember.Name;
                MemberData.Phone = updateMember.Phone;
                MemberData.Address.BulidingNumber = updateMember.BuildingNumber;
                MemberData.Address.Street = updateMember.Street;
                MemberData.Address.City = updateMember.City;
                MemberData.CreatedAt = DateTime.Now;

                 Repo.Update(MemberData) ;
                return _unitOfWork.SaveChanges()>0;
            }
            catch 
            {

                return false;
            }
        }

        public bool RemoveMember(int MemberId)
        {
            var MemberRepo = _unitOfWork.GenericRepository<Member>();

            var Member = MemberRepo.GetById(MemberId);
            if (Member is null) return false;
            var HasActiveMemberSession = _unitOfWork.GenericRepository<MemberSession>()
                .GetAll(X => X.MemberId == MemberId && X.Sessions.StartDate > DateTime.Now)
                .Any();
            if (HasActiveMemberSession) return false;

            var MemberShipRepo = _unitOfWork.GenericRepository<MemberPlan>();
            var MemberShips = MemberShipRepo.GetAll(X=>X.MemberId==MemberId);
            try
            {
                if (MemberShips.Any())
                {
                    foreach( var membership in MemberShips)
                    {
                        MemberShipRepo.Delete(membership);
                    }
                }
                 MemberRepo.Delete(Member);
                return _unitOfWork.SaveChanges()>0;
            }
            catch
            {
                return false;
            }
        }


        #region HelperMethod

        private bool IsEmailExist(string  email)
        {
            var Result = _unitOfWork.GenericRepository<Member>()
                .GetAll(X=>X.Email == email)
                .Any();
            return Result;
        }
        private bool IsPhoneExist(string phone)
        {
            return _unitOfWork.GenericRepository<Member>()
                .GetAll(X => X.Phone == phone)
                .Any();

        }

        #endregion
    }
}
 