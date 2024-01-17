using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IdentityManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

public class User{
  [Required(ErrorMessage = "Email is required.")]
  [EmailAddress]
  public string? EmailId{get;set;}

  [Required(ErrorMessage = "Password is required.")]
  [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please Provide Valid Email")]  
  [DataType(DataType.Password)]
  public string? userPassword{get;set;}
  

  // public string? role{get; set;}
  public bool keepLoggedIn{get;set;}

 

}

