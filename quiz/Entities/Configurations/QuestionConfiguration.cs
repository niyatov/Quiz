using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace quiz.Entities.Configurations;
public class QuestionConfiguration : EntityBaseConfiguration<Question>
{
    public override void Configure(EntityTypeBuilder<Question> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.Title).HasColumnType("nvarchar(50)");
        builder.Property(b => b.Description).HasColumnType("nvarchar(250)");
        builder.Property(b => b.TimeAllowed).IsRequired();
    }
}