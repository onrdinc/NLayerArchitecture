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
        public BankController(IHttpApiService httpApiService)
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

            var response = await _httpApiService.PostData<ResponseBody<List<BankDto.Response>>>("/Bank/MultipleGet", JsonSerializer.Serialize(form), token);

            // JSON veri olarak dönüş yap
            return Json(response);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var token = Request.Headers.Authorization.FirstOrDefault();

            var response = await _httpApiService.DeleteData<ResponseBody<NoData>>($"/bank/{id}", token);

            if (response.Status == 200)
                return Json(new { IsSuccess = true, Message = response.StatusTexts });

            return Json(new { IsSuccess = false, Message = response.StatusTexts });
        }

        [HttpPost]
        public async Task<IActionResult> Save(BankDto.Form form)
        {
            var token = Request.Headers.Authorization.FirstOrDefault();


            var response = await _httpApiService.PostData<ResponseBody<BankDto.Response>>("/bank", JsonSerializer.Serialize(form), token);

            //ya da response.statuscode == 201
            if (response.Status.ToString().StartsWith("2"))
            {
                return Json(new { IsSuccess = true, Message = response.StatusTexts, response.Item });

            }
            else
            {
                return Json(new { IsSuccess = false, Message = response.StatusTexts });
            }
        }

    }
}
