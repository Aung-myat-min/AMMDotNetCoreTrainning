using AMMDotNetCoreTrainning.Domain.Features.MiniKpay;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniKPay.Database.Models;
using System.Text.RegularExpressions;

namespace MinKpay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiniKpayController : ControllerBase
    {
        private readonly MiniKpayService _kpayService;
        private readonly PersonService _personService;
        private readonly HistoryService _historyService;

        public MiniKpayController()
        {
            _kpayService = new MiniKpayService();
            _personService = new PersonService();
            _historyService = new HistoryService();
        }

        [HttpGet("{mobileNo}")]
        public IActionResult GetBalance(string mobileNo)
        {
            long? balance = _kpayService.BalanceCheck(mobileNo);

            if (balance is null)
            {
                return NotFound("User Not Found!");
            }

            return Ok(balance);
        }

        [HttpPatch("{mobileNo}/{oldPin}")]
        public IActionResult ChangePin(string mobileNo, string oldPin, string newPin)
        {
            bool? success = _kpayService.ChangePin(mobileNo, oldPin, newPin);
            if (success is null)
            {
                return NotFound("User Not Found!");
            }
            else if (success == false)
            {
                return BadRequest("Wrong Pin");
            }

            return Ok("Pin Changed Successfully!");
        }

        [HttpPost("{mobileNo}/{pin}/{amount}")]
        public IActionResult Deposit(string mobileNo, string pin, long amount)
        {
            if (amount < 0)
            {
                return BadRequest("Balance can't be equal to or less than 0.");
            }

            bool? isSuccess = _kpayService.Deposit(mobileNo, amount, pin);
            if (isSuccess is null)
            {
                return NotFound("User Not Found!");
            }
            if (isSuccess == false)
            {
                return BadRequest("Wrong Pin");
            }

            return Ok($"You have added {amount} to your account!");
        }

        [HttpPost("{mobileNo}/{pin}/{amount}")]
        public IActionResult Withdraw(string mobileNo, string pin, long amount)
        {
            if (amount < 0)
            {
                return BadRequest("Balance can't be equal to or less than 0.");
            }

            bool? isSuccess = _kpayService.Withdraw(mobileNo, amount, pin);
            if (isSuccess is null)
            {
                return NotFound("User Not Found!");
            }
            if (isSuccess == false)
            {
                return BadRequest("Wrong Pin");
            }

            return Ok($"You have withdrawed {amount} to your account!");
        }

        [HttpPost("{fromMobileNo}/{toMobileNo}/{pin}/{amount}")]
        public IActionResult Transfer(string fromMobileNo, string toMobileNo, string pin, long amount)
        {
            if (amount < 0)
            {
                return BadRequest("Balance can't be equal to or less than 0.");
            }

            bool? isSuccess = _kpayService.Tansfer(fromMobileNo, toMobileNo, amount, pin);
            if (isSuccess is null)
            {
                return NotFound("User Not Found!");
            }
            if (isSuccess == false)
            {
                return BadRequest("Wrong Pin");
            }

            return Ok($"You have transferred {amount}!");
        }
    }
}
