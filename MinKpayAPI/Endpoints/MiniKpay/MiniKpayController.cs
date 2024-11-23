using AMMDotNetCoreTrainning.Domain.Features.MiniKpay;
using AMMDotNetCoreTrainning.Domain.Features.MiniKpay.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniKPay.Database.Models;
using System.Text.RegularExpressions;

namespace AMMDotNetTrainning.MiniKpay.API.Endpoints.MiniKpay
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiniKpayController : BaseContorller
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
            var balance = _kpayService.BalanceCheck(mobileNo);

            return Excute(balance);
        }

        [HttpPatch("{mobileNo}")]
        public IActionResult ChangePin(string mobileNo, string oldPin, string newPin)
        {
            var success = _kpayService.ChangePin(mobileNo, oldPin, newPin);
            return Excute(success);
        }

        [HttpPost("deposit/{mobileNo}/{amount}")]
        public IActionResult Deposit(string mobileNo, string pin, long amount)
        {
            var model = _kpayService.Deposit(mobileNo, amount, pin);
            return Excute(model);
        }

        [HttpPost("withdraw/{mobileNo}/{amount}")]
        public IActionResult Withdraw(string mobileNo, string pin, long amount)
        {

            var model = _kpayService.Withdraw(mobileNo, amount, pin);
            return Excute(model);
        }

        [HttpPost("{fromMobileNo}/{toMobileNo}/{amount}")]
        public IActionResult Transfer(string fromMobileNo, string toMobileNo, string pin, long amount)
        {
            var model = _kpayService.Tansfer(fromMobileNo, toMobileNo, amount, pin);
            return Excute(model);
        }

        [HttpGet("history/{mobileNo}")]
        public IActionResult GetHistory(string mobileNo)
        {
            var history = _historyService.GetHistoryByPerson(mobileNo);
            return Excute(history);
        }
    }
}
