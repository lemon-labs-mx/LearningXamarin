using System.Collections.Generic;
using System.Threading.Tasks;
using LearningXamarin.Models.Responses;
using RestSharp;

namespace LearningXamarin.Services.APIClientService
{
    public interface IAPIClientService
    {
        Task<RestResponse<List<string>>> GetCategories();

        Task<RestResponse<List<StoreProductResponse>>> GetProducts();
    }
}