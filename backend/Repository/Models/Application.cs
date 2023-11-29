using System;
using System.Collections.Generic;

namespace Repository;

public partial class Application
{
    public long Id { get; set; }

    public string Ticket { get; set; } = null!;

    public int TypeTicket { get; set; }

    public int SubTypeTicket { get; set; }

    public int Priority { get; set; }

    public int Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<File> Files { get; set; } = new List<File>();
}
