using AMMDotNetCoreTrainning.Domain.Features.MiniKpay;
using AMMDotNetCoreTrainning.Domain.Features.MiniKpay.Models;
using AMMDotNetTrainning.MiniKpay.API.Endpoints;
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
    public class PersonController : BaseContorller
    {
        private readonly PersonService _personService;
        private readonly MiniKpayService _kpayService;
        private readonly AppDbContext _db;          //for development purpose

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
            try
            {
                var list = _db.TblPeople.AsNoTracking().ToList();
                return Ok(list);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{mobileNo}")]
        public async Task<IActionResult> GetPersonByMobile(string mobileNo)
        {

            try
            {
                var person = await _personService.GetPersonByMobileNo(mobileNo);
                return Excute<ResultPersonResponseModel>(person);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewAccount(TblPerson person)
        {

            try
            {
                var newUser = await _personService.CreateAccount(person);
                return Excute<ResultPersonResponseModel>(newUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{mobileNo}/{pin}")]
        public async Task<IActionResult> UpdateDetails(string mobileNo, string pin, TblPerson person)
        {

            try
            {
                Result<ResultPersonResponseModel> model = new Result<ResultPersonResponseModel>();
                bool? result = await _kpayService.CheckPin(mobileNo, pin);
                if (result is null || result == false)
                {
                    model = Result<ResultPersonResponseModel>.Error("Wrong Password or User Not Found!");
                    goto Result;
                }

                model = await _personService.UpdatePerson(mobileNo, person);

            Result:
                return Excute<ResultPersonResponseModel>(model);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{mobileNo}/{pin}")]
        public async Task<IActionResult> Delete(string mobileNo, string pin)
        {

            try
            {
                Result<ResultPersonResponseModel> model = new Result<ResultPersonResponseModel>();
                bool? result = await _kpayService.CheckPin(mobileNo, pin);
                if (result is null || result == false)
                {
                    model = Result<ResultPersonResponseModel>.Error("Wrong Password or User Not Found!");
                    goto Result;
                }

                model = await _personService.DeactivatePerson(mobileNo);


            Result:
                return Excute<ResultPersonResponseModel>(model);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
