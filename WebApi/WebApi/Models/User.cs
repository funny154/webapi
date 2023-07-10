using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Phone { get; set; }
}
