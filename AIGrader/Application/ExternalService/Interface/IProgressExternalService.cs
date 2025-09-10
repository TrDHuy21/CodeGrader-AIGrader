using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Domain.Model;

namespace Application.ExternalService.Interface
{
    public interface IProgressExternalService
    {
        Task<Result> AddProgress(GradedResult gradedResult);
    }
}
