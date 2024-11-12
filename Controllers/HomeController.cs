using CloudP3.Models;
using CloudP3.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CloudP3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CustomerService _customerService;
        private readonly BlobService _blobService;
        private readonly ProductService _productService;

        public HomeController(
            ILogger<HomeController> logger,
            CustomerService customerService,
            BlobService blobService,
            ProductService productService)
        {
            _logger = logger;
            _customerService = customerService;
            _blobService = blobService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var model = new CustomerProfile();
            return View(model);
        }

        public IActionResult Product()
        {
            var model = new ProductModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> StoreTableInfo(CustomerProfile profile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _customerService.InsertCustomerAsync(profile);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception occurred while submitting client info: {ex.Message}");
                }
            }
            return View("Index", profile);
        }

        [HttpPost]
        public async Task<IActionResult> UploadBlob(IFormFile imageFile)
        {
            if (imageFile != null)
            {
                try
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(memoryStream);
                        var imageData = memoryStream.ToArray();

                        await _blobService.InsertBlobAsync(imageData);
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception occurred while submitting image: {ex.Message}");
                }
            }
            else
            {
                _logger.LogError("No image file provided.");
            }

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> StoreProductInfo(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.InsertProductAsync(model);
                    return RedirectToAction("Product");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception occurred while submitting product info: {ex.Message}");
                }
            }
            return View("Product", model);
        }
    }
}