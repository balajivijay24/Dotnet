using System;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using IdentityManagement.Models;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net;

namespace IdentityManagement.Models{
    public class Database{
           
    public bool KeepLoggedIn{get;set;}
    public static string? Name { get; private set; }

    static SqlConnection sqlconnection=new SqlConnection("Data Source=ASPLAP1368\\SQLEXPRESS;Initial Catalog=database;Integrated Security=True");
      
    public bool Admin{get;set;}

      [Required]
      [DataType(DataType.Password)]
      [MinLength(6,ErrorMessage="Code must contain 6 digits")]
      public string? userInput{get;set;}
      public string to="abinashabinash711@gmail.com";
  
      public static string? password{get;set;}
      private  static string? randomCode;
      public static string? mail { get;set; }
      


  
  static public string employeeLogin(User user)
     {
      sqlconnection.Open();
               SqlCommand command=new SqlCommand("VerifyEmployee",sqlconnection);
               
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmailId",user.EmailId);
                command.Parameters.AddWithValue("@userPassword", user.userPassword);
                int Count=Convert.ToInt32(command.ExecuteScalar());
                sqlconnection.Close();
               
                if(Count==1)
                {
                  mail=user.EmailId;
                  return "Employee";         
                }
            
                else{
                  
                  return "fails";
                
                 }
     }
  static public string adminLogin(User user)
        {
                
                sqlconnection.Open();
                SqlCommand command=new SqlCommand("VerifyAdmin",sqlconnection); 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmailId",user.EmailId);
                command.Parameters.AddWithValue("@userPassword", user.userPassword);
                int Count=Convert.ToInt32(command.ExecuteScalar());
                sqlconnection.Close();
                if(Count==1)
                {
                  return "Admin";         
                }
                
                  return "fails";
                
                     
    }
   public static string? display(String? Mail)
        {
                sqlconnection.Open();
                SqlCommand command=new SqlCommand("getName",sqlconnection); 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmailId",Mail);
                SqlDataReader sqlDataReader=command.ExecuteReader();
                while(sqlDataReader.Read()){
                    Name=(string)sqlDataReader["Name"];
                   
                }
                sqlconnection.Close();
                return Name;
    }

     public static string? displayEmployee(String? Mail)
        {
                sqlconnection.Open();
                SqlCommand command=new SqlCommand("getEmployeeName",sqlconnection); 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmailId",Mail);
                SqlDataReader sqlDataReader=command.ExecuteReader();
                while(sqlDataReader.Read()){
                    Name=(string)sqlDataReader["Name"];
                   
                }
                sqlconnection.Close();
                return Name;
    }

static public string sendEmail(string to, User employee){
                sqlconnection.Open();
                SqlCommand command=new SqlCommand("AvailableUser",sqlconnection); 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmailId",employee.EmailId);
                int Count=Convert.ToInt32(command.ExecuteScalar());
                sqlconnection.Close();
                if(Count==1)
                {
                  
                string from, passwords, messageBody;
                Random random =new Random();
                 randomCode=(random.Next(999999)).ToString();
                MailMessage message=new MailMessage();
                from="abinashabinash711@gmail.com";
                  passwords  = "vzkgbrayouwkqrit";
                 messageBody="Your Verification Code is "+randomCode;
                   message.To.Add(new MailAddress(to));
                   message.From=new MailAddress(from);
                     message.Body=messageBody;
                    message.Subject="password code";
                SmtpClient smtp=new SmtpClient("smtp.gmail.com");
        smtp.EnableSsl=true;
        smtp.Port=587;
        smtp.DeliveryMethod=SmtpDeliveryMethod.Network;
        smtp.Credentials=new NetworkCredential(from,passwords);
        smtp.Send(message);
        return "sent";       
                }
        return "no-user";
        
}

static public string sendEmployeeEmail(string to, User employee){
                sqlconnection.Open();
                SqlCommand command=new SqlCommand("AvailableEmployee",sqlconnection); 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmailId",employee.EmailId);
                int Count=Convert.ToInt32(command.ExecuteScalar());
                sqlconnection.Close();
                if(Count==1)
                {
                  
                string from, passwords, messageBody;
                Random random =new Random();
                 randomCode=(random.Next(999999)).ToString();
                MailMessage message=new MailMessage();
                from="abinashabinash711@gmail.com";
                  passwords  = "vzkgbrayouwkqrit";
                 messageBody="Your Verification Code is "+randomCode;
                   message.To.Add(new MailAddress(to));
                   message.From=new MailAddress(from);
                     message.Body=messageBody;
                    message.Subject="password code";
                SmtpClient smtp=new SmtpClient("smtp.gmail.com");
        smtp.EnableSsl=true;
        smtp.Port=587;
        smtp.DeliveryMethod=SmtpDeliveryMethod.Network;
        smtp.Credentials=new NetworkCredential(from,passwords);
        smtp.Send(message);
        return "sent";       
                }
        return "no-user";
        
}

static public string verifyCode(Database database){
  if(randomCode==database.userInput){
    
    return "code sent";
  }
  else{
    return "Invalid code";
  }
}
 public static string? sendPassword(string Mail)
        {
                User employee1=new User();
                sqlconnection.Open();
                SqlCommand command=new SqlCommand("sendPassword",sqlconnection); 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmailId",Mail);
                SqlDataReader sqlDataReader=command.ExecuteReader();
                while(sqlDataReader.Read()){
                    password=Convert.ToString(sqlDataReader[0]);
                }
                sqlconnection.Close();
                return password;
    }
        
        public static string? sendEmployeePassword(string Mail)
        {
                User employee1=new User();
                sqlconnection.Open();
                SqlCommand command=new SqlCommand("sendPassword",sqlconnection); 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmailId",Mail);
                SqlDataReader sqlDataReader=command.ExecuteReader();
                while(sqlDataReader.Read()){
                    password=Convert.ToString(sqlDataReader[0]);
                }
                sqlconnection.Close();
                return password;
    }
        
    }
}