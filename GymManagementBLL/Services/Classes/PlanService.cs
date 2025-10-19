using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.UnitOFWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    internal class PlanService : IPlanService
    {
        private readonly UnitOfWork _unitOfWork;

        public PlanService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var Plans = _unitOfWork.GenericRepository<Plan>().GetAll();
            if(Plans is null || !Plans.Any()) return [];
            return Plans.Select(P => new PlanViewModel()
            {
                Id = P.Id,
                Name = P.Name,
                Description = P.Description,
                DurationDays = P.DurationDays,
                Price = P.Price,
                IsActive=P.IsActives,
            });
        }

        public PlanViewModel? GetPlanById(int PlanId)
        {
            var Plan = _unitOfWork.GenericRepository<Plan>().GetById(PlanId);
            if(Plan is null ) return null;
            return new PlanViewModel()
            {
                Id = Plan.Id,
                Name = Plan.Name,
                Description = Plan.Description,
                DurationDays = Plan.DurationDays,
                Price = Plan.Price,
                IsActive = Plan.IsActives,
            };
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int PlanId)
        {
            var Plan = _unitOfWork.GenericRepository<Plan>().GetById(PlanId);
            if(Plan is null || Plan.IsActives == false ||HasActiveMemberShips(PlanId)) return null;
            return new UpdatePlanViewModel()
            {
                Description = Plan.Description,
                DurationDays= Plan.DurationDays,
                Price = Plan.Price,
                PlanName = Plan.Name,
            };

        }
        public bool UpdatePlan(int PlanId, UpdatePlanViewModel updatedPlan)
        {
            var PlanRepo = _unitOfWork.GenericRepository<Plan>();
            var Plan = PlanRepo.GetById(PlanId);
            if (Plan is null || HasActiveMemberShips(PlanId)) return false;
            try
            {
                (Plan.Description, Plan.DurationDays, Plan.Price, Plan.UpdatedAt)
                        = (updatedPlan.Description, updatedPlan.DurationDays, Plan.Price, DateTime.Now);
                PlanRepo.Update(Plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch 
            {

                return false;
            }

        }
        // SoftDelete
        public bool ToggleStatus(int PlanId)
        {
            var PlanRepo = _unitOfWork.GenericRepository<Plan>();
            var Plan = PlanRepo.GetById(PlanId);
            if(Plan is null || HasActiveMemberShips(PlanId)) return false; 
            Plan.IsActives = Plan.IsActives==true ?false :true;
          
            // if (Plan.IsActives)
           //     Plan.IsActives = false;
           // Plan.IsActives =true;
           Plan.UpdatedAt = DateTime.Now;
            try
            {
                PlanRepo.Update(Plan);
                return _unitOfWork.SaveChanges()>0;
            }
            catch 
            {

                return false ;
            }
        }

        #region HelperMethod
        private bool HasActiveMemberShips(int PlanId)
        {
            var ActiveMermbersips = _unitOfWork.GenericRepository<MemberPlan>()
                .GetAll(X=>X.Id == PlanId && X.Status =="Active"); 
            return ActiveMermbersips.Any();
        }

        #endregion

    }
}
