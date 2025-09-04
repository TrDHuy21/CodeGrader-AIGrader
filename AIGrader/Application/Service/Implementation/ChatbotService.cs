using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Service.Interface;
using Common;
using Common.Extension;
using GenerativeAI;
using GenerativeAI.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Service.Implementation
{
    public class ChatbotService : IChatbotService
    {
        private readonly ILogger<ChatbotService> _logger;
        private readonly IConfiguration _configuration;
        private readonly GenerativeModel _model;

        public ChatbotService(IConfiguration configuration, ILogger<ChatbotService> logger)
        {
            _configuration = configuration;
            var apiKey = _configuration["Gemini:ApiKey"];
            var geminiModel = _configuration["Gemini:Model"];
            _model = new GenerativeModel(apiKey, geminiModel);
            _logger = logger;
        }

      

        public async Task<Result<string>> SendMessageAsync(string message, IFormFile file = null, List<IFormFile> files = null)
        {
            try
            {
                var resquest = new GenerateContentRequest();
                resquest.AddText(message);
                resquest.AddFile(file);
                resquest.AddMultiFile(files);
                var response = await _model.GenerateContentAsync(resquest);
                return Result<String>.Success(response.Text());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending message: {Message}", message);
                return Result<string>.Failure(ex.Message, null);
            }
        }

        public async Task<Result<T>> SendMessageAsync<T>(string message, IFormFile file = null, List<IFormFile> files = null) where T : class 
        {
            try
            {
                var resquest = new GenerateContentRequest();
                resquest.AddText(message);
                resquest.AddFile(file);
                resquest.AddMultiFile(files);
                var response = await _model.GenerateContentAsync<T>(resquest);
                Console.WriteLine(response.Text());
                var myObject = response.ToObject<T>();
                return Result<T>.Success(myObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending message: {Message}", message);
                return Result<T>.Failure(ex.Message, null);
            }
        }
    }
}
