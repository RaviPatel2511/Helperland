using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Helperland.Models
{
    public partial class User
    {
        public User()
        {
            FavoriteAndBlockedTargetUsers = new HashSet<FavoriteAndBlocked>();
            FavoriteAndBlockedUsers = new HashSet<FavoriteAndBlocked>();
            RatingRatingFromNavigations = new HashSet<Rating>();
            RatingRatingToNavigations = new HashSet<Rating>();
            ServiceRequestServiceProviders = new HashSet<ServiceRequest>();
            ServiceRequestUsers = new HashSet<ServiceRequest>();
            UserAddresses = new HashSet<UserAddress>();
        }

        public int UserId { get; set; }
        [Required(ErrorMessage = "First name is must")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Min 2 and Max 15 Characters  allow")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is must")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Min 2 and Max 15 Characters  allow")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is must")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Enter Valid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is must")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Mobile is must")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Enter Valid Mobile")]
        public string Mobile { get; set; }

        public int UserTypeId { get; set; }
        public int? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string UserProfilePicture { get; set; }
        public bool IsRegisteredUser { get; set; }
        public string PaymentGatewayUserRef { get; set; }
        public string ZipCode { get; set; }
        public bool WorksWithPets { get; set; }
        public int? LanguageId { get; set; }
        public int? NationalityId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? Status { get; set; }
        public string BankTokenId { get; set; }
        public string TaxNo { get; set; }

        public virtual ICollection<FavoriteAndBlocked> FavoriteAndBlockedTargetUsers { get; set; }
        public virtual ICollection<FavoriteAndBlocked> FavoriteAndBlockedUsers { get; set; }
        public virtual ICollection<Rating> RatingRatingFromNavigations { get; set; }
        public virtual ICollection<Rating> RatingRatingToNavigations { get; set; }
        public virtual ICollection<ServiceRequest> ServiceRequestServiceProviders { get; set; }
        public virtual ICollection<ServiceRequest> ServiceRequestUsers { get; set; }
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
    }
}
