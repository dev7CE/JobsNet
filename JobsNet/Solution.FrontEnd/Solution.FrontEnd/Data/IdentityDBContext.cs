using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.FrontEnd.Data
{
    public class IdentityDBContext : IdentityDbContext
    {
        public IdentityDBContext(DbContextOptions<IdentityDBContext> options)
            : base(options) { }
    }
}
