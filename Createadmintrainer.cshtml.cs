using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using trial.Pages;

namespace trial.Pages
{
    public class CreateadmintrainerModel : PageModel
    {

        public TrainerInfo trainerInfo = new TrainerInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {

            trainerInfo.TrainerName = Request.Form["TrainerName"];
            trainerInfo.number = Request.Form["number"];
            trainerInfo.CountryCode = Request.Form["CountryCode"];
            trainerInfo.Email =Request.Form["Email"];
            string birthdateString = Request.Form["Birthdate"];



            if (trainerInfo.TrainerName.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            if (trainerInfo.number.Length == 0)
            {
                errorMessage = "Invalid ID";
                return;
            }
            if (trainerInfo.CountryCode.Length == 0)
            {
                errorMessage = "Invalid ";
                return;
            }

            if (trainerInfo.Email.Length == 0)
            {
                errorMessage = "Invalid";
                return;
            }
            if (DateTime.TryParse(birthdateString, out DateTime birthdate))
            {
                trainerInfo.Birthdate = birthdate;
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
                    String sql = "INSERT INTO Trainer" +
                        "([TranierName],number,CountryCode,Email,Birthdate) VALUES" +
                        "(@TranierName,@number,@CountryCode,@Email,@Birthdate);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@TranierName", trainerInfo.TrainerName);
                        command.Parameters.AddWithValue("@number", trainerInfo.number);
                        command.Parameters.AddWithValue("@CountryCode", trainerInfo.CountryCode);
                        command.Parameters.AddWithValue("@Email", trainerInfo.Email);
                        command.Parameters.AddWithValue("@Birthdate", trainerInfo.Birthdate);
                       
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            trainerInfo.TrainerName = ""; trainerInfo.number = ""; trainerInfo.CountryCode = ""; trainerInfo.Email = ""; trainerInfo.Birthdate = DateTime.MinValue;
            successMessage = "New Trainer Added Correctly";
            Response.Redirect("/Admins");

        }
    }
}

