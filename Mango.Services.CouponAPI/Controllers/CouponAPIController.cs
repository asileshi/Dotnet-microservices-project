using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Mango.Services.CouponAPI.Controllers;

[Route("api/coupon")]
[ApiController]
[Authorize]
public class CouponAPIController : Controller
{
    private readonly AppDbContext _db;
    private ResponseDto _response;
    private IMapper _mapper;
    public CouponAPIController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _response = new ResponseDto();
        _mapper = mapper;
    }

    [HttpGet]
    public ResponseDto Get()
    {
        try
        {
            IEnumerable<Coupon> objList = _db.Coupons.ToList();
            _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);

        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }

    [HttpGet]
    [Route("{id:int}")]
    public ResponseDto Get(int id)
    {
        try
        {
            Coupon obj = _db.Coupons.First(u=>u.CouponId == id);
            _response.Result = _mapper.Map<CouponDto>(obj);
        }
        catch (Exception e)
        { 
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }

    [HttpGet]
    [Route("GetByCode/{code}")]
    public ResponseDto GetByCode(string code)
    {
        try
        {
            Coupon obj = _db.Coupons.First(u => u.CouponCode == code);
            _response.Result = _mapper.Map<CouponDto>(obj);

        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }
    
    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public ResponseDto Post([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon obj = _mapper.Map<Coupon>(couponDto);
            _db.Coupons.Add(obj);
            _db.SaveChanges();

            _response.Result = _mapper.Map<CouponDto>(obj); 
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }

    [HttpPut]
    [Route("Update")]
    [Authorize(Roles = "ADMIN")]
    public ResponseDto Put([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon obj = _mapper.Map<Coupon>(couponDto);
            _db.Update(obj);
            _db.SaveChanges();
            _response.Result = _mapper.Map<CouponDto>(obj);

        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }

    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "ADMIN")]
    public ResponseDto Delete(int id)
    {
        try
        {
            Coupon obj = _db.Coupons.First(u => u.CouponId == id);
            _db.Coupons.Remove(obj);
            _db.SaveChanges();
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }
    
    
}