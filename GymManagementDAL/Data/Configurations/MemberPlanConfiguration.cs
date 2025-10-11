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
    internal class MemberPlanConfiguration : IEntityTypeConfiguration<MemberPlan>
    {
        public void Configure(EntityTypeBuilder<MemberPlan> builder)
        {
            builder.Property(X => X.CreatedAt)
                .HasColumnName("StartDate")
                .HasDefaultValueSql("GETDATE()");

            builder.HasKey(X => new { X.MemberId, X.PlanId }); // Now The Member Can't Join This Plan Againe
            // builder.HasKey(X => new { X.MemberId, X.PlanId , X.CreatedAt }); // Composite PK
            builder.Ignore(X=>X.Id);
        }
    }
}
