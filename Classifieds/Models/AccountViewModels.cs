using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Classifieds.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Username / Email")]
        public string Email { get; set; }
        [Display(Name = "Alias / Nickname")]
        public string Alias { get; set; }
        [Required]
        [Display(Name="Phone")]
        public string ClassifiedsPhone { get; set; }
        [Display(Name="Home / Buss address")]
        public string Address { get; set; }
        public string Fullname { get; set; }
        [Required(ErrorMessage="You need to accept the terms and conditions.")]
        public Boolean Terms { get; set; }

    }

    public class ExternalLoginListViewModel
    {
        public string Action { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
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
        [Display(Name = "Alias / Nickname"),MaxLength(10)]
        [Required]
        public string Alias { get; set; }

        [Required]
        [Display(Name = "Fullname")]
        [MaxLength(50)]
        public string FullName { get; set; }
        public string Sex { get; set; }
        [MaxLength(100),MinLength(5)]
        [Required]
        [Display(Name = "Phone")]
        public string ClassifiedsPhone { get; set; }
        [Display(Name="Home / Buss address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "You need to accept the terms and conditions.")]
        public Boolean Terms { get; set; }
        // Return a pre-poulated instance of AppliationUser:
        public ApplicationUser GetUser()
        {
            var user = new ApplicationUser()
            {
                //FullName = this.FullName,
                //Sex = this.Sex,
                //DOB = this.DOB,
                Email = this.Email,
                UserName = this.Email
            };
            return user;
        }
    }

    public class EditAccountViewModel
    {
        public EditAccountViewModel() { }
        public EditAccountViewModel(ApplicationUser user)
        {
            Email = user.Email;
            Address = user.Address;
            FullName = user.FullName;
            Sex = user.Sex;
            ClassifiedsPhone = user.ClassifiedsPhone;
            Pic = user.Pic;
            DOB = user.DOB;
            Alias = user.Alias;
        }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        [Display(Name = "Alias / Nickname")]
        public string Alias { get; set; }
        public string Pic { get; set; }
        [Required, Display(Name = "Fullname")]
        [MaxLength(50)]
        public string FullName { get; set; }
        public string Sex { get; set; }
        [MaxLength(100), MinLength(5)]
        [Required]
        [Display(Name = "Phone")]
        public string ClassifiedsPhone { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-mm-YY}")]
        [Display(Name = "Date of birth")]
        public DateTime? DOB { get; set; }
        // Return a pre-poulated instance of AppliationUser:
        
    }
    public class ResetPasswordViewModel
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

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class EditUserViewModel
    {
        public EditUserViewModel() { }

        // Allow Initialization with an instance of ApplicationUser:
        public EditUserViewModel(ApplicationUser user)
        {
            this.UserName = user.UserName;
           // this.FullName = user.FullName;
            //this.Sex = user.Sex;
           // this.DOB = user.DOB;
        }
        
        [Display(Name = "Fullname")]
        public string FullName { get; set; }
        [Display(Name = "Alias / Nickname")]
        public string Alias { get; set; }
        [Display(Name = "Sex")]
        public string Sex { get; set; }
        [Required]
        [Display(Name = "Phone")]
        [MaxLength(100),MinLength(5)]
        public string ClassifiedsPhone { get; set; }
        [Display(Name = "DOB")]
        public DateTime DOB { get; set; }
        [EmailAddress,Required,Display(Name="Email")]
        public string UserName { get; set; }

    }

    public class SelectUserRolesViewModel
    {
       public SelectUserRolesViewModel() 
        {
            this.Roles = new List<SelectRoleEditorViewModel>();
        }
  
  
        // Enable initialization with an instance of ApplicationUser:
        public SelectUserRolesViewModel(ApplicationUser user) : this()
        {
            this.UserName = user.UserName;
            //this.FullName = user.FullName;
           // this.DOB = user.DOB;
           // this.Sex = user.Sex;
            
            var Db = new ApplicationDbContext();
            
            // Add all available roles to the list of EditorViewModels:
            var allRoles = Db.Roles;
            foreach(var role in allRoles)
            {
                //An EditorViewModel will be used by Editor Template
                var rvm = new SelectRoleEditorViewModel(role);
                this.Roles.Add(rvm);
            }
            // Set the Selected property to true for those roles for 
            // which the current user is a member:
            foreach(var userRole in user.Roles)
            {
                var checkUserRole = this.Roles.Find(r => r.RoleName == userRole.RoleId);
                checkUserRole.Selected = true;
            }
        }
  
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Sex { get; set; }
        public DateTime DOB { get; set; }

        public List<SelectRoleEditorViewModel> Roles { get; set; }
    }

    // Used to display a single role with a checkbox, within a list structure:
    public class SelectRoleEditorViewModel
    {
        public SelectRoleEditorViewModel() { }
        public SelectRoleEditorViewModel(IdentityRole role)
        {
            this.RoleName = role.Name;
        }

        public bool Selected { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
