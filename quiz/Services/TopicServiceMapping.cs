
using quiz.Models.Topic;

namespace quiz.Services;
public partial class TopicService

{

    private static Entities.Topic ToEntity(TopicCreated topic)
    {
        return new Entities.Topic()
        {
            Name = topic.Name,
            Description = topic.Description,
            Difficulty = ToEntity(topic.Difficulty),
        };
    }
    private static Entities.EDifficulty ToEntity(EDifficulty difficulty)
    => difficulty switch
    {
        EDifficulty.Beginner => Entities.EDifficulty.Beginner,
        EDifficulty.Intermediate => Entities.EDifficulty.Intermediate,
        _=> Entities.EDifficulty.Beginner
    };

    private static Topic ToModel(Entities.Topic topic)
    {
        return new Topic()
        {
            Id = topic.Id,
            Name = topic.Name,
            Description = topic.Description,
            Difficulty = topic.Difficulty.ToString(),
            CreatedAt = topic.CreatedAt,
            UpdatedAt = topic.UpdatedAt,
        };
    }
}