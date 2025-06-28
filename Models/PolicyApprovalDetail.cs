using System;
using System.Collections.Generic;

namespace EPROJECT.Models
{
    public partial class PolicyApprovalDetail
    {
        public int? Policyid { get; set; }
        public int? Requestid { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
        public string? Status { get; set; }
        public string? Reason { get; set; }

        public virtual Policy Policy { get; set; } = null!;
    }
}
