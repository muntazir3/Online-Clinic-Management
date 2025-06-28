using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPROJECT.Models
{
    public partial class CompanyDetail
    {
        public CompanyDetail()
        {
            Policies = new HashSet<Policy>();
        }

        public int Companyid { get; set; }
        [Required(ErrorMessage ="This field is required")]
        public string CompanyName { get; set; } = null!;
        [Required(ErrorMessage = "This field is required")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? CompanyUrl { get; set; }

        public virtual ICollection<Policy> Policies { get; set; }
    }
}
