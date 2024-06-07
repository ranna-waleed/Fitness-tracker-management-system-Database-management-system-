using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using trial.Pages;
namespace trial.Pages
{
    public class CreateadminuserModel : PageModel
    {

        public UsersInfo userInfo = new UsersInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {

            userInfo.UserName = Request.Form["UserName"];
            userInfo.number = Request.Form["number"];
            userInfo.CountryCode = Request.Form["CountryCode"];
            userInfo.Email = Request.Form["Email"];
            string birthdateString = Request.Form["Birthdate"];



            if (userInfo.UserName.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            if (userInfo.number.Length == 0)
            {
                errorMessage = "Invalid ";
                return;
            }
            if (userInfo.CountryCode.Length == 0)
            {
                errorMessage = "Invalid ";
                return;
            }

            if (userInfo.Email.Length == 0)
            {
                errorMessage = "Invalid";
                return;
            }
            if (DateTime.TryParse(birthdateString, out DateTime birthdate))
            {
                userInfo.Birthdate = birthdate;
            }
            else
            {
                errorMessage = "Invalid Birthdate format";
                return;
            }


            try
            {
                String connectionString = "Data Source=DESKTOP-TVFTFOF;Initial Catalog=trial;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO user_" +
                        "(UserName,number,CountryCode,Email,Birthdate) VALUES" +
                        "(@UserName,@number,@CountryCode,@Email,@Birthdate);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@UserName", userInfo.UserName);
                        command.Parameters.AddWithValue("@number", userInfo.number);
                        command.Parameters.AddWithValue("@CountryCode", userInfo.CountryCode);
                        command.Parameters.AddWithValue("@Email", userInfo.Email);
                        command.Parameters.AddWithValue("@Birthdate", userInfo.Birthdate);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            userInfo.UserName = ""; userInfo.number = ""; userInfo.CountryCode = ""; userInfo.Email = ""; userInfo.Birthdate = DateTime.MinValue;
            successMessage = "New User Added Correctly";
            Response.Redirect("/Admins");

        }
    }
}

