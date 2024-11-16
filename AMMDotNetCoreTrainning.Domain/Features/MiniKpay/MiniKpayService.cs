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

        public long? BalanceCheck(string MobileNo)
        {
            var balance = _db.TblPeople.Where(x => x.DeleteFalg == false && x.MobileNo == MobileNo).Select(x => x.Balance).FirstOrDefault();
            return balance;
        }



    }
}
