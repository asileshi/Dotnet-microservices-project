using Mango.Web.Models;
using Mango.Web.Service;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    // GET
    public async Task<IActionResult> ProductIndex()
    {
        
        List<ProductDto>? list = new();
        ResponseDto response = await _productService.GetAllProductsAsync();
        if (response != null && response.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
        }
        else
        {
            TempData["error"] = response?.Message;
        }

        return View(list);

    }
    public async Task<IActionResult> ProductCreate()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ProductCreate(ProductDto productDto)
    {
        if (ModelState.IsValid)
        {
            ResponseDto response = await _productService.CreateProductAsync(productDto);
            //return NotFound(r);
            if (response != null && response.IsSuccess)
            {
                
                TempData["success"] = "Product created successfully";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
        }

        TempData["error"] = "the product is not valid";
        return View(productDto);
    }
    
    public async Task<IActionResult> ProductUpdate(int id)
    {
        ResponseDto? response = await _productService.GetProductByIdAsync(id);
        if (response != null && response.IsSuccess)
        {
            ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            return View(model);
        }
        else
        {
            TempData["error"] = response?.Message;
        }
        return NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> ProductUpdate(ProductDto productDto)
    {
        ResponseDto? response = await _productService.UpdateProductAsync(productDto);
        if (response != null && response.IsSuccess)
        {
            TempData["success"] = "Coupon deleted successfully";
            return RedirectToAction(nameof(ProductIndex)); 
        }
        else
        {
            TempData["error"] = response?.Message;
        }
        
        return NotFound(productDto);
    }
    
    
    public async Task<IActionResult> ProductDelete(int id)
    {
        
        ResponseDto? response = await _productService.GetProductByIdAsync(id);
        if (response != null && response.IsSuccess)
        {
            ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            return View(model);
        }
        else
        {
            TempData["error"] = response?.Message;
        }
        return NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> ProductDelete(ProductDto productDto)
    {
        ResponseDto? response = await _productService.DeleteProductAsync(productDto.ProductId);
        if (response != null && response.IsSuccess)
        {
            TempData["success"] = "Coupon deleted successfully";
            return RedirectToAction(nameof(ProductIndex)); 
        }
        else
        {
            TempData["error"] = response?.Message;
        }
        
        return NotFound(productDto);
    }
}

