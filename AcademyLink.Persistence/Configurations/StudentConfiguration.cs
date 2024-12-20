using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AcademyLink.Domain.Entities;

namespace AcademyLink.Persistence.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
