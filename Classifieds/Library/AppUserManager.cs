using Classifieds.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;


namespace Classifieds.Library
{
    public class AppUserManager : UserManager<ApplicationUser> 
    {
        public AppUserManager():base(new UserStore<ApplicationUser>(new ApplicationDbContext()))
        {
           // PasswordValidator = new MinimumLengthValidator(5);

            PasswordValidator = new CustomizePasswordValidation(5);
        }
    }   
    public class CustomizePasswordValidation : IIdentityValidator<string>
    {
        public int LengthRequired { get; set; } 

        public CustomizePasswordValidation(int length)
        {
            LengthRequired = length;
        }

        public Task<IdentityResult> ValidateAsync(string Item)
        {
            if (String.IsNullOrEmpty(Item) || Item.Length < LengthRequired)
            {
                return Task.FromResult(IdentityResult.Failed(String.Format("Minimum Password Length Required is:{0}", LengthRequired)));
            }

            //string PasswordPattern = @"^(?=.*[0-9])(?=.*[!@#$%^&*])[0-9a-zA-Z!@#$%^&*0-9]{10,}$";

            //if (!Regex.IsMatch(Item, PasswordPattern))
            //{
            //    return Task.FromResult(IdentityResult.Failed(String.Format("The Password must have at least one numeric character.")));
            //}

            return Task.FromResult(IdentityResult.Success);
        }
    }
}