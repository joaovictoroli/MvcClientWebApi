using Api.Data;
using Microsoft.EntityFrameworkCore;
using Projeto.BLL.Model;

namespace Api.Services
{
    public class FriendsService : IFriendsService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public FriendsService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Friend> AddAsync(Friend post)
        {
            await _applicationDbContext.AddAsync(post);
            await _applicationDbContext.SaveChangesAsync();
            return post;
        }

        public async Task<Friend> DeleteAsync(int id)
        {
            var post = await _applicationDbContext.Friends.FirstOrDefaultAsync(p => p.Id == id);

            if (post == null) { return null; }

            _applicationDbContext.Remove(post);
            await _applicationDbContext.SaveChangesAsync();
            return post;
        }

        public async Task<IEnumerable<Friend>> GetAllAsync()
        {
            return await _applicationDbContext.Friends.ToListAsync();
        }

        public async Task<Friend> GetAsync(int id)
        {
            return await _applicationDbContext.Friends.FirstOrDefaultAsync(p => p.Id == id);

        }

        public async Task<Friend> UpdateAsync(int id, Friend friend)
        {
            var existingFriend = await _applicationDbContext.Friends.FirstOrDefaultAsync(p => p.Id == id);

            if (existingFriend == null) { return null; }

            existingFriend.Name = friend.Name;
            existingFriend.LastName = friend.LastName;
            existingFriend.Email = friend.Email;
            existingFriend.PhoneNumber = friend.PhoneNumber;
            existingFriend.BirthDate = friend.BirthDate;

            await _applicationDbContext.SaveChangesAsync();
            return existingFriend;

        }
    }
}
