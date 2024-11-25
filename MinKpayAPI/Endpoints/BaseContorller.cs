using AMMDotNetCoreTrainning.Domain.Features.MiniKpay;
using AMMDotNetCoreTrainning.Domain.Features.MiniKpay.Models;
using Azure;
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

                if (response.ResponseType == EnumResponseType.Error)
                {
                    return StatusCode(417, model);
                }

                return Ok(model);
            }

            return StatusCode(500, "Internal Server! (You Haven't Added a Response Type, Dev)");
        }

        [NonAction]
        public IActionResult Excute<T>(Result<T> model)
        {
            if (model.IsValidationError)
            {
                return BadRequest(model);
            }

            if (model.IsServerError)
            {
                return StatusCode(500, model);
            }

            if (model.IsNormalError)
            {
                return NotFound(model);
            }

            if (model.IsNormalError)
            {
                return StatusCode(417, model);
            }

            return Ok(model);
        }
    }
}
