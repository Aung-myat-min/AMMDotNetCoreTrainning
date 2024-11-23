using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMMDotNetCoreTrainning.Domain.Features.MiniKpay
{
    public class BaseResponseModel
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public EnumResponseType ResponseType { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsError { get { return !IsSuccess; } }

        public static BaseResponseModel Success(string responseCode, string responseDescription)
        {
            return new BaseResponseModel
            {
                ResponseCode = responseCode,
                ResponseDescription = responseDescription,
                ResponseType = EnumResponseType.Success,
                IsSuccess = true
            };
        }

        public static BaseResponseModel ValidationError(string responseCode, string responseDescription)
        {
            return new BaseResponseModel
            {
                ResponseCode = responseCode,
                ResponseDescription = responseDescription,
                ResponseType = EnumResponseType.ValidationError,
                IsSuccess = false
            };
        }

        public static BaseResponseModel NotFound(string responseCode, string responseDescription)
        {
            return new BaseResponseModel
            {
                ResponseCode = responseCode,
                ResponseDescription = responseDescription,
                ResponseType = EnumResponseType.NotFound,
                IsSuccess = false
            };
        }

        public static BaseResponseModel Error(string responseCode, string responseDescription)
        {
            return new BaseResponseModel
            {
                ResponseCode = responseCode,
                ResponseDescription = responseDescription,
                ResponseType = EnumResponseType.Error,
                IsSuccess = false
            };
        }

        public static BaseResponseModel ServerError(string responseCode, string responseDescription)
        {
            return new BaseResponseModel
            {
                ResponseCode = responseCode,
                ResponseDescription = responseDescription,
                ResponseType = EnumResponseType.ServerError,
                IsSuccess = false
            };
        }
    }
}

public enum EnumResponseType
{
    None,
    Success,
    ValidationError,
    NotFound,
    Error,
    ServerError
}
