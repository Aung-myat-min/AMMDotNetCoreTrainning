using AMMDotNetCoreTrainning.Domain.Features.MiniKpay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        private readonly AppDbContext _db;
        private readonly PersonService _personService;

        public HistoryService()
        {
            _db = new AppDbContext();
            _personService = new PersonService();
        }

        public async Task<Result<ResultHistoryResponseModel>> CreateWithdrawHistory(int PersonId, long Amount, string message)
        {
            Result<ResultHistoryResponseModel> response = new Result<ResultHistoryResponseModel>();

            var newHistory = new TblHistory
            {
                Account = PersonId,
                Amount = Amount,
                ActionType = "Withdraw"
            };

            await _db.AddAsync(newHistory);
            await _db.SaveChangesAsync();

            if (newHistory == null)
            {
                response = Result<ResultHistoryResponseModel>.Error("History Creation Failed!");
                goto Result;
            }

            ResultHistoryResponseModel result = new ResultHistoryResponseModel
            {
                History = newHistory
            };
            response = Result<ResultHistoryResponseModel>.Success("History, ", result);

        Result:
            return response;
        }

        public async Task<Result<ResultHistoryResponseModel>> CreateDepositHistory(int PersonId, long Amount, string message)
        {
            Result<ResultHistoryResponseModel> response = new Result<ResultHistoryResponseModel>();

            var newHistory = new TblHistory
            {
                Account = PersonId,
                Amount = Amount,
                ActionType = "Deposit"
            };

            await _db.AddAsync(newHistory);
            await _db.SaveChangesAsync();

            if (newHistory == null)
            {
                response = Result<ResultHistoryResponseModel>.Error("History Creation Failed!");
                goto Result;
            }

            ResultHistoryResponseModel result = new ResultHistoryResponseModel
            {
                History = newHistory
            };
            response = Result<ResultHistoryResponseModel>.Success("History, ", result);

        Result:
            return response;
        }

        public async Task<Result<ResultHistoryResponseModel>> CreateTransferHistory(int FromPersonId, int ToPersonId, long Amount, string message)
        {
            Result<ResultHistoryResponseModel> response = new Result<ResultHistoryResponseModel>();

            var sendHistory = new TblHistory
            {
                FromAccount = FromPersonId,
                ToAccount = ToPersonId,
                Amount = Amount,
                ActionType = "SendToAccount"
            };

            var receiveHistory = new TblHistory
            {
                FromAccount = FromPersonId,
                ToAccount = ToPersonId,
                Amount = Amount,
                ActionType = "GetFromAccount"
            };

            await _db.AddAsync(sendHistory);
            await _db.AddAsync(receiveHistory);
            await _db.SaveChangesAsync();

            if (sendHistory == null || receiveHistory == null)
            {
                response = Result<ResultHistoryResponseModel>.Error("History Creation Failed!");
                goto Result;
            }

            ResultHistoryResponseModel result = new ResultHistoryResponseModel
            {
                History = sendHistory
            };
            response = Result<ResultHistoryResponseModel>.Success("History, ", result);

        Result:
            return response;
        }

        public async Task<Result<List<ExtendedHistory>>> GetHistoryByPerson(string mobileNo)
        {
            Result<List<ExtendedHistory>> response = new Result<List<ExtendedHistory>>();
            var person = await _personService.GetPersonByMobileNo(mobileNo);
            if (person.IsNotFound)
            {
                response = Result<List<ExtendedHistory>>.NotFound("Person Not Found!");
                goto Result;
            }

            int id = person.Data.Person.PersonId;
            var list = await _db.TblHistories
                .AsNoTracking()
                .Where(x => x.FromAccount == id || x.ToAccount == id || x.Account == id)
                .Select(history => new ExtendedHistory
                {
                    HistoryId = history.HistoryId,
                    CreatedTime = history.CreatedTime,
                    ActionType = history.ActionType,
                    Amount = history.Amount,
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
                .ToListAsync();

            if (list.IsNullOrEmpty())
            {
                response = Result<List<ExtendedHistory>>.NotFound("Your Transition History is empty!");
                goto Result;
            }

            List<ExtendedHistory> histories = list;
            response = Result<List<ExtendedHistory>>.Success("Here are your Transition Histories!", histories);

        Result:
            return response;
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
