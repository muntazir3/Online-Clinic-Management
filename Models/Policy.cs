using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPROJECT.Models
{
    public partial class Policy
    {
        public Policy()
        {
            PoliciesOnEmployees = new HashSet<PoliciesOnEmployee>();
            PolicyRequestDetails = new HashSet<PolicyRequestDetail>();
        }

        public int Policyid { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Policyname { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Policydesc { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public decimal? Amount { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public decimal? Emi { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int? Companyid { get; set; }
        public int? Medicalid { get; set; }

        public virtual CompanyDetail? Company { get; set; }
        public virtual PolicyApprovalDetail? PolicyApprovalDetail { get; set; }
        public virtual ICollection<PoliciesOnEmployee> PoliciesOnEmployees { get; set; }
        public virtual ICollection<PolicyRequestDetail> PolicyRequestDetails { get; set; }
    }
}
