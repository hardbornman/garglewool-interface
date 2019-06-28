using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

namespace GargleWool.EntityFrameworkCore.EntityFrameworkCore.Repositories
{
    public abstract class GargleWoolRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<GargleWoolDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public GargleWoolRepositoryBase(IDbContextProvider<GargleWoolDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }


    }

    public abstract class GargleWoolRepositoryBase<TEntity> : EfCoreRepositoryBase<GargleWoolDbContext, TEntity, int>
       where TEntity : class, IEntity<int>
    {
        public GargleWoolRepositoryBase(IDbContextProvider<GargleWoolDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }


    }
}
