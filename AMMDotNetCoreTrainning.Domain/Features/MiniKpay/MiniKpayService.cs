using AMMDotNetCoreTrainning.Domain.Features.MiniKpay.Models;
using Azure;
using MiniKPay.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMMDotNetCoreTrainning.Domain.Features.MiniKpay
{
    public class MiniKpayService
    {
        private readonly AppDbContext _db;
        private readonly PersonService _personService;
        private readonly HistoryService _historyService;

        public MiniKpayService()
        {
            _db = new AppDbContext();
            _personService = new PersonService();
            _historyService = new HistoryService();
        }

        public async Task<Result<ResultPersonResponseModel>> BalanceCheck(string MobileNo)
        {
            Result<ResultPersonResponseModel> response = new Result<ResultPersonResponseModel>();

            var person = await _personService.GetPersonByMobileNo(MobileNo);
            if (person.IsError)
            {
                response = person;
                goto Result;
            }

            ResultPersonResponseModel result = new ResultPersonResponseModel
            {
                Person = default,
                Balance = person.Data.Person.Balance,
            };
            response = Result<ResultPersonResponseModel>.Success("Here is your balance...", result);

        Result:
            return response;
        }

        public async Task<bool?> CheckPin(string MobileNo, string Pin)
        {
            var person = await _personService.GetPersonByMobileNo(MobileNo);
            if (person.Data is null)
            {
                return null;
            }

            var pin = person.Data.Person.Pin;
            if (String.Equals(pin, Pin))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Result<ResultPersonResponseModel>> ChangeMobileNo(string OldMobile, string NewMobile, string Pin)
        {
            Result<ResultPersonResponseModel> response = new Result<ResultPersonResponseModel>();
            var isPinCorrect = await CheckPin(OldMobile, Pin);
            if (isPinCorrect == false || isPinCorrect is null)
            {
                response = Result<ResultPersonResponseModel>.Error("Wrong Pin");
                goto Result;
            }

            var person = await _personService.GetPersonByMobileNo(OldMobile);
            if (person.IsError)
            {
                response = person;
                goto Result;
            }

            person.Data.Person.MobileNo = NewMobile;
            response = await _personService.UpdatePerson(OldMobile, person.Data.Person);

        Result:
            return response;
        }

        public async Task<Result<ResultPersonResponseModel>> ChangePin(string MobileNo, string OldPin, string NewPin)
        {
            Result<ResultPersonResponseModel> response = new Result<ResultPersonResponseModel>();

            var isPinCorrect = await CheckPin(MobileNo, OldPin);
            if (isPinCorrect == false || isPinCorrect is null)
            {
                response = Result<ResultPersonResponseModel>.Error("Wrong Pin");
                goto Result;
            }

            if (OldPin == NewPin)
            {
                response = Result<ResultPersonResponseModel>.Error("New Pin can't be the same with the old pin.");
                goto Result;
            }

            var person = await _personService.GetPersonByMobileNo(MobileNo);
            if (person.IsError)
            {
                response = person;
                goto Result;
            }

            person.Data.Person.Pin = NewPin;

            var updatedPeson = await _personService.UpdatePerson(MobileNo, person.Data.Person);
            if (updatedPeson.IsError)
            {
                updatedPeson.Data.Person = default;
                response = updatedPeson;
                goto Result;
            }

            response = Result<ResultPersonResponseModel>.Success("Pin Changed Successfully!");

        Result:
            return response;
        }

        public async Task<Result<ResultHistoryResponseModel>> Deposit(string MobileNo, long Amount, string Pin)
        {
            Result<ResultHistoryResponseModel> response = new Result<ResultHistoryResponseModel>();

            if (Amount < 0)
            {
                response = Result<ResultHistoryResponseModel>.ValidationError("Amount can't be less than or equal to 0.");
                goto Result;
            }

            var person = await _personService.GetPersonByMobileNo(MobileNo);
            if (person.IsError)
            {
                response = Result<ResultHistoryResponseModel>.NotFound("Person Not Found!");
                goto Result;
            }

            var isPinCorrect = await CheckPin(MobileNo, Pin);
            if (isPinCorrect == false || isPinCorrect is null)
            {
                response = Result<ResultHistoryResponseModel>.Error("Wrong Password!");
                goto Result;
            }

            var updatedPerson = await AddBalance(MobileNo, Amount);
            if (updatedPerson.IsError)
            {
                response = Result<ResultHistoryResponseModel>.Error(person.message);
                goto Result;
            }

            var history = await _historyService.CreateDepositHistory(updatedPerson.Data.Person.PersonId, Amount, "Successful!");
            response = history;

        Result:
            return response;
        }

        public async Task<Result<ResultHistoryResponseModel>> Withdraw(string MobileNo, long Amount, string Pin)
        {
            Result<ResultHistoryResponseModel> response = new Result<ResultHistoryResponseModel>();

            if (Amount < 0)
            {
                response = Result<ResultHistoryResponseModel>.ValidationError("Amount can't be less than or equal to 0.");
                goto Result;
            }

            var person = await _personService.GetPersonByMobileNo(MobileNo);
            if (person.IsError)
            {
                response = Result<ResultHistoryResponseModel>.Error(person.message);
                goto Result;
            }

            var isPinCorrect = await CheckPin(MobileNo, Pin);
            if (isPinCorrect == false || isPinCorrect is null)
            {
                response = Result<ResultHistoryResponseModel>.Error("Wrong Password!");
                goto Result;
            }

            var updatedPerson = await ReduceBalance(MobileNo, Amount, 10000);
            if (updatedPerson!.IsError)
            {
                response = Result<ResultHistoryResponseModel>.Error(updatedPerson.message);
                goto Result;
            }

            var history = await _historyService.CreateWithdrawHistory(updatedPerson.Data.Person.PersonId, Amount, "Successful!");
            response = history;

        Result:
            return response;
        }

        public async Task<Result<ResultHistoryResponseModel>> Tansfer(string FromMobileNo, string ToMobileNo, long Amount, string Pin)
        {
            Result<ResultHistoryResponseModel> response = new Result<ResultHistoryResponseModel>();

            if (Amount < 0)
            {
                response = Result<ResultHistoryResponseModel>.ValidationError("Amount can't be less than or equal to 0.");
                goto Result;
            }

            var person = await _personService.GetPersonByMobileNo(FromMobileNo);
            if (person.IsError)
            {
                response = Result<ResultHistoryResponseModel>.Error(person.message);
                goto Result;
            }

            var isPinCorrect = await CheckPin(FromMobileNo, Pin);
            if (isPinCorrect == false || isPinCorrect is null)
            {
                response = Result<ResultHistoryResponseModel>.Error("Wrong Password!");
                goto Result;
            }

            var fromPerson = await ReduceBalance(FromMobileNo, Amount, 0);
            if (fromPerson!.IsError)
            {
                response = Result<ResultHistoryResponseModel>.Error(fromPerson.message);
                goto Result;
            }

            var toPerson = await AddBalance(ToMobileNo, Amount);
            if (toPerson.IsError)
            {
                response = Result<ResultHistoryResponseModel>.Error(toPerson.message);
                goto Result;
            }

            var history = await _historyService.CreateTransferHistory(fromPerson!.Data.Person.PersonId, toPerson!.Data.Person.PersonId, Amount, "Successfully Transferred!");
            response = history!;

        Result:
            return response;
        }

        public async Task<Result<ResultPersonResponseModel>> ReduceBalance(string MobileNo, long Amount, long minimum)
        {
            Result<ResultPersonResponseModel> response = new Result<ResultPersonResponseModel>();
            var person = await _personService.GetPersonByMobileNo(MobileNo);
            if (person.IsError)
            {
                response = person;
                goto Result;
            }

            person.Data.Person.Balance = (person.Data.Person.Balance ?? 0) - Amount;
            if (person.Data.Person.Balance < minimum)
            {
                response = Result<ResultPersonResponseModel>.Error("Not Enough Balance!");
                goto Result;
            }

            var updatedPerson = await _personService.UpdatePerson(MobileNo, person.Data.Person);
            if (updatedPerson.IsError)
            {
                response = updatedPerson;
                goto Result;
            }

            ResultPersonResponseModel result = new ResultPersonResponseModel { Person = updatedPerson.Data.Person, Balance = updatedPerson.Data.Person.Balance };
            response = Result<ResultPersonResponseModel>.Success("Success!", result);

        Result:
            return response;
        }

        public async Task<Result<ResultPersonResponseModel>> AddBalance(string MobileNo, long Amount)
        {
            Result<ResultPersonResponseModel> response = new Result<ResultPersonResponseModel>();
            var person = await _personService.GetPersonByMobileNo(MobileNo);
            if (person.IsError)
            {
                response = person;
                goto Result;
            }

            person.Data.Person.Balance = (person.Data.Person.Balance ?? 0) + Amount;
            var updatedPerson = await _personService.UpdatePerson(MobileNo, person.Data.Person);

            if (updatedPerson.IsError)
            {
                response = updatedPerson;
                goto Result;
            }

            ResultPersonResponseModel result = new ResultPersonResponseModel
            {
                Person = person.Data.Person,
                Balance = person.Data.Person.Balance
            };
            response = Result<ResultPersonResponseModel>.Success("Success!", result);

        Result:
            return response;
        }
    }
}
