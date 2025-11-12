using Microsoft.AspNetCore.Mvc;
using Api.Domain.Core;
using Api.Service.Dtos;
using Api.Service.Interfaces;


namespace Api.Domain.Controllers;


[Route("users")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    private IUserService UserService { get; init; } = userService;


    [HttpPost]
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
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult Find(long id)
    {
        var user = UserService.FindUser(id);
        return Ok(user);
    }


    [HttpGet("{page}/{size}")]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(long id)
    {
        return Ok();
    }
}
