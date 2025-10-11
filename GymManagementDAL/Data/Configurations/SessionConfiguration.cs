using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Configurations
{
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(Tb =>
            {
                Tb.HasCheckConstraint("SessionCapacityCheck", "Capacity Between 1 and 25");
                Tb.HasCheckConstraint("SessionEndDateChec", "EndDate > StartDate");
            });

            builder.HasOne(S => S.SessionCategory)
                .WithMany(X => X.Sessions)
                .HasForeignKey(X => X.CategoryId); // FK Not Needed

            builder.HasOne(X => X.SessionTrainer)
                .WithMany(X => X.TrainerSessions)
                .HasForeignKey(X => X.TrainerId); // FK Not Needed [By Conventional]
        }
    }
}
