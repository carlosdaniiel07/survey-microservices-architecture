using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Survey.Microservices.Architecture.Domain.Entities.v1;

namespace  Survey.Microservices.Architecture.Infrastructure.Data.Configurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.ToTable("User");
            builder.HasIndex(entity => entity.Email).IsUnique().HasDatabaseName("IX_User_Email");
            builder.Property(entity => entity.Name).HasMaxLength(100).IsRequired();
            builder.Property(entity => entity.Email).HasMaxLength(120).IsRequired();
            builder.Property(entity => entity.Password).HasMaxLength(300).IsRequired();
            builder.Property(entity => entity.Active);
        }
    }
}
