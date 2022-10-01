using quiz.Data;
using quiz.Entities;

namespace quiz.Repositories;

public class McqOptionRepository : GenericRepository<McqOption>, IMcqOptionRepository
{
    public McqOptionRepository(ApplicationDbContext context)
        : base(context) { }
}