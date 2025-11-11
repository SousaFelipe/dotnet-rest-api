using Microsoft.AspNetCore.Mvc;
using Api.Service.Interfaces;


namespace Api.Core.Controllers;


[Route("users")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    private IUserService UserService { get; init; } = userService;


    [HttpGet]
    public async Task<ActionResult> Get()
    {
        return Ok();
    }


    [HttpPost]
    public async Task<ActionResult> Post()
    {
        return Ok();
    }


    [HttpPut]
    public async Task<ActionResult> Put()
    {
        return Ok();
    }


    [HttpDelete]
    public async Task<ActionResult> Delete()
    {
        return Ok();
    }
}
