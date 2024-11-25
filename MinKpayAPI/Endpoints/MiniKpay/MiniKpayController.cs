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
        public async Task<IActionResult> GetBalance(string mobileNo)
        {
            var balance = await _kpayService.BalanceCheck(mobileNo);

            return Excute<ResultPersonResponseModel>(balance);
        }

        [HttpPatch("{mobileNo}")]
        public async Task<IActionResult> ChangePin(string mobileNo, string oldPin, string newPin)
        {
            var success = await _kpayService.ChangePin(mobileNo, oldPin, newPin);
            return Excute<ResultPersonResponseModel>(success);
        }

        [HttpPost("deposit/{mobileNo}/{amount}")]
        public async Task<IActionResult> Deposit(string mobileNo, string pin, long amount)
        {
            var model = await _kpayService.Deposit(mobileNo, amount, pin);
            return Excute<ResultHistoryResponseModel>(model);
        }

        [HttpPost("withdraw/{mobileNo}/{amount}")]
        public async Task<IActionResult> Withdraw(string mobileNo, string pin, long amount)
        {

            var model = await _kpayService.Withdraw(mobileNo, amount, pin);
            return Excute<ResultHistoryResponseModel>(model);
        }

        [HttpPost("{fromMobileNo}/{toMobileNo}/{amount}")]
        public async Task<IActionResult> Transfer(string fromMobileNo, string toMobileNo, string pin, long amount)
        {
            var model = await _kpayService.Tansfer(fromMobileNo, toMobileNo, amount, pin);
            return Excute<ResultHistoryResponseModel>(model);
        }

        [HttpGet("history/{mobileNo}")]
        public async Task<IActionResult> GetHistory(string mobileNo)
        {
            var history = await _historyService.GetHistoryByPerson(mobileNo);
            return Excute<List<ExtendedHistory>>(history);
        }
    }
}
