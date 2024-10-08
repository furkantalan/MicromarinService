﻿

using Micromarin.Domain.Entities;

namespace Micromarin.Application.Entities;

public class Customer : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}
