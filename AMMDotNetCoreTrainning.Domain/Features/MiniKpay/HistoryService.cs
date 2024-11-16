using Microsoft.EntityFrameworkCore;
using MiniKPay.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMMDotNetCoreTrainning.Domain.Features.MiniKpay
{
    public class HistoryService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public TblHistory? CreateWithdrawHistory(int PersonId, long Amount)
        {
            var newHistory = new TblHistory
            {
                Account = PersonId,
                Amount = Amount,
                ActionType = "Withdraw"
            };

            _db.Add(newHistory);
            _db.SaveChanges();

            return newHistory;
        }

        public TblHistory? CreateDepositHistory(int PersonId, long Amount)
        {

            var newHistory = new TblHistory
            {
                Account = PersonId,
                Amount = Amount,
                ActionType = "Deposit"
            };

            _db.Add(newHistory);
            _db.SaveChanges();

            return newHistory;
        }

        public TblHistory? CreateTransferHistory(int FromPersonId, int ToPersonId, long Amount)
        {
            var newHistory = new TblHistory
            {
                FromAccount = FromPersonId,
                ToAccount = ToPersonId,
                Amount = Amount
            };

            _db.Add(newHistory);
            _db.SaveChanges();

            return newHistory;
        }

        public List<ExtendedHistory> GetHistoryByPerson(int id)
        {
            var list = _db.TblHistories
                .AsNoTracking()
                .Where(x => x.FromAccount == id || x.ToAccount == id || x.Account == id)
                .Select(history => new ExtendedHistory
                {
                    HistoryId = history.HistoryId,
                    CreatedTime = history.CreatedTime,
                    ActionType = history.ActionType,
                    Amount = history.Amount,
                    Account = history.Account,
                    FromAccount = history.FromAccount,
                    ToAccount = history.ToAccount,
                    FromPersonFullName = history.FromAccountNavigation != null
                        ? history.FromAccountNavigation.FullName
                        : null,
                    FromPersonMobileNo = history.FromAccountNavigation != null
                        ? history.FromAccountNavigation.MobileNo
                        : null,
                    ToPersonFullName = history.ToAccountNavigation != null
                        ? history.ToAccountNavigation.FullName
                        : null,
                    ToPersonMobileNo = history.ToAccountNavigation != null
                        ? history.ToAccountNavigation.MobileNo
                        : null
                })
                .ToList();

            return list;
        }

    }

    public class ExtendedHistory : TblHistory
    {
        public string? FromPersonFullName { get; set; }
        public string? FromPersonMobileNo { get; set; }
        public string? ToPersonFullName { get; set; }
        public string? ToPersonMobileNo { get; set; }
    }
}
