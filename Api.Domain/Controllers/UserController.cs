using Microsoft.AspNetCore.Mvc;
using Api.Domain.Helpers;
using Api.Service.Dtos;
using Api.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace Api.Domain.Controllers;


[Route("users")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    private IUserService UserService { get; init; } = userService;


    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] UserCreateDto newUser)
    {
        var createdUser = await UserService.CreateUser(newUser);
        return Ok(createdUser);
    }


    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Find(long id)
    {
        var user = await UserService.FindUser("Id", id);
        return Ok(user);
    }


    [HttpGet("{page}/{size}")]
    [Authorize]
    [ProducesResponseType(typeof(PagedResponse<UserResultDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult ReadPaged(int page, int size)
    {
        var users = UserService.ReadPagedUsers(page, size);
        return Ok(users);
    }


    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult Update(long id, [FromBody] UserUpdateDto userDto)
    {
        var updatedUser = UserService.UpdateUser(id, userDto);
        return Ok(updatedUser);
    }


    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(long id)
    {
        await UserService.DeleteUser(id);
        return Ok();
    }
}
