using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponAPIController : Controller
{
    private readonly AppDbContext _db;
    public CouponAPIController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public object Get()
    {
        try
        {
            IEnumerable<Coupon> objList = _db.Coupons.ToList();
            return objList;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}