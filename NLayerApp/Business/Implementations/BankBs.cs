using Business.Interfaces;
using DataAccess.Interfaces;
using Infrastructure.Utilites.ApiResponses;
using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class BankBs : IBankBs
    {
        private readonly IBankRepository _repo;
        public BankBs(IBankRepository repo)
        {
            _repo = repo;   
        }
        public Task<ApiResponse<BankDto.Response>> Add(BankDto.Form form, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<NoData>> Delete(int id, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<NoData>> Delete(BankDto.FilterForm form, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<BankDto.Response>>> MultipleGet(BankDto.FilterForm form, string currentUserId, params string[] includeList)
        {
            throw new NotImplementedException();

        }

        public Task<ApiResponse<BankDto.Response>> SingleGet(int id, string currentUserId, params string[] includeList)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<NoData>> Update(BankDto.Form form, string currentUserId)
        {
            throw new NotImplementedException();
        }
    }
}
