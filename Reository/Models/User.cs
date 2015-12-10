using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Reository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Repository.Models
{
    public class User : IdentityUser
    {
        public User()
        {

        }

        [DataType(DataType.Date)]
        [Display(Name = "RegisterDateTime")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RegisterDateTime { get; set; }

        [Display(Name = "AvatarPath")]
        [Required]
        public string AvatarPath { get; set; }

        [ForeignKey("Province")]
        public int ProvinceId { get; set; }
        public virtual Provinces Province { get; set; }

        [ForeignKey("AgeRange")]
        public int AgeRangeId { get; set; }
        public virtual AgeRange AgeRange { get; set; }

        [ForeignKey("Education")]
        public int EducationId { get; set; }
        public virtual Education Education { get; set; }

        public string Gender { get; set; }
        //public virtual User_Questionnaire User_Questionnaire { get; set; }

        public virtual ICollection<Questionnaire> Questionnaires { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}