﻿using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IBankBs : IBaseBs<BankDto.Response,BankDto.Form,BankDto.FilterForm>
    {
    }
}
