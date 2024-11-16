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
        public IActionResult GetPersonByMobile(string mobile)
        {
            var person = _personService.GetPersonByMobileNo(mobile);
            if (person == null)
            {
                return NotFound("Person Not Found!");
            }
            return Ok(person);
        }

        [HttpPost()]
        public IActionResult CreateNewAccount(TblPerson person)
        {
            string mobilePattern = @"^(09|01)[0-9]{6,9}$";
            string pinPattern = @"^[0-9]{6}$";
            if (person == null)
            {
                return BadRequest("Person can't be empty!");
            }
            if (String.IsNullOrEmpty(person.FullName) || person.FullName.Length < 4)
            {
                return BadRequest("Full name should be 4 characters minimum.");
            }
            if (String.IsNullOrEmpty(person.MobileNo) || !Regex.IsMatch(person.MobileNo, mobilePattern))
            {
                return BadRequest("Mobile Number should be \n- 8 digits mimum\n- 11 digits maximum\n- And, should starts from 01 or 09");
            }
            if (String.IsNullOrEmpty(person.Pin) || !Regex.IsMatch(person.Pin, pinPattern))
            {
                return BadRequest("A Pin should be a combination of 6 digits");
            }
        }
    }
}
