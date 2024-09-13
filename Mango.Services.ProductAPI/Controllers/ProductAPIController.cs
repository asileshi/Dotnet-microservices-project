using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Mango.Services.ProductAPI.Controllers;

[Route("api/coupon")]
[ApiController]
//[Authorize]
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
            IEnumerable<Product> objList = _db.Products.ToList();
            _response.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);
            
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
            Product obj = _db.Products.First(u=>u.ProductId == id);
            _response.Result = _mapper.Map<ProductDto>(obj);
        }
        catch (Exception e)
        { 
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }

    
    [HttpPost]
    //[Authorize(Roles = "ADMIN")]
    public ResponseDto Post([FromBody] ProductDto couponDto)
    {
        try
        {
            Product obj = _mapper.Map<Product>(couponDto);
            _db.Products.Add(obj);
            _db.SaveChanges();

            _response.Result = _mapper.Map<ProductDto>(obj); 
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
    //[Authorize(Roles = "ADMIN")]
    public ResponseDto Put([FromBody] ProductDto couponDto)
    {
        try
        {
            Product obj = _mapper.Map<Product>(couponDto);
            _db.Update(obj);
            _db.SaveChanges();
            _response.Result = _mapper.Map<ProductDto>(obj);

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
    //[Authorize(Roles = "ADMIN")]
    public ResponseDto Delete(int id)
    {
        try
        {
            Product obj = _db.Products.First(u => u.ProductId == id);
            _db.Products.Remove(obj);
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