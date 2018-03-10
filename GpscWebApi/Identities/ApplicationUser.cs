using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GpscWebApi.Identities
{
    public class ApplicationUser : IdentityUser
    {
        
        [MaxLength(100)]
        public string FirstName { get; set; }

        
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }
    }
}