using System.Linq.Expressions;
using UserDemo.Domain.Entities;

namespace UserDemo.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> InsertAsync(User user);
        public Task<User> UpdateAsync(User user);
        public Task<bool> DeleteAsync(Expression<Func<User, bool>> predicate);
        public Task<User> GetByIdAysnc(Expression<Func<User, bool>> expression);
        IQueryable<User> GetAll();

        public Task SaveAsync();
    }
}
