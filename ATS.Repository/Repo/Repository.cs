using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Core.Model;
using ATS.Repository.Interface;
using ATS.Repository.Model;

namespace ATS.Repository.Repo
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ATSDBContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ATSDBContext context)
        {
            this._context = context;
            this._dbSet = context.Set<T>();
        }
        public bool Create(ref T input)
        {
            _dbSet.Add(input);
            return true;
        }

        public bool Delete(T input)
        {
            _dbSet.Remove(input);
            return true;
        }

        //public T Get(Guid id)
        //{
        //    return _dbSet.Find(id);
        //}

        //public IEnumerable<T> GetAll()
        //{
        //    return _dbSet.ToList();
        //}

        //public IEnumerable<T> Select(Func<T, bool> condition)
        //{
        //    return _dbSet.Where(condition);
        //}

        public bool Update(ref T input)
        {
           _context.Entry(input).State=EntityState.Modified;
            return true;
        }

        public ATSDBContext GetConnection()
        {
            return _context;
        }
    }
}
