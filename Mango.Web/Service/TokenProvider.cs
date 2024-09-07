using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service;

public class TokenProvider:ITokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public void SetToken(string token)
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie, token);
        
    }

    public string? GetToken()
    {
        string? token = null;
        bool? hasToken = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(SD.TokenCookie, out token);
        if (hasToken is true)
        {
            return token;
        }

        return null;
    }

    public void ClearToken()
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenCookie);
    }
}