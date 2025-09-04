using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Http;

namespace Application.Service.Interface
{
    public interface IChatbotService
    {
        public Task<Result<string>> SendMessageAsync(string message, IFormFile? file = null, List<IFormFile>? files = null);
        public Task<Result<T>> SendMessageAsync<T>(string message, IFormFile? file = null, List<IFormFile>? files = null) where T : class;
    }
}
