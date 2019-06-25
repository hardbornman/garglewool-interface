using Abp.IdentityServer4;
using Abp.Zero.EntityFrameworkCore;
using GargleWool.Core.Authorization.Roles;
using GargleWool.Core.Authorization.Users;
using GargleWool.Core.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace GargleWool.EntityFrameworkCore.EntityFrameworkCore
{
    public class GargleWoolDbContext:AbpZeroDbContext<Tenant,Role,User, GargleWoolDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<PersistedGrantEntity> PersistedGrants { get; set; }


        public GargleWoolDbContext(DbContextOptions<GargleWoolDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}