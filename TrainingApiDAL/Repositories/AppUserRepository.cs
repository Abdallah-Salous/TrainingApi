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

        public Task<AppUser> AddUserAsync(AppUser user);

        Task<AppUser> GetUserByName(string userName);

    }
    public class AppUserRepository : IAppUserRepositry
    {
        private readonly TrainingTestDbContext _trainingTestDbContext;

        public AppUserRepository(TrainingTestDbContext trainingTestDbContext)
        {
            _trainingTestDbContext = trainingTestDbContext;
        }

        public async Task<AppUser> AddUserAsync(AppUser user)
        {
            var entityEntry = await _trainingTestDbContext.AppUsers.AddAsync(user);
            return entityEntry.Entity;
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

        public async Task<AppUser> GetUserByName(string userName)
        {
            var user = await _trainingTestDbContext.AppUsers.Where(user => string.Equals(user.Email, userName)).
                FirstOrDefaultAsync();
            if (user == null) throw new ArgumentNullException("User");
            return user;
        }
    }
}
