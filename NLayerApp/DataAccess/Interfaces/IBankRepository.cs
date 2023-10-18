using Infrastructure.DataAccess.Interfaces;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IBankRepository : IBaseRepository<Banks,int>
    {

        Task<List<Banks>>GetBanks();
    }
}
