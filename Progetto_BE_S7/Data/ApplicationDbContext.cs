﻿using System.Reflection.Emit;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Progetto_BE_S7.Models.Auth;

namespace Progetto_BE_S7.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>,IdentityUserToken<string>>
    {
       	public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<ApplicationRole> ApplicationRoles { get; set; }
		public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUserRole>().HasOne(p=> p.User).WithMany(p=>p.ApplicationUserRole).HasForeignKey(p=> p.UserId);
            builder.Entity<ApplicationUserRole>().HasOne(p=> p.Role).WithMany(p=>p.ApplicationUserRole).HasForeignKey(p=> p.RoleId);

            builder.Entity<ApplicationUserRole>().Property(p=>p.Date).HasDefaultValueSql("GETDATE()").IsRequired(true);

            builder.Entity<ApplicationRole>().HasData(new ApplicationRole() { Id = "952F5F5C-A098-4C82-A961-4B16F3E08CF9", Name = "Amministratore", NormalizedName = "AMMINISTRATORE", ConcurrencyStamp = "952F5F5C-A098-4C82-A961-4B16F3E08CF9" });
            builder.Entity<ApplicationRole>().HasData(new ApplicationRole() { Id = "DAE37CB4-D623-47AE-86EA-452264CB8EC6", Name = "Utente", NormalizedName = "UTENTE", ConcurrencyStamp = "DAE37CB4-D623-47AE-86EA-452264CB8EC6" });
        }
    }

    
}
