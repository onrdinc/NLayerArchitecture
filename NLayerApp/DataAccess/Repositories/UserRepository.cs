using DataAccess.Contexts;
using DataAccess.Interfaces;
using Infrastructure.DataAccess.Implementations;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : BaseRepository<Users, DataContext, long>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public async Task<Users> GetByUserAsync(string email, string password, params string[] includeList)
        {
            return await GetAsync(k => k.Email == email && k.Password == password && k.IsDeleted == false, includeList);
        }


    }
}
