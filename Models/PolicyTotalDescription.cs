using System;
using System.Collections.Generic;

namespace EPROJECT.Models
{
    public partial class PolicyTotalDescription
    {
        public int? Policyid { get; set; }
        public string? Policyname { get; set; }
        public string? Policydes { get; set; }
        public decimal? Policyamount { get; set; }
        public decimal? Emi { get; set; }
        public int? Policydurationmonths { get; set; }
        public string? Companyname { get; set; }

        public virtual Policy? Policy { get; set; }
    }
}
