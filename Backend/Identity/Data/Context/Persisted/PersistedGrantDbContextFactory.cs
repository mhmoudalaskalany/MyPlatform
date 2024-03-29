﻿using System.Reflection;
using Data.Context.Factory;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace Data.Context.Persisted
{
    public class PersistedGrantDbContextFactory : DesignTimeDbContextFactoryBase<PersistedGrantDbContext>
    {
        protected override PersistedGrantDbContext CreateNewInstance(DbContextOptions<PersistedGrantDbContext> options)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
            optionsBuilder.UseSqlServer(ConnectionString, sql => sql.MigrationsAssembly(this.GetType().GetTypeInfo().Assembly.GetName().Name));
            return new PersistedGrantDbContext(optionsBuilder.Options, new OperationalStoreOptions());
        }
    }
}