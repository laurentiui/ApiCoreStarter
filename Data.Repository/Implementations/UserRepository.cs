using Data.Domain.Entity;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public User GetByConfirmToken(string confirmToken)
        {
            var user = _appDbContext.Set<User>().FirstOrDefault(u => u.ConfirmToken == confirmToken);
            return user;
        }
        public User GetByEmail(string email)
        {
            var user = _appDbContext.Set<User>().FirstOrDefault(u => u.Email.ToLower() == email);
            return user;
        }
        public async Task<User> Insert(User newEntity)
        {
            _appDbContext.Set<User>().Add(newEntity);
            await _appDbContext.SaveChangesAsync().ConfigureAwait(false);

            _appDbContext.Entry(newEntity).State = EntityState.Detached;
            return newEntity;
        }
        public async Task<User> Update(User entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync().ConfigureAwait(false);

            return entity;
        }
    }
}
