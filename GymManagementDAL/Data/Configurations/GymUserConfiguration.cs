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
    internal class GymUserConfiguration<T>: IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(N => N.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(E=>E.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100);
            builder.Property(P=>P.Email)
                .HasColumnType("varchar")
                .HasMaxLength(11);

            builder.ToTable(Tb =>
            {
                Tb.HasCheckConstraint("EmailValidFormatConstraint", "Email Like '_%@_%._%'");
                Tb.HasCheckConstraint("PhoneValidEgpConstraint", "Phone Like '01[0125]%' and Phone not Like '%[^0-9]%'");
            });
            builder.HasIndex(E => E.Email).IsUnique(); 
            builder.HasIndex(P=>P.Phone).IsUnique();

            builder.OwnsOne(A => A.Address, AddressBuiler =>
            {
                AddressBuiler.Property(S => S.Street)
                .HasColumnType("varchar")
                .HasMaxLength(30)
                .HasColumnName("Street");

                AddressBuiler.Property(C => C.City)
                .HasColumnType("varchar")
                .HasMaxLength(30)
                .HasColumnName("City");
                
                AddressBuiler.Property(BN=>BN.BulidingNumber)
                .HasColumnName("BuilderNumber");
            });
        }
    }
}
