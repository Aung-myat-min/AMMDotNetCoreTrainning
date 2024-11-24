using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMMDotNetCoreTrainning.Domain.Features.MiniKpay.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public bool IsError { get { return !IsSuccess; } }
        public bool IsValidationError { get { return Type == EnumResponseType.ValidationError; } }
        public bool IsServerError { get { return Type == EnumResponseType.ServerError; } }
        public bool IsNormalError { get { return Type == EnumResponseType.Error; } }
        private EnumResponseType Type { get; set; }
        public T Data { get; set; }
        public string message { get; set; }

        public static Result<T> Success(string message, T data = default)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Type = EnumResponseType.Success,
                Data = data,
                message = message
            };
        }

        public static Result<T> Error(string message, T data = default)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Type = EnumResponseType.Error,
                Data = data,
                message = message
            };
        }

        public static Result<T> ValidationError(string message, T data = default)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Type = EnumResponseType.ValidationError,
                Data = data,
                message = message
            };
        }

        public static Result<T> NotFound(string message, T data = default)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Type = EnumResponseType.NotFound,
                Data = data,
                message = message
            };
        }

        public static Result<T> ServerError(string message, T data = default)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Type = EnumResponseType.ServerError,
                Data = data,
                message = message
            };
        }
    }
}
