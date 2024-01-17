using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IdentityManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Drawing;
using System.Collections.Generic;


namespace IdentityManagement.Models;
public partial class EmployeeData
{
   [Key]
    public int ID { get; set; }

  ///Name///
    [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide  Name")] 
    [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Please provide a valid Name")] 
    [StringLength(20, MinimumLength = 3, ErrorMessage = " Name Should be min 3 and max 20 length")]  
    public string? Name{get; set;}
    
    
    ///Profile Image//
    //[Required(ErrorMessage = "Please select an image file.")]
    //[RegularExpression(@"^.*\.(jpg|jpeg|png)$", ErrorMessage = "The file must be an image with a valid extension (e.g., jpg, jpeg, png).")]
    public string? ProfilePicture { get; set; }


    ///Email///
    [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Eamil")]  
    [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please Provide Valid Email")]  
    public string? UserID { get; set; }

    ///Pasword///
    [Required(ErrorMessage ="{0} is required.")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[$@$!%*?&])[A-Za-z\\d$@$!%*?&]{8,}", ErrorMessage = "The password must be between 8 to 16 characters long and contain at least one lowercase letter, one uppercase letter, one digit, and one special character (@#$%^&+=).")]
    public string? Password { get; set; }

      /// Emloyee Package//
     [Required(ErrorMessage ="{0} is required.")]
    [RegularExpression(@"^([1-9]\d{5,})$", ErrorMessage = "the EmployeePackage  does not match the specified pattern (i.e., it should be  greater than 100000)")] 
     public double EmployeePackage {get; set;}

    
    ///Gender///
    [Required(ErrorMessage ="{0} is required.")]
    [RegularExpression(@"^(Male|male|female|others|Female|Other)$", ErrorMessage = "The gender input does not match Male, Female, or Other")] 
     public string? Gender { get; set; } 

     public int Experience { get; set; }

    
    ////Mobile Number///
    [Required(ErrorMessage = "You must provide a phone number")]
    [Display(Name = "Mobile Number")]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^\(?([6-9]{1})\)?[-. ]?([0-9]{9})$", ErrorMessage = "Not a valid phone number")]
    public double MobileNumber { get; set; }

    
    ////Team Name///
    [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide  your Team Name")] 
    [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Please provide a valid Name")] 
    [StringLength(20, MinimumLength = 5, ErrorMessage = " TeamName Should be min 5 and max 20 length")] 
    public string? TeamName { get; set; }


    public string? Address { get; set; }

    ////Date of Birth////
    [Required( ErrorMessage = "Please Provide  your Date of Birth")] 
    [RegularExpression(@"^(0[1-9]|1[0-2])/(0[1-9]|[12][0-9]|3[01])/(19[6-9]\d|200[0-2])$", ErrorMessage = "Please provide  the date of birth in the format MM/DD/YYYY where the year is between 1960 and 2002.")] 
    public string? DateofBirth{get; set;}

    ////Aadhaar///
    [Required(ErrorMessage = "You must provide a Aadhaar number")]
    [RegularExpression( @"^\(?([2-9]{1})\)?[-. ]?([0-9]{11})$", ErrorMessage = "Not a valid Aadhaar number")]
    public  double Aadhaar{get; set;}
}
