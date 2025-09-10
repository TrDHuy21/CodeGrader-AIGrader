using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class GradedResult
    {   
        public int UserId { get; set; }
        public int ProblemId { get; set; }
        public string ProgrammingLanguage { get; set; }
        public int Point { get; set; }
        public EvaluationCriteria EvaluationCriteria { get; set; }
        public DateTime SubmissionAt { get; set; }

    }
}
