using AMMDotNetTrainning.RestAPI2.Models.RefitInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace AMMDotNetTrainning.RestAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickAPileController : ControllerBase
    {
        private readonly IPickAPile _refitClient;

        public PickAPileController(IPickAPile refitClient)
        {
            _refitClient = refitClient;
        }

        [HttpGet("")]
        public async Task<IActionResult> getPiles()
        {
            var response = await _refitClient.GetPiles();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getAPile(int id)
        {
            var response = await _refitClient.GetAPile(id);
            return Ok(response);
        }
    }
}
