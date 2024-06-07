using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using trial.Pages;
namespace trial.Pages
{
    public class CreateGoalModel : PageModel
    {
        public GoalInfo goalInfo = new GoalInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {

            goalInfo.TrainerID = Convert.ToInt32(Request.Form["TrainerID"]);
            goalInfo.GoalID = Convert.ToInt32(Request.Form["GoalID"]);
            goalInfo.GoalType = Request.Form["GoalType"];
            goalInfo.TargetValue = Convert.ToInt32(Request.Form["TargetValue"]);




            if (goalInfo.TrainerID <= 0)
            {
                errorMessage = "All the fields are required";
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
                    String sql = "INSERT INTO Goal" +
                        "(TrainerID,GoalID,GoalType,TargetValue) VALUES" +
                        "(@TrainerID,@GoalID,@GoalType,@TargetValue);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

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
            goalInfo.TrainerID = 0; goalInfo.GoalID = 0; goalInfo.GoalType  = "" ; goalInfo.TargetValue = 0;
            successMessage = "New Goal Added Correctly";
            Response.Redirect("/Goal");

        }
    }
}

