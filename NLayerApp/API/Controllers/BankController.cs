using API.Helper;
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
            return await Task.FromResult(StatusCode(response.Status, response));

        }

        [HttpPost]
        [Route("MultipleGet")]
        public async Task<IActionResult> MultipleGet([FromBody] BankDto.FilterForm form)
        {
            var response = await _bankBs.MultipleGet(form, 0);
            return await Task.FromResult(StatusCode(response.Status, response));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _bankBs.Delete(id, 0);

            return await Task.FromResult(StatusCode(response.Status, response));
        }
    }
}
