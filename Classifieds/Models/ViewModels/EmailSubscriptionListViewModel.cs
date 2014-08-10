using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Classifieds.Library;
using Microsoft.AspNet.Identity;

namespace Classifieds.Models.ViewModels
{

    public class EmailSubscriptionListViewModel
    {
        public EmailSubscriptionListViewModel()
        {
            string userId = HttpContext.Current.User.Identity.GetUserId();
            ApplicationDbContext db = new ApplicationDbContext();
            
            var catHelper = new CategoryHelper();
            var cats = catHelper.subCategories(0);
            Groups = new List<Group>();
            if (!String.IsNullOrEmpty(userId))
            {
                var user = db.Users.Find(userId);
                if (user != null)
                {
                    Email = user.Email;
                    Name = user.Alias ?? user.FullName;
                }
            }
            foreach (var cat in cats)
            {
                Group grp = new Group { Cycle = "", Text = cat.Title, GroupId = cat.CategoryId };
                    var subsQ = from s in db.EmailSubscriptions
                                where s.UserId == userId && s.CategoryId == cat.CategoryId
                                select s;
                    var gp = subsQ.FirstOrDefault();
                    if (gp != null)
                    {
                        grp.Cycle = gp.Period;
                        Email = gp.Email;
                        Name = gp.Name;
                    }
                Groups.Add(grp);
            }
            

        }
        public int EmailSubscriptionId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
        [Required]
        public List<Group> Groups { get; set; }
        public string subscriptionType { get; set; }

    }
    public class Group
    {
        public int GroupId { get; set; }
        public string Text { get; set; }
        public string Cycle { get; set; }
    }
}