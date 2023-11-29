using System;
using System.Collections.Generic;

namespace Repository;

public partial class FormAbi
{
    public long Id { get; set; }

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Email { get; set; } = null!;

    public long Phone { get; set; }

    public int EduState { get; set; }

    public int? Gomadi { get; set; }

    public int Year { get; set; }

    public bool Courses { get; set; }

    public bool Agree { get; set; }

    public bool Subscribe { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
