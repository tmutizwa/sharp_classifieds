using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;

namespace Classifieds.Models.ViewModels
{
    public class EmailSubscriptionViewModel
    {
        public EmailSubscriptionViewModel(){
            string userId = HttpContext.Current.User.Identity.GetUserId();
            ApplicationDbContext db = new ApplicationDbContext();
            if (!String.IsNullOrEmpty(userId))
            {
                var subsQ = from s in db.EmailSubscriptions
                            where s.UserId == userId 
                            select s;
                var gp = subsQ.FirstOrDefault();
                if (gp != null)
                {
                    Email = gp.Email;
                    Name = gp.Name;
                }
                else
                {
                    var user = db.Users.Find(userId);
                    if (user != null)
                    {
                        Email = user.Email;
                        Name = user.Alias;
                    }
                }

            }
        }
        [Required(ErrorMessage="Please choose group.")]

        public int GroupId { get; set; }
        [Required]
        public string Cycle { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Choice { get; set; }
    }
}