using AutoMapper;
using Business.Helper;
using Business.Interfaces;
using DataAccess.Interfaces;
using Infrastructure.UnitOfWorks.Interface;
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
    public class UserBs : IUserBs
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        public UserBs(IMapper mapper,IUserRepository repo,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repo = repo;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<UserDto.Response>> Add(UserDto.Form form, long currentUserId)
        {
            try
            {
                ApiResponse<UserDto.Response> rb = new ApiResponse<UserDto.Response>();
               
                //var currentUser = await _repo.GetCurrentUserAsync(currentUserId);
                //if (currentUser == null)
                //{
                //    rb.StatusCode = 400;
                //    rb.StatusTexts.Add("Bu işlemi yapmaya yetkiniz yok!");
                //    return await Task.FromResult(rb);
                //}

                

                var user = _mapper.Map<Users>(form);
                //user.CreatedBy = currentUserId;
                user.Password = form.Password.ConvertToMD5();
                var insert = await _repo.AddAsync(user);
                await _unitOfWork.CommitAsync();
                var u = _mapper.Map<UserDto.Response>(insert);

                rb.StatusCode = 200;
                rb.StatusTexts.Add("Kaydedildi");
                return await Task.FromResult(rb);

                //return ApiResponse<MUserDto.Response>.Success(StatusCodes.Status201Created, u);
            }
            catch (Exception ex)
            {

                throw await Task.FromResult(ex);
            }

        }

        public Task<ApiResponse<NoData>> Delete(long id, long currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<NoData>> Delete(UserDto.FilterForm form, long currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<UserDto.Response>>> MultipleGet(UserDto.FilterForm form, long currentUserId, params string[] includeList)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<UserDto.Response>> SingleGet(long id, long currentUserId, params string[] includeList)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<NoData>> Update(UserDto.Form form, long currentUserId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<UserDto.Response>> Login(UserDto.LoginForm form, long currentUserId, params string[] includeList)
        {
            try
            {
                ApiResponse<UserDto.Response> mr = new ApiResponse<UserDto.Response>();

                

                var user = await _repo.GetByUserAsync(form.Email, form.Password.ConvertToMD5(), includeList);
                if (user == null)
                {
                    mr.StatusCode = 400;
                    mr.StatusTexts.Add("Email veya şifrenizi kontrol edin!");
                    return await Task.FromResult(mr);

                }
                var response = _mapper.Map<UserDto.Response>(user);


                //var response = SingleGet(user.Id, user.Id, "Category").Result;
                mr.StatusCode = 200;
                mr.Item = response;
                mr.StatusTexts.Add("Giriş Başarılı");
                return await Task.FromResult(mr);


            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
