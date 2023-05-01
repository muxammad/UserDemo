using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserDemo.DataAccess.Interfaces;
using UserDemo.Domain.Configuration;
using UserDemo.Domain.Entities;
using UserDemo.Service.Commons.Exceptions;
using UserDemo.Service.Commons.Extension;
using UserDemo.Service.Commons.Security;
using UserDemo.Service.DTOs;
using UserDemo.Service.Interfaces;

namespace UserDemo.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;
        public UserService(IUserRepository repository,IMapper mapper)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task<UserForResultDto> AddAsync(UserCreationDto dto)
        {

            var user = await this.repository.GetByIdAysnc(u => u.FirstName.ToLower() == dto.FirstName.ToLower()
            && u.LastName.ToLower() == dto.LastName.ToLower());
            if (user is not null)
                throw new CustomException(403, "User already exists");

            var mapped = this.mapper.Map<User>(dto);
            mapped.CreatedAt = DateTime.UtcNow;
            mapped.Password = Hash(dto.Password);
            var result = await this.repository.InsertAsync(mapped);
            await this.repository.SaveAsync();

            return this.mapper.Map<UserForResultDto>(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await this.repository.GetByIdAysnc(u => u.Id == id);
            if (user is null)
                throw new CustomException(404, "User not found");

            var deleted = await this.repository.DeleteAsync(u => u.Id == id);
            await this.repository.SaveAsync();

            return deleted;
        }

        public async Task<IEnumerable<UserForResultDto>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null)
        {
            if (expression == null) expression = x => true;

            var users = this.repository.GetAll();
            if (users is null)
                throw new CustomException(404, "Users not found");

            users = users.Where(expression).ToPagedList<User>(@params);
            return this.mapper.Map<IEnumerable<UserForResultDto>>(users);
        }

        public async Task<UserForResultDto> GetByIdAsync(int id)
        {
            var user = await this.repository.GetByIdAysnc(u => u.Id == id);
            if (user is null)
                throw new CustomException(404, "User not found");

            return this.mapper.Map<UserForResultDto>(user);
        }

        public async Task<UserForResultDto> UpdateAsync(int id, UserForResultDto dto)
        {
            var user = await this.repository.GetByIdAysnc(u => u.Id == id);
            if (user is null)
                throw new CustomException(404, "Couldn't found found user for given id.");

            var mapped = this.mapper.Map(dto, user);

            mapped.UpdatedAt = DateTime.UtcNow;
            var result = await this.repository.UpdateAsync(mapped);
            await this.repository.SaveAsync();

            return this.mapper.Map<UserForResultDto>(result);
        }
    }
}
