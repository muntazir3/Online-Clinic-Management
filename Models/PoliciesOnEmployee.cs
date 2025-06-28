using System;
using System.Collections.Generic;

namespace EPROJECT.Models
{
    public partial class PoliciesOnEmployee
    {
        public int Empid { get; set; }
        public int Policyid { get; set; }
        public string? Policyname { get; set; }
        public decimal? Policyamount { get; set; }
        public decimal? Policyduration { get; set; }
        public decimal? Emi { get; set; }
        public DateTime? Pstartdate { get; set; }
        public DateTime? Penddate { get; set; }
        public int? Companyid { get; set; }
        public string? Companyname { get; set; }

        public virtual EmpRegister Emp { get; set; } = null!;
        public virtual Policy Policy { get; set; } = null!;
    }
}
