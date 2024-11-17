using Microsoft.EntityFrameworkCore;
using MiniKPay.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMMDotNetCoreTrainning.Domain.Features.MiniKpay
{
    public class PersonService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public TblPerson CreateAccount(TblPerson person)
        {
            var newPerson = _db.TblPeople.Add(person);
            _db.SaveChanges();
            return person;
        }

        public TblPerson? GetPersonByMobileNo(string MobileNo)
        {
            var person = _db.TblPeople.AsNoTracking().Where(x => x.DeleteFalg == false && x.MobileNo == MobileNo).FirstOrDefault();
            if (person is null)
            {
                return null;
            }

            return person;
        }

        public TblPerson? UpdatePerson(string MobileNo, TblPerson person)
        {
            var item = GetPersonByMobileNo(MobileNo);
            if (item is null && person is null)
            {
                return null;
            }

            if (person.FullName != null)
            {
                item!.FullName = person.FullName;

            }
            if (person.MobileNo != null)
            {
                item!.MobileNo = person.MobileNo;

            }
            if (person.Balance != null)
            {
                item!.Balance = person.Balance;

            }
            if (person.Pin != null)
            {
                item!.Pin = person.Pin;

            }

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return item;
        }

        public bool? DeactivatePerson(string MobileNo)
        {
            var person = GetPersonByMobileNo(MobileNo);
            if (person is null)
            {
                return null;
            }

            person.DeleteFalg = true;
            _db.Entry(person).State = EntityState.Modified;
            int result = _db.SaveChanges();

            return result > 0;
        }
    }
}
