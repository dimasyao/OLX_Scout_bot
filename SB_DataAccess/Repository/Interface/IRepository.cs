﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SB_DataAccess.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null,
            bool isTraking = true);

        T? FirstOrDefault(
             Expression<Func<T, bool>>? filter = null,
            string? includeProperties = null,
            bool isTraking = true);

        void Add(T entity);

        void Remove(T entity);

        void Remove(IEnumerable<T> entities);

        void Save();
    }
}
