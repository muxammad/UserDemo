using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserDemo.Domain.Configuration;
using UserDemo.Domain.Entities;
using UserDemo.Service.DTOs;

namespace UserDemo.Service.Interfaces
{
    public interface IUserService
    {
        public Task<UserForResultDto> AddAsync(UserCreationDto userCreationDto);
        public Task<UserForResultDto> UpdateAsync(int id, UserForResultDto userCreationDto);
        public Task<UserForResultDto> GetByIdAsync(int id);
        public Task<IEnumerable<UserForResultDto>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null);
        public Task<bool> DeleteAsync(int id);
    }
}
