using Infrastructure.Utilites.ApiResponses;
using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUserBs:IBaseBs2<UserDto.Response, UserDto.Form,UserDto.FilterForm>
    {
        Task<ApiResponse<UserDto.Response>> Login(UserDto.LoginForm form, long currentUserId, params string[] includeList);
    }
}
