using System;
using System.Collections.Generic;

namespace MiniKPay.Database.Models;

public partial class TblHistory
{
    public int HistoryId { get; set; }

    public DateTime CreatedTime { get; set; }

    public string? ActionType { get; set; }

    public long Amount { get; set; }

    public int FromAccount { get; set; }

    public int ToAccount { get; set; }

    public virtual TblPerson FromAccountNavigation { get; set; } = null!;

    public virtual TblPerson ToAccountNavigation { get; set; } = null!;
}
