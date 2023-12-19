using System;
using System.Collections.Generic;

namespace Repository;

public partial class FileLaravel
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Path { get; set; } = null!;

    public long ApplicationsId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Application Applications { get; set; } = null!;
}
