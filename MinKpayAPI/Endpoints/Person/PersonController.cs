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
            var list = _db.TblPeople.AsNoTracking().ToList();
            return Ok(list);
        }

        [HttpGet("{mobileNo}")]
        public IActionResult GetPersonByMobile(string mobileNo)
        {
            var person = _personService.GetPersonByMobileNo(mobileNo);
            return Excute<ResultPersonResponseModel>(person);
        }

        [HttpPost]
        public IActionResult CreateNewAccount(TblPerson person)
        {
            var newUser = _personService.CreateAccount(person);
            return Excute<ResultPersonResponseModel>(newUser);
        }

        [HttpPatch("{mobileNo}/{pin}")]
        public IActionResult UpdateDetails(string mobileNo, string pin, TblPerson person)
        {
            Result<ResultPersonResponseModel> model = new Result<ResultPersonResponseModel>();
            bool? result = _kpayService.CheckPin(mobileNo, pin);
            if (result is null || result == false)
            {
                model = Result<ResultPersonResponseModel>.Error("Wrong Password or User Not Found!");
                goto Result;
            }

            model = _personService.UpdatePerson(mobileNo, person);

        Result:
            return Excute<ResultPersonResponseModel>(model);
        }

        [HttpDelete("{mobileNo}/{pin}")]
        public IActionResult Delete(string mobileNo, string pin)
        {
            Result<ResultPersonResponseModel> model = new Result<ResultPersonResponseModel>();
            bool? result = _kpayService.CheckPin(mobileNo, pin);
            if (result is null || result == false)
            {
                model = Result<ResultPersonResponseModel>.Error("Wrong Password or User Not Found!");
                goto Result;
            }

            model = _personService.DeactivatePerson(mobileNo);


        Result:
            return Excute<ResultPersonResponseModel>(model);
        }
    }
}
