using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class EvaluationCriteria
    {
        [Description("")]
        public string Algorithm { get; set; }

        public string CleanCode { get; set; }


    }
}
