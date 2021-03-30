using Microsoft.AspNetCore.Identity;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            string role = "Administrator";
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole 
                { 
                    Name = role, 
                    NormalizedName = role.ToUpper() 
                }
            );

            role = "Oferente";
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole 
                { 
                    Name = role, 
                    NormalizedName = role.ToUpper() 
                }
            );

            role = "Empleador";
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole 
                { 
                    Name = role, 
                    NormalizedName = role.ToUpper() 
                }
            );
        }
    }
}
