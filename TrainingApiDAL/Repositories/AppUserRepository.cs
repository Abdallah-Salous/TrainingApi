using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApiDAL.Models;

namespace TrainingApiDAL.Repositories
{
    public interface IAppUserRepositry
    {
        public Task<List<AppUser>> GetAllUsersAsync();
        public Task<AppUser> GetUserWithPosts(long id);

    }
    public class AppUserRepository : IAppUserRepositry
    {
        private readonly TrainingTestDbContext _trainingTestDbContext;

        public AppUserRepository(TrainingTestDbContext trainingTestDbContext)
        {
            _trainingTestDbContext = trainingTestDbContext;
        }

        public Task<List<AppUser>> GetAllUsersAsync()
        {
            return _trainingTestDbContext.AppUsers.ToListAsync();
        }

        public Task<AppUser> GetUserWithPosts(long id)
        {
            return _trainingTestDbContext.AppUsers.
                Include(user => user.Posts).
                Where(user => user.Id == id).SingleOrDefaultAsync();
        }
    }
}
