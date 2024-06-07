using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using trial.Pages;
namespace trial.Pages
{
    public class EditWorkoutModel : PageModel
    {
        public WorkoutInfo workoutInfo = new WorkoutInfo();
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
                    string sql = "SELECT * FROM Workout WHERE UserID=@UserID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                workoutInfo.UserID = reader.GetInt32(0);
                                workoutInfo.WorkoutPlanDuration = reader.GetInt32(1);
                                workoutInfo.PlanLevel = reader.GetString(2);
                                workoutInfo.NumberOfWorkouts = reader.GetInt32(3);
                                workoutInfo.WorkoutName = reader.GetString(4);
                                workoutInfo.NumberOfSets = reader.GetInt32(5);
                                workoutInfo.NumberOfReps = reader.GetInt32(6);
                                workoutInfo.TargetArea = reader.GetString(7);

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
            workoutInfo.UserID = Convert.ToInt32(Request.Form["UserID"]);
            workoutInfo.WorkoutPlanDuration = Convert.ToInt32(Request.Form["WorkoutPlanDuration"]);
            workoutInfo.PlanLevel = Request.Form["PlanLevel"];
            workoutInfo.NumberOfWorkouts = Convert.ToInt32(Request.Form["NumberOfWorkouts"]);
            workoutInfo.WorkoutName = Request.Form["WorkoutName"];
            workoutInfo.NumberOfSets = Convert.ToInt32(Request.Form["NumberOfSets"]);
            workoutInfo.NumberOfReps = Convert.ToInt32(Request.Form["NumberOfReps"]);
            workoutInfo.TargetArea = Request.Form["TargetArea"];




            if (workoutInfo.UserID <= 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            if (workoutInfo.WorkoutPlanDuration <= 0)
            {
                errorMessage = "Invalid ID";
                return;
            }
            if (workoutInfo.PlanLevel.Length == 0)
            {
                errorMessage = "Invalid ID";
                return;
            }
            if (workoutInfo.NumberOfWorkouts <= 0)
            {
                errorMessage = "Invalid ";
                return;
            }

            if (workoutInfo.WorkoutName.Length == 0)
            {
                errorMessage = "Invalid";
                return;
            }
            if (workoutInfo.NumberOfSets<= 0)
            {
                errorMessage = "Invalid ID";
                return;
            }
            if (workoutInfo.NumberOfReps <= 0)
            {
                errorMessage = "Invalid ";
                return;
            }

            if (workoutInfo.TargetArea.Length == 0)
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
    UPDATE Workout
    SET 
        WorkoutPlanDuration = @WorkoutPlanDuration,
        PlanLevel = @PlanLevel,
        NumberOfWorkouts = @NumberOfWorkouts,
        WorkoutName = @WorkoutName,
        NumberOfSets = @NumberOfSets,
        NumberOfReps = @NumberOfReps,
        TargetArea = @TargetArea
    WHERE UserID = @UserID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@UserID", workoutInfo.UserID);
                        command.Parameters.AddWithValue("@WorkoutPlanDuration", workoutInfo.WorkoutPlanDuration);
                        command.Parameters.AddWithValue("@PlanLevel", workoutInfo.PlanLevel);
                        command.Parameters.AddWithValue("@NumberOfWorkouts", workoutInfo.NumberOfWorkouts);
                        command.Parameters.AddWithValue("@WorkoutName", workoutInfo.WorkoutName);
                        command.Parameters.AddWithValue("@NumberOfSets", workoutInfo.NumberOfSets);
                        command.Parameters.AddWithValue("@NumberOfReps", workoutInfo.NumberOfReps);
                        command.Parameters.AddWithValue("@TargetArea", workoutInfo.TargetArea);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Workout");
        }
    }
}
