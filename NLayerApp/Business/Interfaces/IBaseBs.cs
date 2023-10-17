using Infrastructure.Utilites.ApiResponses;


namespace Business.Interfaces
{
    public interface IBaseBs<Response, Form, FilterFrom>
    {
        Task<ApiResponse<Response>> SingleGet(int id, int currentUserId, params string[] includeList);
        Task<ApiResponse<List<Response>>> MultipleGet(FilterFrom form, int currentUserId, params string[] includeList);
        Task<ApiResponse<Response>> Add(Form form, int currentUserId);
        Task<ApiResponse<NoData>> Update(Form form, int currentUserId);
        Task<ApiResponse<NoData>> Delete(int id, int currentUserId);
        Task<ApiResponse<NoData>> Delete(FilterFrom form, int currentUserId);
    }
    public interface IBaseBs2<Response, Form, FilterFrom>
    {
        Task<ApiResponse<Response>> SingleGet(long id, long currentUserId, params string[] includeList);
        Task<ApiResponse<List<Response>>> MultipleGet(FilterFrom form, long currentUserId, params string[] includeList);
        Task<ApiResponse<Response>> Add(Form form, long currentUserId);
        Task<ApiResponse<NoData>> Update(Form form, long currentUserId);
        Task<ApiResponse<NoData>> Delete(long id, long currentUserId);
        Task<ApiResponse<NoData>> Delete(FilterFrom form, long currentUserId);
    }
}
