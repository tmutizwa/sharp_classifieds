using Classifieds.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace Classifieds.Areas.Admin.Models
{
    public class AdminAccountViewModel
    {
    }


    public class AdminRegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //terry added
        [Required, Display(Name = "Fullname")]
        [MaxLength(50)]
        public string FullName { get; set; }
        public Boolean Suspended { get; set; }
       
        // Return a pre-poulated instance of AppliationUser:
        public ApplicationUser GetUser()
        {
            var user = new ApplicationUser()
            {
                FullName = this.FullName,
                Email = this.Email,
                UserName = this.Email,
                Suspended = this.Suspended
            };
            return user;
        }
    }

    public class AdminEditUserViewModel
    {
     
        public AdminEditUserViewModel() { }
     

        // Allow Initialization with an instance of ApplicationUser:
        public AdminEditUserViewModel(ApplicationUser user)
        {
            var Db = new ApplicationDbContext();
          
            FullName = user.FullName;
            Email    = user.Email;
            Suspended = user.Suspended;
            UserName = user.UserName;
            Alias = user.Alias;
            Confirmed = user.EmailConfirmed;

            
            AllRoles = new List<AdminRoleEditorViewModel>();
            UserRoles = new List<AdminRoleEditorViewModel>();
            var DbRoles = Db.Roles.ToList();
            var UsrRoles = user.Roles.ToList();
            foreach (var DbRole in DbRoles)
            {
                this.AllRoles.Add(new AdminRoleEditorViewModel(DbRole));
            }
            foreach (var UsrRole in UsrRoles)
            {
                var userRole = this.AllRoles.Find(r => r.RoleId == UsrRole.RoleId);
                this.UserRoles.Add(userRole);
            }
        }
        
        [Required]
        [Display(Name = "Fullname")]
        public string FullName { get; set; }
        public Boolean Confirmed { get; set; }
        public string Alias { get; set; }
        public Boolean Suspended { get; set; }
        [EmailAddress, Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string NewPasswordRepeat { get; set; }
        [Required,MaxLength(100,ErrorMessage="Username too long")]
        public string UserName { get; set; }
        public List<AdminRoleEditorViewModel> AllRoles { get; set; }
        public List<AdminRoleEditorViewModel> UserRoles { get; set; }

    }

    public class AdminUserRolesViewModel
    {
        public AdminUserRolesViewModel()
        {
            this.Roles = new List<AdminRoleEditorViewModel>();
        }


        // Enable initialization with an instance of ApplicationUser:
        public AdminUserRolesViewModel(ApplicationUser user) : this()
        {
            this.UserName = user.UserName;
            this.FullName = user.FullName;
            // this.DOB = user.DOB;
            // this.Sex = user.Sex;

            var Db = new ApplicationDbContext();

            // Add all available roles to the list of EditorViewModels:
            var allRoles = Db.Roles;
            foreach (var role in allRoles)
            {
                //An EditorViewModel will be used by Editor Template
                var rvm = new AdminRoleEditorViewModel(role);
                this.Roles.Add(rvm);
            }
            // Set the Selected property to true for those roles for which the current user is a member
            foreach (var userRole in user.Roles)
            {
                var checkUserRole = this.Roles.Find(r => r.RoleId == userRole.RoleId);
                if(checkUserRole != null)
                   checkUserRole.Selected = true;
            }
        }

        public string UserName { get; set; }
        public string FullName { get; set; }

        public List<AdminRoleEditorViewModel> Roles { get; set; }
    }
    // Used to display a single role with a checkbox, within a list structure:
    public class AdminRoleEditorViewModel
    {
        public AdminRoleEditorViewModel() { }
        public AdminRoleEditorViewModel(IdentityRole role)
        {
            this.RoleName = role.Name;
            this.RoleId = role.Id;
        }

        public bool Selected { get; set; }

        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
    }
}