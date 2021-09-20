using GeoSys.Domain.Helpers;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoSys.Services.UIServices
{
    public interface ICategoriesServices
    {
        Task<ApiResponse> GetCategori(int id);
        Task<ApiResponse> GetCategoriList();
    }

    public class CategoriServices : ICategoriesServices
    {
        private readonly IAPIClient apiClient;
        private readonly IConfiguration configuration;

        public CategoriServices(IAPIClient apiClient, IConfiguration configuration)
        {
            this.apiClient = apiClient;
            this.configuration = configuration;
        }

        public async Task<ApiResponse> GetCategori(int id)
        {
            var result = new ApiResponse
            {
                StatusCode = 400,
                IsSuccess = false
            };


            var entpoint = configuration.GetSection("Config:ApiUrl").Value;

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

        public async Task<ApiResponse> GetCategoriList()
        {
            var result = new ApiResponse
            {
                StatusCode = 400,
                IsSuccess = false
            };


            var entpoint = configuration.GetSection("Config:ApiUrl").Value;
            entpoint += "Categories";
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
