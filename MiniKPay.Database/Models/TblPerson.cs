using System;
using System.Collections.Generic;

namespace MiniKPay.Database.Models;

public partial class TblPerson
{
    public int PersonId { get; set; }

    public string FullName { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public long? Balance { get; set; }

    public string Pin { get; set; } = null!;

    public bool? DeleteFalg { get; set; }

    public virtual ICollection<TblHistory> TblHistoryAccountNavigations { get; set; } = new List<TblHistory>();

    public virtual ICollection<TblHistory> TblHistoryFromAccountNavigations { get; set; } = new List<TblHistory>();

    public virtual ICollection<TblHistory> TblHistoryToAccountNavigations { get; set; } = new List<TblHistory>();
}
