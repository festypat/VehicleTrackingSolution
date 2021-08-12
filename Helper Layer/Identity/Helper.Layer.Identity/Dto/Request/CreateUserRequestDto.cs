using System.ComponentModel.DataAnnotations;

namespace Helper.Layer.Identity.Dto.Request
{
    public class CreateUserRequestDto
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

    }
}
