﻿using Micromarin.Application.Entities;
using Micromarin.Application.Interfaces.Repositories;
using Micromarin.Domain.Repositories;



namespace Micromarin.Infrastructure.Persistence.Repositories;

public class CustomerRepository : EntityFrameworkRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(MicromarinDbContext context) : base(context)
    {
    }
}
