using System;
using System.Collections.Generic;

namespace EPROJECT.Models
{
    public partial class PolicyRequestDetail
    {
        public int Requestid { get; set; }
        public DateTime? Requestdate { get; set; }
        public int? Empid { get; set; }
        public int? Policyid { get; set; }
        public string? Policyname { get; set; }
        public decimal? Policyamount { get; set; }
        public decimal? Emi { get; set; }
        public int? Companyid { get; set; }
        public string? Companyname { get; set; }
        public string? Status { get; set; }

        public virtual EmpRegister? Emp { get; set; }
        public virtual Policy? Policy { get; set; }
    }
}
