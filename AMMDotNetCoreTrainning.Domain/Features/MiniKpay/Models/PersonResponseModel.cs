using MiniKPay.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMMDotNetCoreTrainning.Domain.Features.MiniKpay.Models;

public class PersonResponseModel
{
    public BaseResponseModel ResponseModel { get; set; }
    public TblPerson Person { get; set; }
}
