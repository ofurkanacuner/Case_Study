#region - Using

using GeoSys.Domain.Helpers;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace GeoSys.Services.UIServices
{
    public interface IProductsServices
    {
        Task<ApiResponse> GetProducts(int id);
        Task<ApiResponse> GetProductsList();
    }

    public class ProductsServices : IProductsServices
    {
        private readonly IAPIClient apiClient;
        private readonly IConfiguration configuration;

        public ProductsServices(IAPIClient apiClient, IConfiguration configuration)
        {
            this.apiClient = apiClient;
            this.configuration = configuration;
        }

        public async Task<ApiResponse> GetProducts(int id)
        {
            var result = new ApiResponse
            {
                StatusCode = 400,
                IsSuccess = false
            };


            var entpoint = configuration.GetSection("Config:ApiUrl").Value;
            entpoint += "Products/" + id;

            var headers = new Dictionary<string, string>();

            var response = await apiClient.GetAsync(entpoint, headers);

            if (response.IsSuccess)
            {
                result.IsSuccess = true;
                result.StatusCode = 200;
                result.Data = response.Data;
                result.DataObject = response.DataObject;
            }
            else
            {
                result.Reason = response.Reason;
            }

            return result;
        }

        public async Task<ApiResponse> GetProductsList()
        {
            var result = new ApiResponse
            {
                StatusCode = 400,
                IsSuccess = false
            };


            var entpoint = configuration.GetSection("Config:ApiUrl").Value;
            entpoint += "Products";
            var headers = new Dictionary<string, string>();

            var response = await apiClient.GetAsync(entpoint, headers);

            if (response.IsSuccess)
            {
                result.IsSuccess = true;
                result.StatusCode = 200;
                result.Data = response.Data;
                result.DataObject = response.DataObject;
            }
            else
            {
                result.Reason = response.Reason;
            }

            return result;
        }
    }
}
