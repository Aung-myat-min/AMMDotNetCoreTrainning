using AMMDotNetCoreTrainning.Domain.Features.MiniKpay;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniKPay.Database.Models;
using System;
using System.Text.RegularExpressions;

namespace MinKpay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonService _personService;
        private readonly MiniKpayService _kpayService;
        private readonly AppDbContext _db;          //for development purpose
        private readonly string mobilePattern = @"^(09|01)[0-9]{6,9}$";
        private readonly string pinPattern = @"^[0-9]{6}$";

        public PersonController()
        {
            _personService = new PersonService();
            _kpayService = new MiniKpayService();
            _db = new AppDbContext();
        }

        //For Development Purpose
        [HttpGet]
        public IActionResult GetUsers()
        {
            var list = _db.TblPeople.AsNoTracking().ToList();
            return Ok(list);
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

        [HttpPost]
        public IActionResult CreateNewAccount(TblPerson person)
        {

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

            var newUser = _personService.CreateAccount(person);
            if (newUser == null)
            {
                return BadRequest("Person Creation Failed!");
            }

            return Ok(newUser);
        }

        [HttpPatch("{mobileNo}/{pin}")]
        public IActionResult UpdateDetails(string mobileNo, string pin, TblPerson person)
        {
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

            bool? result = _kpayService.CheckPin(mobileNo, pin);
            if (result is null || result == false)
            {
                return BadRequest("Invalid mobile no or pin code!");
            }

            var updatedUser = _personService.UpdatePerson(mobileNo, person);
            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        [HttpDelete("{mobileNo}/{pin}")]
        public IActionResult Delete(string mobileNo, string pin)
        {
            bool? result = _kpayService.CheckPin(mobileNo, pin);
            if (result is null || result == false)
            {
                return BadRequest("Invalid mobile no or pin code!");
            }

            var updatedUser = _personService.DeactivatePerson(mobileNo);
            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok("User Deactivated!");
        }
    }
}
