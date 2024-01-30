using System.Collections.Generic;
using System.Threading.Tasks;
using LearningXamarin.Models.Responses;
using RestSharp;

namespace LearningXamarin.Services.APIClientService
{
    public class APIClientService : IAPIClientService
    {
        public const string URLBase = "https://fakestoreapi.com/";
        public RestClient client;

        public APIClientService()
        {
            var options = new RestClientOptions(URLBase);
            client = new RestClient(options);
        }

        public async Task<RestResponse<List<StoreProductResponse>>> GetProducts()
        {
            var request = new RestRequest("products");
            var restResponse = await client.ExecuteGetAsync<List<StoreProductResponse>>(request);

            return restResponse;
        }

        public async Task<RestResponse<List<string>>> GetCategories()
        {
            var request = new RestRequest("products/categories");
            var restResponse = await client.ExecuteGetAsync<List<string>>(request);

            return restResponse;
        }
    }
}
