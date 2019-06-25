using GargleWool.Core;
using GargleWool.Core.Configuration;
using GargleWool.Core.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GargleWool.EntityFrameworkCore.EntityFrameworkCore
{
    public class GargleWoolDbContextFactory:IDesignTimeDbContextFactory<GargleWoolDbContext>
    {
        public GargleWoolDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<GargleWoolDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            GargleWoolDbContextConfigure.Configure(builder, configuration.GetConnectionString(GargleWoolConstants.ConnectionStringName));

            return new GargleWoolDbContext(builder.Options);
        }
    }
}