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

        public HistoryResponseModel CreateWithdrawHistory(int PersonId, long Amount, string message)
        {
            HistoryResponseModel response = new HistoryResponseModel();

            var newHistory = new TblHistory
            {
                Account = PersonId,
                Amount = Amount,
                ActionType = "Withdraw"
            };

            _db.Add(newHistory);
            _db.SaveChanges();

            if (newHistory == null)
            {
                response.ResponseModel = BaseResponseModel.ServerError("999", "Internal Server Error");
                goto Result;
            }

            response.ResponseModel = BaseResponseModel.Success("001", message);
            response.History = newHistory;

        Result:
            return response;
        }

        public HistoryResponseModel CreateDepositHistory(int PersonId, long Amount, string message)
        {
            HistoryResponseModel response = new HistoryResponseModel();

            var newHistory = new TblHistory
            {
                Account = PersonId,
                Amount = Amount,
                ActionType = "Deposit"
            };

            _db.Add(newHistory);
            _db.SaveChanges();

            if (newHistory == null)
            {
                response.ResponseModel = BaseResponseModel.ServerError("999", "Internal Server Error");
                goto Result;
            }

            response.ResponseModel = BaseResponseModel.Success("001", message);
            response.History = newHistory;

        Result:
            return response;
        }

        public HistoryResponseModel? CreateTransferHistory(int FromPersonId, int ToPersonId, long Amount, string message)
        {
            HistoryResponseModel response = new HistoryResponseModel();

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

            _db.Add(sendHistory);
            _db.Add(receiveHistory);
            _db.SaveChanges();

            if (sendHistory == null || receiveHistory == null)
            {
                response.ResponseModel = BaseResponseModel.ServerError("999", "Internal Server Error");
                goto Result;
            }

            response.ResponseModel = BaseResponseModel.Success("001", message);
            response.History = sendHistory;

        Result:
            return response;
        }

        public HistoryResponseModel GetHistoryByPerson(string mobileNo)
        {
            HistoryResponseModel response = new HistoryResponseModel();
            var person = _personService.GetPersonByMobileNo(mobileNo);
            if (person.ResponseModel.ResponseType == EnumResponseType.NotFound)
            {
                response.ResponseModel = BaseResponseModel.NotFound("404", "Person not found!");
                goto Result;
            }

            int id = person.Person.PersonId;
            var list = _db.TblHistories
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
                .ToList();

            if (list.IsNullOrEmpty())
            {
                response.ResponseModel = BaseResponseModel.NotFound("404", "Your History is empty!");
                goto Result;
            }

            response.ResponseModel = BaseResponseModel.Success("001", "Here are your histories");
            response.Histories = list;

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
