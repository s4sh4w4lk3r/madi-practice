﻿using System;
using System.Collections.Generic;

namespace Repository;

public partial class MigrationLaravel
{
    public int Id { get; set; }

    public string Migration1 { get; set; } = null!;

    public int Batch { get; set; }
}
