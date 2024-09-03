using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers;
[Route("api/auth")]
[ApiController]
public class AuthAPIController : Controller
{
    private readonly IAuthService _authService;
    protected ResponseDto _response;
    public AuthAPIController(IAuthService authService)
    {
        _authService = authService;
        _response = new();

    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
    {
        string errorMessage = await _authService.Register(model);
        if (!string.IsNullOrEmpty(errorMessage))
        {
            _response.IsSuccess = false;
            _response.Message = errorMessage;
            BadRequest(_response);
        }

        return Ok(_response);
    }

    [HttpPost("login")]
    public IActionResult Login()
    {
        return Ok();
    }
}