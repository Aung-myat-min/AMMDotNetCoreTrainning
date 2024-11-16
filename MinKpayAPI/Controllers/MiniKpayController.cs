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
            if (success == false)
            {
                return BadRequest("Wrong Pin");
            }

            return Ok("Pin Changed Successfully!");
        }

        [HttpPost("{mobileNo}/{pin}/{balance}")]
        public IActionResult Deposit(string mobileNo, string pin, long balance)
        {
            if (balance < 0)
            {
                return BadRequest("Balance can't be equal to or less than 0.");
            }

            bool? isPinCorrect = _kpayService.CheckPin(mobileNo, pin);
            if (isPinCorrect is null)
            {
                return NotFound("User Not Found!");
            }
            if (isPinCorrect == false)
            {
                return BadRequest("Wrong Pin");
            }


        }
    }
}
