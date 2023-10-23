using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Web.HttpApiServices;
using Web.Models;
using Web.Models.Dtos;

namespace Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        public AuthenticationController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInDto dto)
        {

            var response =
              await _httpApiService.PostData<ResponseBody<UserItem>>("/user/token", JsonSerializer.Serialize(dto));



            if (response.Status == 200)
            {

                return Json(new { response.Item, StatusTexts = response.StatusTexts });
            }
            else
            {
                //Anonim Nesne
                return Json(new { IsSuccess = false, StatusTexts = response.StatusTexts });
            }
        }


        public async Task<IActionResult> SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Admin");
        }
    }
}

