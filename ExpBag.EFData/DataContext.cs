using ExpBag.Domain;
using ExpBag.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpBag.EFData
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, long, IdentityUserClaim<long>,
                    AppUserRole, IdentityUserLogin<long>,
                    IdentityRoleClaim<long>, IdentityUserToken<long>>
    {


        //public DbSet<Module> Modules { get; set; }
        //public DbSet<ModuleFile> ModuleFiles { get; set; }

        public DbSet<ExpModule> Modules { get; set; }
        public DbSet<ExpModuleFile> ModuleFiles { get; set; }
        public DbSet<ExpModuleExtention> ModuleExtentions { get; set; }


        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
            builder.Entity<ExpModule>(expModule =>
            {
                expModule.HasKey(em => new { em.Id });

                expModule.HasMany(em => em.IncludedFiles)
                    .WithOne(emf => emf.Module);
            });
        }
    }
}
