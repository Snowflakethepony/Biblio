﻿using Biblio.Server.Data;
using Biblio.Server.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Biblio.Server.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext DbContext { get; set; }

        public RepositoryBase(ApplicationDbContext applicationDbContext)
        {
            this.DbContext = applicationDbContext;
            //DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public IQueryable<T> FindAll()
        {
            return this.DbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.DbContext.Set<T>()
                .Where(expression); // .AsNoTracking()
        }

        public IQueryable<T> FindBySqlLike(string modelAssembly, string property, string query)
        {
            // Get the property
            var modelProp = Type.GetType(modelAssembly).GetProperty(property);

            var t = from a in this.DbContext.Set<T>().AsNoTracking() where EF.Functions.Contains(modelProp.Name, query) select a;
            return t;
        }

        public void Create(T entity)
        {
            this.DbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.DbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.DbContext.Set<T>().Remove(entity);
        }

        public Task<T> FindByPrimaryKey(object key)
        {
            return this.DbContext.Set<T>()
                .FindAsync(key).AsTask();
        }

        public Task<T> FindByPrimaryKeys(params object[] keyValues)
        {
            return this.DbContext.Set<T>()
                .FindAsync(keyValues[0], keyValues[1]).AsTask();
        }
    }
}
