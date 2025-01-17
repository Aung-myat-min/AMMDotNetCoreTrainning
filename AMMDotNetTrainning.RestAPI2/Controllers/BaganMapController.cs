using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net.Http;

namespace AMMDotNetTrainning.RestAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaganMapController : ControllerBase
    {
        private readonly RestClient? _restSharp;

        public BaganMapController(RestClient? restSharp)
        {
            _restSharp = restSharp;
        }

        [HttpGet("")]
        public async Task<IActionResult> getMaps()
        {
            RestRequest request = new RestRequest("bagan-map", Method.Get );
            var response = await _restSharp.GetAsync(request);
            return Ok(response.Content);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getMap(string id)
        {
            RestRequest request = new RestRequest($"bagan-map/{id}", Method.Get);
            var response = await _restSharp.GetAsync(request);
            return Ok(response.Content);
        }

        [HttpGet("/detial/{id}")]
        public async Task<IActionResult> getMapDetial(string id)
        {
            RestRequest request = new RestRequest($"bagan-map/detail{id}", Method.Get);
            var response = await _restSharp.GetAsync(request);
            return Ok(response.Content);
        }
    }
}
