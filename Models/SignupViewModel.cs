using System.ComponentModel.DataAnnotations;
namespace IdentityManagement.Models{
    public class SignupViewModel{

        [Key]
        public int id{get; set;}

        public string? name{get; set;}

         
        public string? userID{get; set;}

         [Required(ErrorMessage ="{0} is required.")]
         [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[$@$!%*?&])[A-Za-z\\d$@$!%*?&]{8,}", ErrorMessage = "The password must be between 8 and 16 characters long and contain at least one lowercase letter, one uppercase letter, one digit, and one special character (@#$%^&+=).")]
        public String? password{get; set;}

        
         [Required(ErrorMessage ="{0} is required.")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[$@$!%*?&])[A-Za-z\\d$@$!%*?&]{8,}", ErrorMessage = "The password must be between 8 and 16 characters long and contain at least one lowercase letter, one uppercase letter, one digit, and one special character (@#$%^&+=).")]
        public string? confirmpassword{get; set;}
    }
}