using Logic.User.Interfaces;
using Logic.User.Model;
using Microsoft.AspNetCore.Mvc;

namespace ProfileApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserLogicManager _userLogicManager;

    public UserController(IUserLogicManager userLogicManager)
    {
        _userLogicManager = userLogicManager;
    }

    [HttpPost]
    public async Task<ActionResult> CreateUserAsync([FromBody] UserModel user)
    {
        if (await _userLogicManager.CreateUserAsync(user))
        {
            return Ok("User created.");
        }
        return BadRequest("User didn't create.");
    }

    [HttpGet]
    public async Task<ActionResult<UserModel>> GetUserAsync([FromBody] Guid userId)
    {
        return Ok(await _userLogicManager.GetUserByIdAsync(userId));
    }
}