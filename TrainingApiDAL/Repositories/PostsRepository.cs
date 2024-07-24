using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApiDAL.Models;

namespace TrainingApiDAL.Repositories
{
    public interface IPostsRepository
    {
        public Task<Post> GetPostById(long id);
    }

    public class PostsRepository : IPostsRepository
    {
        private readonly TrainingTestDbContext _trainingTestDbContext;

        public PostsRepository(TrainingTestDbContext trainingTestDbContext)
        {
            _trainingTestDbContext = trainingTestDbContext;
        }

        public async Task<Post> GetPostById(long id)
        {
            var result = await _trainingTestDbContext.Posts.FindAsync(id);
            if (result == null) throw new Exception("Not Found");
            return result;
        }
    }
}
