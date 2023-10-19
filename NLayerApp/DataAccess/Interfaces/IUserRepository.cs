using Infrastructure.DataAccess.Interfaces;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserRepository : IBaseRepository<Users,long>
    {
        Task<Users> GetByUserAsync(string email, string password, params string[] includeList);
    }
}
