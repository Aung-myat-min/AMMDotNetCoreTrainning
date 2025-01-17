using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMMDotNetTrainning.RestAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public BirdsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("")]
        public async Task<IActionResult> getBirds()
        {
            var response = await _httpClient.GetAsync("birds");
            String responseStr = await response.Content.ReadAsStringAsync();
            return Ok(responseStr);
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> getBird(int id)
        {
            var response = await _httpClient.GetAsync($"birds/{id}");
            String responseStr = await response.Content.ReadAsStringAsync();
            return Ok(responseStr);
        }
    }
}
