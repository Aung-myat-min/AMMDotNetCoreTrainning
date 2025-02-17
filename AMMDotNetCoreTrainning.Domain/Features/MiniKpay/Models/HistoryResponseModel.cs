﻿using MiniKPay.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMMDotNetCoreTrainning.Domain.Features.MiniKpay.Models;

public class HistoryResponseModel
{
    public BaseResponseModel ResponseModel { get; set; }
    public TblHistory History { get; set; }
    public List<ExtendedHistory>? Histories { get; set; }
}

public class ResultHistoryResponseModel
{
    public TblHistory History { get; set; }
}
