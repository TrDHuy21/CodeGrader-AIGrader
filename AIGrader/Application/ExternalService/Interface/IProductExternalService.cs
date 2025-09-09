using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.RequestDtos;
using Common;

namespace Application.ExternalService.Interface
{
    public interface IProductExternalService
    {
        Task<Result<ProblemExternalDto>> GetProblem(int id);    
    }
}
