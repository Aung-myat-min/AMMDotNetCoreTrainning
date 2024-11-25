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

        public Result<ResultPersonResponseModel> IsPersonDetailValid(TblPerson person)
        {

            Result<ResultPersonResponseModel> result = new Result<ResultPersonResponseModel>();

            if (person == null)
            {
                result = Result<ResultPersonResponseModel>.ValidationError("Person can't be empty!");
                goto Result;
            }
            if (String.IsNullOrEmpty(person.FullName) || person.FullName.Length < 4)
            {
                result = Result<ResultPersonResponseModel>.ValidationError("Full name should be 4 characters minimum.");
                goto Result;
            }
            if (!String.IsNullOrEmpty(person.MobileNo) && !Regex.IsMatch(person.MobileNo, mobilePattern))
            {
                result = Result<ResultPersonResponseModel>.ValidationError("Mobile Number should be \n- 8 digits mimum\n- 11 digits maximum\n- And, should starts from 01 or 09");
                goto Result;
            }
            if (!String.IsNullOrEmpty(person.Pin) && !Regex.IsMatch(person.Pin, pinPattern))
            {
                result = Result<ResultPersonResponseModel>.ValidationError("A Pin should be a combination of 6 digits");
                goto Result;
            }

        Result:
            return result;
        }

        public async Task<Result<ResultPersonResponseModel>> CreateAccount(TblPerson person)
        {
            Result<ResultPersonResponseModel> response = new Result<ResultPersonResponseModel>();

            response = IsPersonDetailValid(person);
            if (response.IsValidationError)
            {
                goto Result;
            }

            var newPerson = await _db.TblPeople.AddAsync(person);
            await _db.SaveChangesAsync();

            ResultPersonResponseModel resultPerson = new ResultPersonResponseModel
            {
                Person = person
            };
            response = Result<ResultPersonResponseModel>.Success("Person Created Successfully!", resultPerson);

        Result:
            return response;
        }

        public async Task<Result<ResultPersonResponseModel>> GetPersonByMobileNo(string MobileNo)
        {
            Result<ResultPersonResponseModel> response = new Result<ResultPersonResponseModel>();
            var person = await _db.TblPeople.AsNoTracking().Where(x => x.DeleteFalg == false && x.MobileNo == MobileNo).FirstOrDefaultAsync();
            if (person is null)
            {
                response = Result<ResultPersonResponseModel>.NotFound("Person Not Found!");
                goto Result;
            }

            ResultPersonResponseModel resultPerson = new ResultPersonResponseModel
            {
                Person = person
            };
            response = Result<ResultPersonResponseModel>.Success("Person Found!", resultPerson);

        Result:
            return response;
        }

        public async Task<Result<ResultPersonResponseModel>> UpdatePerson(string MobileNo, TblPerson person)
        {
            var item = await GetPersonByMobileNo(MobileNo);
            if (item.IsError)
            {
                goto Result;
            }

            //Validation and Updating Steps
            TblPerson targetedPerson = item.Data.Person;
            if (person == null)
            {
                item = Result<ResultPersonResponseModel>.ValidationError("Person can't be empty!");
                goto Result;
            }
            if (!String.IsNullOrEmpty(person.FullName))
            {
                if (person.FullName.Length < 4)
                {
                    item = Result<ResultPersonResponseModel>.ValidationError("Full name should be 4 characters minimum.");
                    goto Result;
                }
                else
                {
                    targetedPerson.FullName = person.FullName;
                }
            }
            if (!String.IsNullOrEmpty(person.MobileNo))
            {
                if (Regex.IsMatch(person.MobileNo, mobilePattern))
                {
                    targetedPerson.MobileNo = person.MobileNo;
                }
                else
                {
                    item = Result<ResultPersonResponseModel>.ValidationError("Mobile Number should be \n- 8 digits mimum\n- 11 digits maximum\n- And, should starts from 01 or 09");
                    goto Result;
                }
            }
            if (!String.IsNullOrEmpty(person.Pin))
            {
                if (Regex.IsMatch(person.Pin, pinPattern))
                {
                    targetedPerson.Pin = person.Pin;
                }
                else
                {
                    item = Result<ResultPersonResponseModel>.ValidationError("A Pin should be a combination of 6 digits");
                    goto Result;
                }
            }
            if (person.Balance.HasValue && person.Balance > 0)
            {
                targetedPerson.Balance = person.Balance;
            }

            _db.Entry(targetedPerson).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            ResultPersonResponseModel result = new ResultPersonResponseModel
            {
                Person = person
            };
            item = Result<ResultPersonResponseModel>.Success("Successfully Updated!", result);

        Result:
            return item;
        }

        public async Task<Result<ResultPersonResponseModel>> DeactivatePerson(string MobileNo)
        {
            var person = await GetPersonByMobileNo(MobileNo);
            if (person.IsError)
            {
                goto Result;
            }

            TblPerson targetedPerson = person.Data.Person;
            targetedPerson.DeleteFalg = true;
            _db.Entry(targetedPerson).State = EntityState.Modified;
            int result = await _db.SaveChangesAsync();

            if (result > 0)
            {
                person = Result<ResultPersonResponseModel>.Success("Person Deactivated!");
                goto Result;
            }

            person = Result<ResultPersonResponseModel>.ServerError("Internal Server Error!");

        Result:
            return person;
        }
    }
}
