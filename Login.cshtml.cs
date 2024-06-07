using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using BCrypt.Net;
using System.ComponentModel.DataAnnotations;

namespace trial.Pages
{
    public class LoginModel : PageModel
    {
    

//        [BindProperty]
//        public LoginInfo? LoginInfo { get; set; }

//        public void OnGet() { }

//        public IActionResult OnPost()
//        {

//            if (LoginInfo.Email.StartsWith("a-"))
//            {
//                return RedirectToPage("/Admins");
//            }

//            if (LoginInfo.Email.StartsWith("u-"))
//            {
//                return RedirectToPage("/User");
//            }

//            if (LoginInfo.Email.StartsWith("t-"))
//            {
//                return RedirectToPage("/Trainer");
//            }

//            return Page();
//        }

//    }

//    public class LoginInfo
//    {
//        [BindProperty]
//        public string? Email { get; set; }
//        [BindProperty]
//        public string? Password { get; set; }
//    }
//}



        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
     
        public required string Password { get; set; }

        private string GetUserRoleUsingADO(string Email, string password)
        {
            using (SqlConnection connection =
                   new SqlConnection(
                       "Data Source=DESKTOP-TVFTFOF;Initial Catalog=trial;Integrated Security=True"))
            {
                connection.Open();

                // Check if the user is in the Student table.
                string studentQuery =
                    "SELECT Email FROM SignUp WHERE Email = @Email AND password = @Password";
                using (SqlCommand studentCommand = new SqlCommand(studentQuery, connection))
                {
                    studentCommand.Parameters.AddWithValue("@Email", Email);
                    studentCommand.Parameters.AddWithValue("@Password", password);

                    if (studentCommand.ExecuteScalar() != null)
                    {
                        return "student";
                    }
                }

                // Check if the user is in the Instructor table.
                //string instructorQuery =
                //    "SELECT instructor_id FROM instructor WHERE instructor_id = @UserId AND password = @Password";
                //using (SqlCommand instructorCommand = new SqlCommand(instructorQuery, connection))
                //{
                //    instructorCommand.Parameters.AddWithValue("@UserId", userId);
                //    instructorCommand.Parameters.AddWithValue("@Password", passwordInput);

                //    if (instructorCommand.ExecuteScalar() != null)
                //    {
                //        return "instructor";
                //    }
                //}

                // Handle the case where the user is not found in either table.
                return string.Empty;
            }
        }

        public IActionResult OnPost()
        {
            // Set User_ID with the appropriate value

            string userRole = GetUserRoleUsingADO(Email, Password);

            if (Email.StartsWith("a-"))
            {
                return RedirectToPage("/Admins", new { Email = Email });
            }

            if (Email.StartsWith("u-"))
            {
                return RedirectToPage("/User", new { Email = Email });
            }

            if (Email.StartsWith("t-"))
            {
                return RedirectToPage("/Trainer", new { Email = Email });
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return Page();
        }
           
        }
    
}

/////////////////////////////////////


//public IActionResult OnPost()
//{
//	loginInfo.Email = Request.Form["Email"];

//	// Validate input
//	if (string.IsNullOrEmpty(loginInfo.Email) || string.IsNullOrEmpty(loginInfo.Password))
//	{
//		errorMessage = "Email and Password are required.";
//		return Page();
//	}

//	try
//	{
//		string connectionString = "Data Source=DESKTOP-TVFTFOF;Initial Catalog=trial;Integrated Security=True";
//		using (SqlConnection connection = new SqlConnection(connectionString))
//		{
//			connection.Open();
//			string sql = "SELECT Email FROM SignUp WHERE Email = @Email";
//			using (SqlCommand command = new SqlCommand(sql, connection))
//			{
//				command.Parameters.AddWithValue("@Email", loginInfo.Email);

//				if (command.ExecuteScalar() != null)
//				{
//					// Redirect to the SignUp page
//					return "SignUp";
//				}
//			}
//		}
//	}
//	catch (Exception ex)
//	{
//		errorMessage = ex.Message;
//	}

//	// If email does not exist or an error occurred, stay on the current page
//	return Page();
//}
