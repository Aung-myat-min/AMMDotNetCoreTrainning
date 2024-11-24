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

        public PersonResponseModel BalanceCheck(string MobileNo)
        {
            PersonResponseModel response = new PersonResponseModel();

            var person = _personService.GetPersonByMobileNo(MobileNo);
            if (person.ResponseModel.ResponseType != EnumResponseType.Success)
            {
                response = person;
                goto Result;
            }

            response.ResponseModel = BaseResponseModel.Success("001", "Here is your balance...");
            response.Balance = person.Person.Balance;

        Result:
            return response;
        }

        public bool? CheckPin(string MobileNo, string Pin)
        {
            var person = _personService.GetPersonByMobileNo(MobileNo);
            if (person == null)
            {
                return null;
            }

            var pin = person.Person.Pin;
            if (String.Equals(pin, Pin))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public PersonResponseModel ChangeMobileNo(string OldMobile, string NewMobile, string Pin)
        {
            PersonResponseModel response = new PersonResponseModel();
            var isPinCorrect = CheckPin(OldMobile, Pin);
            if (isPinCorrect == false || isPinCorrect is null)
            {
                response.ResponseModel = BaseResponseModel.Error("400", "Wrong Password!");
                goto Result;
            }

            var person = _personService.GetPersonByMobileNo(OldMobile);
            if (person.ResponseModel.ResponseType != EnumResponseType.Success)
            {
                response = person;
                goto Result;
            }

            person.Person.MobileNo = NewMobile;
            response = _personService.UpdatePerson(OldMobile, person.Person);

        Result:
            return response;
        }

        public PersonResponseModel ChangePin(string MobileNo, string OldPin, string NewPin)
        {
            PersonResponseModel response = new PersonResponseModel();

            var isPinCorrect = CheckPin(MobileNo, OldPin);
            if (isPinCorrect == false || isPinCorrect is null)
            {
                response.ResponseModel = BaseResponseModel.Error("400", "Wrong Pin!");
                goto Result;
            }

            if (OldPin == NewPin)
            {
                response.ResponseModel = BaseResponseModel.Error("400", "New Pin can't be the same with the old pin.");
                goto Result;
            }

            var person = _personService.GetPersonByMobileNo(MobileNo);
            if (person.ResponseModel.ResponseType != EnumResponseType.Success)
            {
                response = person;
                goto Result;
            }

            person.Person.Pin = NewPin;

            var updatedPeson = _personService.UpdatePerson(MobileNo, person.Person);
            if (updatedPeson.ResponseModel.ResponseType != EnumResponseType.Success)
            {
                response = updatedPeson;
                updatedPeson.Person = default;
                goto Result;
            }

            response.ResponseModel = BaseResponseModel.Success("001", "Pin changed successfully!");

        Result:
            return response;
        }

        public HistoryResponseModel Deposit(string MobileNo, long Amount, string Pin)
        {
            HistoryResponseModel response = new HistoryResponseModel();

            if (Amount < 0)
            {
                response.ResponseModel = BaseResponseModel.ValidationError("400", "Amount can't be less than or equal to 0.");
                goto Result;
            }

            var person = _personService.GetPersonByMobileNo(MobileNo);
            if (person.ResponseModel.ResponseType != EnumResponseType.Success)
            {
                response.ResponseModel = person.ResponseModel;
                goto Result;
            }

            var isPinCorrect = CheckPin(MobileNo, Pin);
            if (isPinCorrect == false || isPinCorrect is null)
            {
                response.ResponseModel = BaseResponseModel.Error("400", "Wrong Password!");
                goto Result;
            }

            var updatedPerson = AddBalance(MobileNo, Amount);
            if (updatedPerson!.ResponseModel.ResponseType != EnumResponseType.Success)
            {
                response.ResponseModel = updatedPerson.ResponseModel;
                goto Result;
            }

            var history = _historyService.CreateDepositHistory(updatedPerson.Person.PersonId, Amount, "Successful!");
            response = history;

        Result:
            return response;
        }

        public HistoryResponseModel Withdraw(string MobileNo, long Amount, string Pin)
        {
            HistoryResponseModel response = new HistoryResponseModel();

            if (Amount < 0)
            {
                response.ResponseModel = BaseResponseModel.ValidationError("400", "Amount can't be less than or equal to 0.");
                goto Result;
            }

            var person = _personService.GetPersonByMobileNo(MobileNo);
            if (person.ResponseModel.ResponseType != EnumResponseType.Success)
            {
                response.ResponseModel = person.ResponseModel;
                goto Result;
            }

            var isPinCorrect = CheckPin(MobileNo, Pin);
            if (isPinCorrect == false || isPinCorrect is null)
            {
                response.ResponseModel = BaseResponseModel.Error("400", "Wrong Password!");
                goto Result;
            }

            var updatedPerson = ReduceBalance(MobileNo, Amount, 10000);
            if (updatedPerson!.ResponseModel.ResponseType != EnumResponseType.Success)
            {
                response.ResponseModel = updatedPerson.ResponseModel;
                goto Result;
            }

            var history = _historyService.CreateDepositHistory(updatedPerson.Person.PersonId, Amount, "Successful!");
            response = history;

        Result:
            return response;
        }

        public HistoryResponseModel Tansfer(string FromMobileNo, string ToMobileNo, long Amount, string Pin)
        {
            HistoryResponseModel response = new HistoryResponseModel();

            if (Amount < 0)
            {
                response.ResponseModel = BaseResponseModel.ValidationError("400", "Amount can't be less than or equal to 0.");
                goto Result;
            }

            var person = _personService.GetPersonByMobileNo(FromMobileNo);
            if (person.ResponseModel.ResponseType != EnumResponseType.Success)
            {
                response.ResponseModel = person.ResponseModel;
                goto Result;
            }

            var isPinCorrect = CheckPin(FromMobileNo, Pin);
            if (isPinCorrect == false || isPinCorrect is null)
            {
                response.ResponseModel = BaseResponseModel.Error("400", "Wrong Password!");
                goto Result;
            }

            var fromPerson = ReduceBalance(FromMobileNo, Amount, 0);
            if (fromPerson!.ResponseModel.ResponseType != EnumResponseType.Success)
            {
                response.ResponseModel = fromPerson.ResponseModel;
                goto Result;
            }

            var toPerson = AddBalance(ToMobileNo, Amount);
            if (toPerson!.ResponseModel.ResponseType != EnumResponseType.Success)
            {
                response.ResponseModel = toPerson.ResponseModel;
                goto Result;
            }

            var history = _historyService.CreateTransferHistory(fromPerson!.Person.PersonId, toPerson!.Person.PersonId, Amount, "Successfully Transferred!");
            response = history!;

        Result:
            return response;
        }

        public PersonResponseModel ReduceBalance(string MobileNo, long Amount, long minimum)
        {
            PersonResponseModel response = new PersonResponseModel();
            var person = _personService.GetPersonByMobileNo(MobileNo);
            if (person.ResponseModel.ResponseType != EnumResponseType.Success)
            {
                response = person;
                goto Result;
            }

            person.Person.Balance = (person.Person.Balance ?? 0) - Amount;
            if (person.Person.Balance < minimum)
            {
                response.ResponseModel = BaseResponseModel.Error("400", "Not Enough Balance!");
                goto Result;
            }

            var updatedPerson = _personService.UpdatePerson(MobileNo, person.Person);
            if (updatedPerson.ResponseModel.ResponseType != EnumResponseType.Success)
            {
                response = updatedPerson;
                goto Result;
            }

            response.ResponseModel = BaseResponseModel.Success("001", "Success!");
            response.Person = updatedPerson.Person;

        Result:
            return response;
        }

        public PersonResponseModel AddBalance(string MobileNo, long Amount)
        {
            PersonResponseModel response = new PersonResponseModel();
            var person = _personService.GetPersonByMobileNo(MobileNo);
            if (person.ResponseModel.ResponseType != EnumResponseType.Success)
            {
                response = person;
                goto Result;
            }

            person.Person.Balance = (person.Person.Balance ?? 0) + Amount;
            var updatedPerson = _personService.UpdatePerson(MobileNo, person.Person);

            if (updatedPerson.ResponseModel.ResponseType != EnumResponseType.Success)
            {
                response = updatedPerson;
                goto Result;
            }

            response.ResponseModel = BaseResponseModel.Success("001", "Success!");
            response.Person = updatedPerson.Person;

        Result:
            return response;
        }
    }
}
