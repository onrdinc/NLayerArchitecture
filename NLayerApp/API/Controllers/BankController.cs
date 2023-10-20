using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.Dtos;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {

        private readonly IBankBs _bankBs;
        public BankController(IBankBs bankBs)
        {
            _bankBs = bankBs;
        }
        [HttpPost]

        public async Task<IActionResult> Post([FromForm] BankDto.Form form)
        {
            var response = await _bankBs.Add(form,0);
            return await Task.FromResult(StatusCode(response.StatusCode, response));

        }

        [HttpGet]
        [Route("MultipleGet")]
        public async Task<IActionResult> MultipleGet()
        {
            var response = await _bankBs.MultipleGet(null, 0);
            return await Task.FromResult(StatusCode(response.StatusCode, response));

        }
    }
}
