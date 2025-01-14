﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        public IEnumerable<T> Get(Expression<Func<T, object>>[]? includeProps = null, Expression<Func<T, bool>>? expression = null, bool tracked = true);
        void Create(T entity);

        void Edit(T entity);

        void Delete(T entity);

        void Commit();
    }
}
