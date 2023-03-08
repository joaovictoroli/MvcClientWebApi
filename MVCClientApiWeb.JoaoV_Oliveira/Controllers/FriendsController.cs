using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.BLL.Model;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : Controller
    {
        private readonly IFriendsService _friendsService;


        public FriendsController(IFriendsService friendsService)
        {
            _friendsService = friendsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friend>>> GetFriends()
        {
            return Ok(await _friendsService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Friend>> GetFriend(int id)
        {
            var friend = await _friendsService.GetAsync(id);

            if (friend == null)
            {
                return NotFound();
            }

            return friend;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Friend>> PostFriend(Friend friend)
        {
            friend = await _friendsService.AddAsync(friend);            

            return CreatedAtAction("GetFriend", new { id = friend.Id }, friend);
        }

        [Authorize]
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteFriend(int id)
        {
            var friend = await _friendsService.DeleteAsync(id);
            if (friend == null)
            {
                return NotFound();
            }

            return Ok(friend);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Friend>> PutFriend(int id, Friend friend)
        {
            if (id != friend.Id)
            {
                return BadRequest();
            }

            var existingFriend = await _friendsService.UpdateAsync(id, friend);

            if (existingFriend == null) { return null; }

            return CreatedAtAction("GetFriend", new { id = friend.Id }, friend);
        }
    }
}
