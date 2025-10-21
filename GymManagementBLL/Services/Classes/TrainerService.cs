using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.UnitOFWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    internal class TrainerService : ITrainerService
    {
        private readonly UnitOfWork _unitOfWork;

        public TrainerService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var Trainers = _unitOfWork.GenericRepository<Trainer>().GetAll();
            if (Trainers is null || !Trainers.Any()) return [];
            var TrainersView = Trainers.Select(T => new TrainerViewModel
            {
                Id = T.Id,
                Name = T.Name,
                Email = T.Email,
                Phone = T.Phone,
                Specialization = T.Specialties.ToString(),
            });
            return TrainersView;
        }

        public bool CreateTrainer(CreateTrainerViewModel CreatedTrainer)
        {
            var Repo = _unitOfWork.GenericRepository<Trainer>();
            try
            {
                if(IsEmailExsit(CreatedTrainer.Email) || IsPhoneExist(CreatedTrainer.Phone)) return false;
                var TrainerData = new Trainer()
                {
                    Name=CreatedTrainer.TrainerName,
                    Email=CreatedTrainer.Email,
                    Phone=CreatedTrainer.Phone,
                    DateOfBirth=CreatedTrainer.DateOfBirth,
                    Gender=CreatedTrainer.Gender,
                    Address = new Address
                    {
                        BulidingNumber = CreatedTrainer.BuildingNumber,
                        Street = CreatedTrainer.Street,
                        City = CreatedTrainer.City,
                    }
                };
                Repo.Add(TrainerData);
                return _unitOfWork.SaveChanges()>0;  
            } 
            catch 
            {

                return false;
            }
        }

       
        public TrainerViewModel? GetTrainerDetails(int TrainerId)
        {
            var Repo = _unitOfWork.GenericRepository<Trainer>();
            var Trainer = Repo.GetById(TrainerId);
            if(Trainer is null ) return null;
            return new TrainerViewModel
            {
                Email = Trainer.Email,
                Phone = Trainer.Phone,
                Name = Trainer.Name,
                Specialization = Trainer.Specialties.ToString()
            };
        }

        
        public UpdateTrainerViewModel? GetTrainerToUpdate(int TrainerId)
        {
            var Repo = _unitOfWork.GenericRepository<Trainer>();
            var Trainer =Repo.GetById(TrainerId); 
            if(Trainer is null ) return null;
            return new UpdateTrainerViewModel()
            {
                TrainerName = Trainer.Name, // For Display
                Email= Trainer.Email,
                Phone= Trainer.Phone,
                Street = Trainer.Address.Street,
                BuildingNumber = Trainer.Address.BulidingNumber,
                City = Trainer.Address.City, 
                
            };
        }
        public bool UpdateTrainerDetails(UpdateTrainerViewModel UpdatedTrainer, int trainerid)
        {
            var Repo = _unitOfWork.GenericRepository<Trainer>();
            var TrainerToUpdate = Repo.GetById(trainerid);
            if (TrainerToUpdate is null || IsEmailExsit(UpdatedTrainer.Email)||IsPhoneExist(UpdatedTrainer.Phone) ) return false;
            TrainerToUpdate.Email = UpdatedTrainer.Email;
            TrainerToUpdate.Phone =UpdatedTrainer.Phone;
            TrainerToUpdate.Address.BulidingNumber = UpdatedTrainer.BuildingNumber;
            TrainerToUpdate.Address.Street = UpdatedTrainer.Street;
            TrainerToUpdate.Address.City = UpdatedTrainer.City;
            TrainerToUpdate.Specialties = UpdatedTrainer.Specialties;
            TrainerToUpdate.UpdatedAt = DateTime.Now;
            return _unitOfWork.SaveChanges()>0;

            
        }
        public bool RemoveTrainer(int TrainerId)
        {
            var Repo = _unitOfWork.GenericRepository<Trainer>();
            var TrainerToRemove = Repo.GetById(TrainerId);
            if(TrainerToRemove is null || HasActiveSessions(TrainerId) ) return false ;
            Repo.Delete(TrainerToRemove);
            return _unitOfWork.SaveChanges() > 0;

        }

        #region HelperMethod
        private bool IsEmailExsit (string email)
        {
            var Result = _unitOfWork.GenericRepository<Trainer>()
                .GetAll(X => X.Email == email)
                .Any();
            return Result;
        }
        private bool IsPhoneExist(string phone)
        {
            return _unitOfWork.GenericRepository<Trainer>()
                .GetAll(X => X.Phone == phone)
                .Any();
        }

        private bool HasActiveSessions(int id)
        {
            var ActiveSession = _unitOfWork.GenericRepository<Session>()
                .GetAll(S=>S.TrainerId == id && S.StartDate > DateTime.Now)
                .Any();
            return ActiveSession;
        }

        

        #endregion
    }
}
