using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace GargleWool.EntityFrameworkCore.EntityFrameworkCore
{
    public class GargleWoolDbContextConfigure
    {
        public static void Configure(DbContextOptionsBuilder<GargleWoolDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<GargleWoolDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}