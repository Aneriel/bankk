using System.Collections.Generic;

namespace bankk;

public partial class Account
{
    public int AccountId { get; set; }

    public int? CustomerId { get; set; }

    public string? AccountNumber { get; set; }

    public decimal? Balance { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
