using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
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
            return View();
        }

        public async Task<IActionResult> GetBankData(BankDto.FilterForm form)
        {
            var token = Request.Headers.Authorization.FirstOrDefault();

            var response = await _httpApiService.PostData<ResponseBody<List<BankDto.Response>>>("/Bank/MultipleGet", JsonSerializer.Serialize(form), token: token);

            // JSON veri olarak dönüş yap
            return Json(response);
        }

    }
}
