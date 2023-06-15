using System;

namespace bankk;

public partial class LoginAttempt
{
    public int AttemptId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? AttemptDate { get; set; }

    public bool? Success { get; set; }

    public virtual Customer? Customer { get; set; }
}
