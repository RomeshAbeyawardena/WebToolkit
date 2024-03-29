﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebToolkit.Contracts.Data
{
    public interface IRepository<T> where T : class
    {
        T Attach(T entity);
        IQueryable<T> Query(Expression<Func<T, bool>> queryExpression = null, bool trackingQuery = false);
        DbSet<T> DbSet { get; }
        DbContext Context { get; }
        Task<T> SaveChangesAsync(T entry, bool commitChanges = true);
    }
}