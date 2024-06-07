using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using trial.Pages;
namespace trial.Pages
{
    public class EditGoalModel : PageModel
    {
        public GoalInfo goalInfo = new GoalInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String UserID = Request.Query["UserID"];


            try
            {
                string connectionString = "Data Source=DESKTOP-TVFTFOF;Initial Catalog=trial;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Goal WHERE UserID=@UserID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                goalInfo.UserID = reader.GetInt32(0);
                                goalInfo.TrainerID = reader.GetInt32(1);
                                goalInfo.GoalID = reader.GetInt32(2);
                                goalInfo.GoalType = reader.GetString(3);
                                goalInfo.TargetValue = reader.GetInt32(4);


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            goalInfo.UserID = Convert.ToInt32(Request.Form["UserID"]);
            goalInfo.TrainerID = Convert.ToInt32(Request.Form["TrainerID"]);
            goalInfo.GoalID = Convert.ToInt32(Request.Form["GoalID"]);
            goalInfo.GoalType = Request.Form["GoalType"];
            goalInfo.TargetValue = Convert.ToInt32(Request.Form["TargetValue"]);




            if (goalInfo.UserID <= 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            if (goalInfo.TrainerID <= 0)
            {
                errorMessage = "Invalid ID";
                return;
            }
            if (goalInfo.GoalID <= 0)
            {
                errorMessage = "Invalid ID";
                return;
            }
            if (goalInfo.GoalType.Length == 0)
            {
                errorMessage = "Invalid ";
                return;
            }

            if (goalInfo.TargetValue <= 0)
            {
                errorMessage = "Invalid";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-TVFTFOF;Initial Catalog=trial;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = @"
    UPDATE Goal
    SET 
        TrainerID = @TrainerID,
        GoalID = @GoalID,
        GoalType = @GoalType,
        TargetValue = @TargetValue
    WHERE UserID = @UserID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@UserID", goalInfo.UserID);
                        command.Parameters.AddWithValue("@TrainerID", goalInfo.TrainerID);
                        command.Parameters.AddWithValue("@GoalID", goalInfo.GoalID);
                        command.Parameters.AddWithValue("@GoalType", goalInfo.GoalType);
                        command.Parameters.AddWithValue("@TargetValue", goalInfo.TargetValue);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Goal");
        }
    }
}
