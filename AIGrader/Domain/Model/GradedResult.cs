using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class GradedResult
    {   
        public string ProgrammingLanguage { get; set; }
        public int Point { get; set; }
        public EvaluationCriteria EvaluationCriteria { get; set; }
    }
}
