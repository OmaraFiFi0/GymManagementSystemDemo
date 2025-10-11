﻿using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Configurations
{
    internal class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(N => N.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(D=>D.Description)
                .HasColumnType("varchar")
                .HasMaxLength(200);

            builder.Property(P => P.Price)
                .HasPrecision(10, 2);

            builder.ToTable(Tb =>
            {
                Tb.HasCheckConstraint("PlanDurationsDaysCheck", "DurationDays Between 1 and 365");
            });
        }
    }
}
