using Microsoft.AspNetCore.Mvc;
using Web.HttpApiServices;
using Web.Models;
using Web.Models.Dtos;

namespace Web.Controllers
{
    public class BankController : Controller
    {

        private readonly IHttpApiService _httpApiService;
        public BankController(IWebHostEnvironment webHostEnvironment, IHttpApiService httpApiService)
        {

            _httpApiService = httpApiService;
        }
        public async Task<IActionResult> Index()
        {
            var token = Request.Headers.Authorization.FirstOrDefault();

            var response = await _httpApiService.GetData<ResponseBody<List<BankDto>>>("/Bank/MultipleGet",token:token);
            return View(response.Item);
        }
    }
}
