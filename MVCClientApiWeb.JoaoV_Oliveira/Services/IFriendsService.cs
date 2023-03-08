using Api.Data;
using Projeto.BLL.Model;

namespace Api.Services
{
    public interface IFriendsService
    {
        Task<IEnumerable<Friend>> GetAllAsync();
        Task<Friend> GetAsync(int id);
        Task<Friend> AddAsync(Friend friend);
        Task<Friend> UpdateAsync(int id, Friend friend);
        Task<Friend> DeleteAsync(int id);
    }
}
