using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Service.Interface;
using Common;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Application.Service.Implementation
{
    public class GraderService : IGraderService
    {
        public IChatbotService _chatbotService { get; set; }

        public GraderService(IChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        public async Task<Result<GradedResult>> Grade(string assignment, IFormFile? file = null, List<IFormFile>? files = null)
        {
            string message = 
$@"You are a programming teacher grading student code. You MUST respond with ONLY valid JSON in the exact format below - no additional text before or after.

Required JSON format:

{{
    ""ProgrammingLanguage"": ""Language name (e.g., Python, Java, C++)"",
    ""Point"": 8,
    ""EvaluationCriteria"": {{
      ""Algorithm"": ""Brief comment about algorithm efficiency and correctness (max 50 words)"",
      ""CleanCode"": ""Brief comment about code quality, readability, and best practices (max 50 words)""
    }}
}}

Rules:
- Programming langauge: lowercase, full name (example: c sharp, java, c++, c, python,...)
- Point: Integer from 0-10 only
- Comments: Concise, specific, constructive feedback
- Response: Valid JSON only, no markdown code blocks or extra text
- Always include all required fields

Assignment: 
{assignment}

Student Code:
";
            var result = await _chatbotService.SendMessageAsync(message, file, files);
            string markdown = result.Data;
            string json = markdown.Replace("```json\n", "").Replace("\n```", "").Trim();
            Console.Write(json);
            GradedResult gradedResult = JsonConvert.DeserializeObject<GradedResult>(json);
            return Result<GradedResult>.Success(gradedResult);
        }
    }
}
