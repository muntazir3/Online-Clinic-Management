using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPROJECT.Models
{
    public partial class EmpRegister
    {
        public EmpRegister()
        {
            PoliciesOnEmployees = new HashSet<PoliciesOnEmployee>();
            PolicyRequestDetails = new HashSet<PolicyRequestDetail>();
        }

        public int Empid { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Designation { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public DateTime? JoinDate { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public decimal? Salary { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Contactno { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? City { get; set; }

        public virtual ICollection<PoliciesOnEmployee> PoliciesOnEmployees { get; set; }
        public virtual ICollection<PolicyRequestDetail> PolicyRequestDetails { get; set; }
       
    }
}
