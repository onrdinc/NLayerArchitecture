using Infrastructure.Utilites.ApiResponses;


namespace Business.Interfaces
{
    public interface IBaseBs<Response, Form, FilterFrom>
    {
        Task<ApiResponse<Response>> SingleGet(int id, string currentUserId, params string[] includeList);
        Task<ApiResponse<List<Response>>> MultipleGet(FilterFrom form, string currentUserId, params string[] includeList);
        Task<ApiResponse<Response>> Add(Form form, string currentUserId);
        Task<ApiResponse<NoData>> Update(Form form, string currentUserId);
        Task<ApiResponse<NoData>> Delete(int id, string currentUserId);
        Task<ApiResponse<NoData>> Delete(FilterFrom form, string currentUserId);
    }
    public interface IBaseBs2<Response, Form, FilterFrom>
    {
        Task<ApiResponse<Response>> SingleGet(long id, string currentUserId, params string[] includeList);
        Task<ApiResponse<List<Response>>> MultipleGet(FilterFrom form, string currentUserId, params string[] includeList);
        Task<ApiResponse<Response>> Add(Form form, string currentUserId);
        Task<ApiResponse<NoData>> Update(Form form, string currentUserId);
        Task<ApiResponse<NoData>> Delete(long id, string currentUserId);
        Task<ApiResponse<NoData>> Delete(FilterFrom form, string currentUserId);
    }
}
