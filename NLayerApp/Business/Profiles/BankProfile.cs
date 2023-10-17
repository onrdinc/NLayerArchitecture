using AutoMapper;
using Models.Dtos;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class BankProfile : Profile
    {
        public BankProfile()
        {
            CreateMap<Banks, BankDto.Response>();
            CreateMap<BankDto.Form, Banks>();
            CreateMap<BankDto.Response, Banks>();

        }
    }
}
