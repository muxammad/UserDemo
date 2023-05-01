using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserDemo.DataAccess.Context;
using UserDemo.DataAccess.Interfaces;
using UserDemo.Domain.Entities;

namespace UserDemo.DataAccess.Repository
{
        public class UserRepository : IUserRepository
        {
            private readonly UserDbContext _appDbContext = new UserDbContext();

            public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
            {
                var user = this._appDbContext.Users.FirstOrDefault(expression);

                this._appDbContext.Users.Remove(user);
                await this._appDbContext.SaveChangesAsync();
                return true;
            }
            public IQueryable<User> GetAll() => _appDbContext.Users;

            public async Task<User> GetByIdAysnc(Expression<Func<User, bool>> expression) =>
                await this._appDbContext.Users.FirstOrDefaultAsync(expression);

            public async Task<User> InsertAsync(User user)
            {
                var entity = await this._appDbContext.Users.AddAsync(user);
                await this._appDbContext.SaveChangesAsync();
                return entity.Entity;
            }

            public async Task<User> UpdateAsync(User user) =>
                this._appDbContext.Users.Update(user).Entity;

            public async Task SaveAsync()
            {
                await this._appDbContext.SaveChangesAsync();
            }
        }
    }

