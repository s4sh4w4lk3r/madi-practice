using System;
using System.Collections.Generic;

namespace Repository;

public partial class AirLine
{
    public long Id { get; set; }

    public string Flight { get; set; } = null!;

    public string Airline { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
