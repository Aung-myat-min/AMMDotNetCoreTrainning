using AMMDotNetCoreTrainning.Domain.Features.MiniKpay.Models;
using Microsoft.EntityFrameworkCore;
using MiniKPay.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AMMDotNetCoreTrainning.Domain.Features.MiniKpay
{
    public class PersonService
    {
        private readonly AppDbContext _db;
        private readonly string mobilePattern = @"^(09|01)[0-9]{6,9}$";
        private readonly string pinPattern = @"^[0-9]{6}$";

        public PersonService()
        {
            _db = new AppDbContext();
        }

        public BaseResponseModel IsPersonDetailValid(TblPerson person)
        {

            BaseResponseModel baseResponseModel = new BaseResponseModel();

            if (person == null)
            {
                baseResponseModel = BaseResponseModel.ValidationError("555", "Person can't be empty!");
                goto Result;
            }
            if (String.IsNullOrEmpty(person.FullName) || person.FullName.Length < 4)
            {
                baseResponseModel = BaseResponseModel.ValidationError("556", "Full name should be 4 characters minimum.");
                goto Result;
            }
            if (!String.IsNullOrEmpty(person.MobileNo) && !Regex.IsMatch(person.MobileNo, mobilePattern))
            {
                baseResponseModel = BaseResponseModel.ValidationError("557", "Mobile Number should be \n- 8 digits mimum\n- 11 digits maximum\n- And, should starts from 01 or 09");
                goto Result;
            }
            if (!String.IsNullOrEmpty(person.Pin) && !Regex.IsMatch(person.Pin, pinPattern))
            {
                baseResponseModel = BaseResponseModel.ValidationError("558", "A Pin should be a combination of 6 digits");
                goto Result;
            }

        Result:
            return baseResponseModel;
        }

        public PersonResponseModel CreateAccount(TblPerson person)
        {
            PersonResponseModel response = new PersonResponseModel();

            response.ResponseModel = IsPersonDetailValid(person);
            if (response.ResponseModel.ResponseType == EnumResponseType.ValidationError)
            {
                response.Person = person;
                goto Result;
            }

            var newPerson = _db.TblPeople.Add(person);
            _db.SaveChanges();
            
            response.ResponseModel = BaseResponseModel.Success("001", "Person Created Successfully!");
            response.Person = person;
            

        Result:
            return response;
        }

        public PersonResponseModel GetPersonByMobileNo(string MobileNo)
        {
            PersonResponseModel response = new PersonResponseModel();
            var person = _db.TblPeople.AsNoTracking().Where(x => x.DeleteFalg == false && x.MobileNo == MobileNo).FirstOrDefault();
            if (person is null)
            {
                response.ResponseModel = BaseResponseModel.NotFound("404", "Person Not Found!");
                goto Result;
            }

            response.ResponseModel = BaseResponseModel.Success("001", "Person Found!");
            response.Person = person;

        Result:
            return response;
        }

        public PersonResponseModel UpdatePerson(string MobileNo, TblPerson person)
        {
            var item = GetPersonByMobileNo(MobileNo);
            if (item.ResponseModel.ResponseType != EnumResponseType.Success)
            {
                goto Result;
            }
            if (person == null)
            {
                item.ResponseModel = BaseResponseModel.ValidationError("555", "Person can't be empty!");
                goto Result;
            }
            if (!String.IsNullOrEmpty(person.FullName))
            {
                if (person.FullName.Length < 4)
                {
                    item.ResponseModel = BaseResponseModel.ValidationError("556", "Full name should be 4 characters minimum.");
                }
                else
                {
                    item.Person.FullName = person.FullName;
                }
            }
            if (!String.IsNullOrEmpty(person.MobileNo))
            {
                if (Regex.IsMatch(person.MobileNo, mobilePattern))
                {
                    item.Person.MobileNo = person.MobileNo;
                }
                else
                {
                    item.ResponseModel = BaseResponseModel.ValidationError("557", "Mobile Number should be \n- 8 digits mimum\n- 11 digits maximum\n- And, should starts from 01 or 09");
                }
            }
            if (!String.IsNullOrEmpty(person.Pin))
            {
                if (Regex.IsMatch(person.Pin, pinPattern))
                {
                    item.Person.Pin = person.Pin;
                }
                else
                {
                    item.ResponseModel = BaseResponseModel.ValidationError("558", "A Pin should be a combination of 6 digits");
                }
            }
            if (person.Balance.HasValue && person.Balance > 0)
            {
                item.Person.Balance = person.Balance;
            }

            _db.Entry(item.Person).State = EntityState.Modified;
            _db.SaveChanges();

            item.ResponseModel = BaseResponseModel.Success("001", "Successfully Updated!");
            item.Person = person;

        Result:
            return item;
        }

        public PersonResponseModel DeactivatePerson(string MobileNo)
        {
            var person = GetPersonByMobileNo(MobileNo);
            if (person.ResponseModel.ResponseType != EnumResponseType.Success)
            {
                goto Result;
            }

            person.Person.DeleteFalg = true;
            _db.Entry(person.Person).State = EntityState.Modified;
            int result = _db.SaveChanges();

            if (result < 0)
            {
                person.ResponseModel = BaseResponseModel.Success("001", "Person Deactivated!");
                goto Result;
            }

            person.ResponseModel = BaseResponseModel.ServerError("999", "Internal Server Error!");

        Result:
            return person;
        }
    }
}
