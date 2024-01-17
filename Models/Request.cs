using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IdentityManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Drawing;
using System.Collections.Generic;

public class Request{
      [Key]
        public int ID { get; set; }

        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Please provide a valid Name")] 
         [StringLength(20, MinimumLength = 3, ErrorMessage = " Name Should be min 3 and max 20 length")]  
        public string? Name { get; set; }
       
        
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please Provide Valid Email")]  
        [EmailAddress]
        
        public string? UserId  { get; set; } 

        [Required(ErrorMessage = "Content is required.")]
        
        public string? request { get; set; }

        public string? ExistingData  { get; set; }

        public string? NewData { get; set; }

        public string? status { get; set; }
  
         public bool keepLoggedIn{get;set;}
}