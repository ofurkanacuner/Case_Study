#region - Using

using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace GeoSys.Domain.Helpers
{
    public interface IAPIClient
    {
        Task<ApiResponse> GetAsync(string endpoint, Dictionary<string, string> headers, string mediaType = "application/json");
        Task<ApiResponse> PostAsync(string endpoint, string data, Dictionary<string, string> headers, string mediaType = "application/json");
    }

    public class APIClient : IAPIClient
    {
        public async Task<ApiResponse> GetAsync(string endpoint, Dictionary<string, string> headers, string mediaType = "application/json")
        {
            var response = new ApiResponse();
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // add headers to request auth etc. 
            if (headers != null && headers.Keys.Count > 0)
            {
                foreach (var key in headers.Keys)
                {
                    //skip media type.
                    if (key.Equals("Content-Type"))
                    {
                        continue;
                    }
                    //httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + authstring);
                    httpClient.DefaultRequestHeaders.Add(key, headers[key]);
                }
            }
            var apiResponse = await httpClient.GetAsync(endpoint);

            //proccess response
            if (apiResponse.IsSuccessStatusCode)
            {
                //string responseData = await apiResponse.Content.ReadAsStringAsync();
                //string responseData = await apiResponse.Content.ReadAsAsync<string>();
                var responseStream = await apiResponse.Content.ReadAsStreamAsync();
                var responseData = "";
                using (var sr = new StreamReader(responseStream))
                {
                    responseData = await sr.ReadToEndAsync();
                }

                if (!string.IsNullOrWhiteSpace(responseData))
                {
                    response = JsonConvert.DeserializeObject<ApiResponse>(responseData);
                    //response.IsSuccess = true;
                    //response.Data = responseData;
                    //response.StatusCode = (int)apiResponse.StatusCode;
                }
            }
            else
            {
                response.Reason = apiResponse.ReasonPhrase;
                response.StatusCode = (int)apiResponse.StatusCode;
            }

            return response;
        }

        public async Task<ApiResponse> PostAsync(string endpoint, string data, Dictionary<string, string> headers, string mediaType = "application/json")
        {
            var response = new ApiResponse();
            var httpClient = new HttpClient();

            // add content
            // var content = new StringContent(data, Encoding.UTF8, "application/json");
            var content = new StringContent(data, Encoding.UTF8);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);

            // add headers to content auth etc. 
            if (headers != null && headers.Keys.Count > 0)
            {
                foreach (var key in headers.Keys)
                {
                    //skip media type.
                    if (key.Equals("Content-Type"))
                    {
                        continue;
                    }
                    httpClient.DefaultRequestHeaders.Add(key, headers[key]);
                }
            }
            // post
            var apiResponse = await httpClient.PostAsync(endpoint, content);

            //proccess response
            if (apiResponse.IsSuccessStatusCode)
            {
                var responseStream = await apiResponse.Content.ReadAsStreamAsync();
                var responseData = "";
                using (var sr = new StreamReader(responseStream))
                {
                    responseData = await sr.ReadToEndAsync();
                }

                if (!string.IsNullOrWhiteSpace(responseData))
                {
                    response.IsSuccess = true;
                    response.Data = responseData;
                    response.StatusCode = (int)apiResponse.StatusCode;
                }
            }
            else
            {
                response.Reason = apiResponse.ReasonPhrase;
                response.StatusCode = (int)apiResponse.StatusCode;
            }
            return response;
        }
    }
}