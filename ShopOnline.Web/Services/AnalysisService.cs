using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;
using System.Net.Http.Json;

namespace ShopOnline.Web.Services
{
    public class AnalysisService : IAnalysisService
    {

        private readonly HttpClient httpClient;
        private readonly int count = 1000;


        public AnalysisService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<TimeSpan> GetRandomDataWithHttpClientTimeSpan()
        {
            try
            {
                var startTime=DateTime.Now;
                var response = await  this.httpClient.GetAsync($"api/Analysis/GetRandomData/{count}");
                //var products = await this.httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("/api/Product");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        throw new Exception($"No Content");
                    }
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }
                var EndTime=DateTime.Now;
                return EndTime - startTime;
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

       
    }
}
