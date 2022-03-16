using Microsoft.EntityFrameworkCore;
using Project.Database.Abstract;
using Project.Database.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Database.Concrete
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ExamProjectContext ExamProjectContext { get; set; }
        public RepositoryBase(ExamProjectContext examContext)
        {
            ExamProjectContext = examContext;
        }

        public IQueryable<T> FindAll() => ExamProjectContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => ExamProjectContext.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => ExamProjectContext.Set<T>().Add(entity);
        public void Update(T entity) => ExamProjectContext.Set<T>().Update(entity);
        public void Delete(T entity) => ExamProjectContext.Set<T>().Remove(entity);

        public void Save()
        {
            ExamProjectContext.SaveChanges();
        }



        //public void Create(T entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Delete(T entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<T> FindAll()
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Update(T entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
