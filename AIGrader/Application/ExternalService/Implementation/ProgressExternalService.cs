using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.ExternalService.Interface;
using Common;
using Domain.Model;
using Microsoft.Extensions.Configuration;

namespace Application.ExternalService.Implementation
{
    public class ProgressExternalService : IProgressExternalService
    {
        protected readonly    IConfiguration _configuration;
        protected readonly IHttpClientFactory _httpClientFactory;

        public ProgressExternalService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result> AddProgress(GradedResult gradedResult)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                string baseUrl = _configuration["ExternalService:BaseUrl"];
                string addProgressUrl = _configuration["ExternalService:ProgressService:addProgress"];

                // Serialize object thành JSON
                var jsonContent = JsonSerializer.Serialize(gradedResult);

                // Tạo HttpContent từ JSON
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");


                var response = await client.PostAsync($"{baseUrl}/{addProgressUrl}", httpContent);


                if (response.IsSuccessStatusCode)
                {
                    var responseResult = await response.Content.ReadFromJsonAsync<Result>();

                    if (responseResult == null)
                    {
                        return Result<bool>.Failure("Invalid response from progress service", null);
                    }

                    if (responseResult.IsSuccess)
                    {
                        return Result.Success();
                    }
                    else
                    {
                        return Result<bool>.Failure(responseResult.Message, null);
                    }
                }
                else
                {
                    return Result.Failure($"Cannot connect to progress service: {response.StatusCode}", null);
                }
            }
            catch(Exception ex)
            {
                return Result.Failure("Error while update progress", null);
            }
        }
    }
}
