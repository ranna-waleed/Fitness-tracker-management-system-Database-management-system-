using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace trial.Pages
{
    public class SignUpModel : PageModel
    {
        //public List<SignUpInfo> list = new List<SignUpInfo>();
        public SignUpInfo signUpInfo = new SignUpInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {

            if (signUpInfo == null)
            {
                errorMessage = "Sign up information is null.";
                return;
            }

            signUpInfo.Name = Request.Form["Name"];
            signUpInfo.Email = Request.Form["Email"];
            signUpInfo.Password = Request.Form["Password"];
            string birthdateString = Request.Form["Birthdate"];
            signUpInfo.number = Request.Form["number"];
            signUpInfo.CountryCode = Request.Form["CountryCode"];



            // Hash password before saving
            string hashedPassword = HashPassword(signUpInfo.Password);

            if (string.IsNullOrEmpty(signUpInfo.Name) || string.IsNullOrEmpty(signUpInfo.Email) ||
                  string.IsNullOrEmpty(signUpInfo.Password) || string.IsNullOrEmpty(signUpInfo.number) ||
                  string.IsNullOrEmpty(signUpInfo.CountryCode))
            {
                errorMessage = "All the fields are required";
                return;
            }



            if (DateTime.TryParse(birthdateString, out DateTime birthdate))
            {
                signUpInfo.Birthdate = birthdate;
            }

             if (signUpInfo.Email.StartsWith("a-"))
    {
        // Admin role
        // Add any additional conditions for admin role if needed
    }
    else if (signUpInfo.Email.StartsWith("t-"))
    {
        // Trainer role
        // Add any additional conditions for trainer role if needed
    }
    else if (signUpInfo.Email.StartsWith("u-"))
    {
        // User role
        // Add any additional conditions for user role if needed
    }
    else
    {
        errorMessage = "Invalid email prefix. Please use 'a-', 't-', or 'u-'.";
        return;
    }

            try
            {
                String connectionString = "Data Source=DESKTOP-TVFTFOF;Initial Catalog=trial;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO SignUp" +
                        "(Name, Email ,Password, Birthdate,number,CountryCode) VALUES" +
                        "(@Name,@Email,@Password,@Birthdate,@number,@CountryCode);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@Name", signUpInfo.Name);
                        command.Parameters.AddWithValue("@Email", signUpInfo.Email);
                        //command.Parameters.AddWithValue("@Password", signUpInfo.Password);
                        command.Parameters.AddWithValue("@Birthdate", signUpInfo.Birthdate);
                        command.Parameters.AddWithValue("@number", signUpInfo.number);
                        command.Parameters.AddWithValue("@CountryCode", signUpInfo.CountryCode);
                        
                        command.Parameters.AddWithValue("@Password", hashedPassword);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                errorMessage = ex.Message;
                return;
            }
            signUpInfo = new SignUpInfo();
            successMessage = "Data Added Correctly";
            Response.Redirect("/Success");

        }
        

        // Helper method for password hashing
        private string HashPassword(string password)
        {
            const int saltSize = 16; // You can adjust the salt size
            const int iterationCount = 10000; // You can adjust the iteration count

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[saltSize]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterationCount);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, saltSize);
            Array.Copy(hash, 0, hashBytes, saltSize, 20);

            return Convert.ToBase64String(hashBytes);
        }

     

        public class SignUpInfo
        {
            public int id { get; set; }
            [Required(ErrorMessage = "Name is required.")]
            [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 30 characters.")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Invalid email address.")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Password is required.")]
            public string Password { get; set; }
            [Required(ErrorMessage = "Birthdate is required.")]
            public DateTime Birthdate { get; set; }

            [BindProperty]
            [Required(ErrorMessage = "Phone Number is required.")]
            [StringLength(11, ErrorMessage = "Phone Number must be at most 11 characters.")]
            [RegularExpression("^[0-9]*$", ErrorMessage = "Phone Number should contain only numbers.")]
            public string number { get; set; }
            [Required(ErrorMessage = "Country code is required.")]
            public string CountryCode { get; set; }

        }
    }
}
