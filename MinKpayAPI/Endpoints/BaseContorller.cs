using AMMDotNetCoreTrainning.Domain.Features.MiniKpay;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AMMDotNetTrainning.MiniKpay.API.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseContorller : ControllerBase
    {
        [NonAction]
        public IActionResult Excute(object model)
        {
            JObject obj = JObject.Parse(JsonConvert.SerializeObject(model));
            if (obj.ContainsKey("ResponseModel"))
            {
                BaseResponseModel response = JsonConvert.DeserializeObject<BaseResponseModel>(obj["ResponseModel"]!.ToString()!)!;

                if (response.ResponseType == EnumResponseType.ValidationError)
                {
                    return BadRequest(model);
                }

                if (response.ResponseType == EnumResponseType.ServerError)
                {
                    return StatusCode(500, model);
                }

                if (response.ResponseType == EnumResponseType.NotFound)
                {
                    return NotFound(model);
                }

                return Ok(model);
            }

            return StatusCode(500, "Internal Server! (You Haven't Added a Response Type, Dev)");
        }
    }
}
