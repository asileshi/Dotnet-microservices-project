using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Model;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Service;

public class AuthService:IAuthService
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;

    }
    public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
    {
        ApplicationUser user = new()
        {
            UserName = registrationRequestDto.Email,
            Email = registrationRequestDto.Email,
            NormalizedEmail = registrationRequestDto.Email.ToUpper(),
            Name = registrationRequestDto.Name,
            PhoneNumber = registrationRequestDto.PhoneNumber
        };
        try
        {
            var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
            if (result.Succeeded)
            {
                var userToreturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);
                UserDto userDto = new()
                {
                    Email = userToreturn.Email,
                    ID = userToreturn.Id,
                    Name = userToreturn.Name,
                    PhoneNumber = userToreturn.PhoneNumber
                };
                return "";
            }
            else
            {
                return result.Errors.FirstOrDefault().Description;
            }

        }
        catch (Exception e)
        {
        }
        return "Error encountered";


    }

    public async Task<LoginRequestDto> Login(LoginRequestDto loginRequestDto)
    {
        throw new NotImplementedException();
    }
}