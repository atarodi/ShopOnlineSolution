namespace ShopOnline.Web.Services.Contracts
{
    public interface IAnalysisService
    {
        Task<TimeSpan> GetRandomDataWithHttpClientTimeSpan();
    }
}
