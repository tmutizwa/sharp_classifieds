using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Classifieds.Library;

namespace Classifieds.Models.ViewModels
{
    
    public class EmailSubscriptionViewModel
    {
        public EmailSubscriptionViewModel()
        {
            var catHelper = new CategoryHelper();
            var cats = catHelper.subCategories(0);
            Groups = new List<Group>();
            foreach(var cat in cats){
                var qn = new Group {Text=cat.Title,GroupId=cat.CategoryId};
                Groups.Add(qn);
            }
        }
        public int EmailSubscriptionId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
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