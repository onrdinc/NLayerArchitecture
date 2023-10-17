using DataAccess.Contexts;
using DataAccess.Interfaces;
using Infrastructure.DataAccess.Implementations;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class BankRepository : BaseRepository<Banks, DataContext, int>, IBankRepository
    {
        public BankRepository(DataContext context) : base(context)
        {
        }
    }
}
