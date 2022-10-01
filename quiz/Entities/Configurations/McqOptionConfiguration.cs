using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace quiz.Entities.Configurations;

public class McqOptionConfiguration : IEntityTypeConfiguration<McqOption>
{
    public virtual void Configure(EntityTypeBuilder<McqOption> builder)
    {
        builder.HasKey(k => new {k.Id, k.QuestionId});
        builder.Property(p => p.IsTrue).IsRequired(true);
        builder.Property(p => p.Content).IsRequired();
    }
}