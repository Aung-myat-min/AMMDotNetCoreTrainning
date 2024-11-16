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
        private readonly AppDbContext _db = new AppDbContext();
        private readonly PersonService _personService = new PersonService();

        public long? BalanceCheck(string MobileNo)
        {
            var person = _personService.GetPersonByMobileNo(MobileNo);
            if (person == null)
            {
                return null;
            }

            var balance = person.Balance;
            return balance;
        }

        public bool? CheckPin(string MobileNo, string Pin)
        {
            var person = _personService.GetPersonByMobileNo(MobileNo);
            if (person == null)
            {
                return null;
            }

            var pin = person.Pin;
            if (String.Equals(pin, Pin))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool? ChangeMobileNo(string OldMobile, string NewMobile, string Pin)
        {
            var isPinCorrect = CheckPin(OldMobile, Pin);
            if (isPinCorrect == false || isPinCorrect is null)
            {
                return false;
            }

            var person = _personService.GetPersonByMobileNo(OldMobile);
            if (person is null)
            {
                return null;
            }

            person.MobileNo = NewMobile;

            var updatedPeson = _personService.UpdatePerson(OldMobile, person);

            return true;
        }

        public bool? ChangePin(string MobileNo, string OldPin, string NewPin)
        {
            var isPinCorrect = CheckPin(MobileNo, OldPin);
            if (isPinCorrect == false || isPinCorrect is null)
            {
                return false;
            }

            var person = _personService.GetPersonByMobileNo(MobileNo);
            if (person is null)
            {
                return null;
            }

            person.Pin = NewPin;

            var updatedPeson = _personService.UpdatePerson(MobileNo, person);

            return true;
        }

        public bool? Deposit(String MobileNo, long Amount, string Pin)
        {
            var person = _personService.GetPersonByMobileNo(MobileNo);
            if (person is null)
            {
                return null;
            }

            var isPinCorrect = CheckPin(MobileNo, Pin);
            if (isPinCorrect == false)
            {
                return false;
            }

            var updatedPerson = AddBalance(MobileNo, Amount);

            return true;
        }

        public bool? Withdraw(String MobileNo, long Amount, string Pin)
        {
            var person = _personService.GetPersonByMobileNo(MobileNo);
            if (person is null)
            {
                return null;
            }

            var isPinCorrect = CheckPin(MobileNo, Pin);
            if (isPinCorrect == false)
            {
                return false;
            }

            var updatedPerson = ReduceBalance(MobileNo, Amount);

            return true;
        }

        public TblPerson? ReduceBalance(string MobileNo, long Amount)
        {
            var person = _personService.GetPersonByMobileNo(MobileNo);
            if (person is null)
            {
                return null;
            }

            person.Balance = person.Balance - Amount;
            var updatedPerson = _personService.UpdatePerson(MobileNo, person);

            return updatedPerson;
        }

        public TblPerson? AddBalance(string MobileNo, long Amount)
        {
            var person = _personService.GetPersonByMobileNo(MobileNo);
            if (person is null)
            {
                return null;
            }

            person.Balance = person.Balance + Amount;
            var updatedPerson = _personService.UpdatePerson(MobileNo, person);

            return updatedPerson;
        }
    }
}
