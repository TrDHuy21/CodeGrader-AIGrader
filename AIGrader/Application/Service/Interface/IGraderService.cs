using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Domain.Model;
using Microsoft.AspNetCore.Http;

namespace Application.Service.Interface
{
    public interface IGraderService
    {
        public Task<Result<GradedResult>> Grade(string assignment, IFormFile? file = null, List<IFormFile>? files = null);
        public Task<Result<GradedResult>> Grade(string assignment, int problemId, IFormFile? file = null, List<IFormFile>? files = null);
    }
}
