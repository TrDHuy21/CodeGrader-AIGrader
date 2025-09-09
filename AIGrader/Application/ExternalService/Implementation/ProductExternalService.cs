using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.RequestDtos;
using Application.ExternalService.Interface;
using Common;
using Microsoft.Extensions.Configuration;

namespace Application.ExternalService.Implementation
{
    public class ProductExternalService : IProductExternalService
    {
        protected IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductExternalService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public Task<Result<ProblemExternalDto>> GetProblem(int id)
        {
            throw new NotImplementedException();
        }
    }
}
