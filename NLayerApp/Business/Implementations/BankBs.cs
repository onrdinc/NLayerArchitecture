using AutoMapper;
using Business.Interfaces;
using DataAccess.Interfaces;
using Infrastructure.Aspects.Caching;
using Infrastructure.Aspects.Performance;
using Infrastructure.CrossCuttingConcerns.Caching;
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
    public class BankBs : IBankBs
    {
        private readonly IBankRepository _repo;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public BankBs(IBankRepository repo, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        //[CacheRemoveAspect("IBankBs.MultipleGet")]
        public async Task<ApiResponse<BankDto.Response>> Add(BankDto.Form form, int currentUserId)
        {
            try
            {
                ApiResponse<BankDto.Response> rb = new ApiResponse<BankDto.Response>();



                if (string.IsNullOrEmpty(form.Name))
                {
                    rb.Status = 400;
                    rb.StatusTexts.Add("Kategori adı boş geçilemez.");
                    return await Task.FromResult(rb);
                }

                var categori = _mapper.Map<Banks>(form);
                categori.CreatedDate = DateTime.Now;
                categori.CreatedBy = currentUserId;
                var insert = await _repo.AddAsync(categori);
                await _unitOfWork.CommitAsync();
                var c = _mapper.Map<BankDto.Response>(insert);

                //rb = SingleGet(c.Id, currentUserId).Result;

                rb.Status = 200;
                rb.StatusTexts.Add("Kaydedildi.");
                return await Task.FromResult(rb);
                //return ApiResponse<MRoleGroupTypeDto.Response>.Success(StatusCodes.Status201Created, c);
            }
            catch (Exception ex)
            {

                throw await Task.FromResult(ex);
            }
        }

        [PerformanceAspect]

        public async Task<ApiResponse<NoData>> Delete(int id, int currentUserId)
        {
            try
            {
                ApiResponse<NoData> rb = new ApiResponse<NoData>();
                //var currentUser = await _.GetCurrentUserAsync(currentUserId);
                //if (currentUser == null)
                //{
                //    rb.StatusCode = 400;
                //    rb.StatusTexts.Add("Bu işlemi yapmaya yetkiniz yok!");
                //    return await Task.FromResult(rb);
                //}
                var category = await _repo.GetByIdAsync(id);
                if (category == null)
                {
                    rb.Status = 400;
                    rb.StatusTexts.Add("Kayıt Bulunamadı");
                    return await Task.FromResult(rb);
                }

                category.DeletedBy = currentUserId;
                _repo.SoftDelete(category);
                await _unitOfWork.CommitAsync();
                rb.Status = 200;
                rb.StatusTexts.Add("Kayıt başarıyla silindi");
                return await Task.FromResult(rb);
                //return ApiResponse<NoData>.Success(StatusCodes.Status200OK);

            }
            catch (Exception ex)
            {

                throw await Task.FromResult(ex);
            }
        }

        public Task<ApiResponse<NoData>> Delete(BankDto.FilterForm form, int currentUserId)
        {
            throw new NotImplementedException();
        }



        //[CacheAspect]
        [PerformanceAspect]
        public async Task<ApiResponse<List<BankDto.Response>>> MultipleGet(BankDto.FilterForm form, int currentUserId, params string[] includeList)
        {
            try
            {
                ApiResponse<List<BankDto.Response>> rb = new ApiResponse<List<BankDto.Response>>();




                var gr = _repo.GetAllAsync(k => k.IsDeleted == false);
                //var gr = await _repo.GetBanks();
                var returnList = _mapper.Map<List<BankDto.Response>>(gr);



                rb.Count = returnList.Count;
                rb.Item = returnList;
                return await Task.FromResult(rb);

                //var response = ApiResponse<List<MRoleGroupTypeDto.Response>>.Success(StatusCodes.Status200OK, returnList);

                //return response;
            }
            catch (Exception ex)
            {

                throw await Task.FromResult(ex);
            }
        }

        public Task<ApiResponse<BankDto.Response>> SingleGet(int id, int currentUserId, params string[] includeList)
        {
            throw new NotImplementedException();
        }

        [CacheRemoveAspect("IBankBs.MultipleGet")]
        public Task<ApiResponse<NoData>> Update(BankDto.Form form, int currentUserId)
        {
            throw new NotImplementedException();
        }
    }
}
