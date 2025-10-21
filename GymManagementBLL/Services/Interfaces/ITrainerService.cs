using GymManagementBLL.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();
        bool CreateTrainer(CreateTrainerViewModel CreatedTrainer);

        TrainerViewModel? GetTrainerDetails(int TrainerId);

        UpdateTrainerViewModel? GetTrainerToUpdate(int TrainerId);
        bool UpdateTrainerDetails(UpdateTrainerViewModel UpdatedTrainer , int TrainerId);

        bool RemoveTrainer(int TrainerId);
    }
}
