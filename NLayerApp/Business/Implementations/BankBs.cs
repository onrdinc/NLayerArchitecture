using AutoMapper;
using Business.Interfaces;
using DataAccess.Interfaces;
using Infrastructure.Utilites.ApiResponses;
using Models.Dtos;
using Models.Entities;
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
        private readonly IMapper _mapper;
        public BankBs(IBankRepository repo,IMapper mapper)
        {
            _repo = repo;   
            _mapper = mapper;
        }

        public Task<ApiResponse<BankDto.Response>> Add(BankDto.Form form, int currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<NoData>> Delete(int id, int currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<NoData>> Delete(BankDto.FilterForm form, int currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<BankDto.Response>>> MultipleGet(BankDto.FilterForm form, int currentUserId, params string[] includeList)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<BankDto.Response>> SingleGet(int id, int currentUserId, params string[] includeList)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<NoData>> Update(BankDto.Form form, int currentUserId)
        {
            throw new NotImplementedException();
        }
    }
}
