namespace CRM.Data.Contracts
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T> where T : IEntity
    {
        IQueryable<T> All();

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Detach(T entity);

        void Delete(T entity);

        void Delete(object id);

        void UpdateValues(Expression<Func<T, object>> entity);

        int SaveChanges();
    }
}
