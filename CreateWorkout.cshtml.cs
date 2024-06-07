using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using trial.Pages;
namespace trial.Pages
{
    public class CreateWorkoutModel : PageModel
    {
    
		public WorkoutInfo workoutInfo = new WorkoutInfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
		{
		}
		public void OnPost()
		{

			workoutInfo.WorkoutPlanDuration = Convert.ToInt32(Request.Form["WorkoutPlanDuration"]);
			workoutInfo.PlanLevel = Request.Form["PlanLevel"];
			workoutInfo.NumberOfWorkouts = Convert.ToInt32(Request.Form["NumberOfWorkouts"]);
			workoutInfo.WorkoutName = Request.Form["WorkoutName"];
			workoutInfo.NumberOfSets = Convert.ToInt32(Request.Form["NumberOfSets"]);
			workoutInfo.NumberOfReps = Convert.ToInt32(Request.Form["NumberOfReps"]);
			workoutInfo.TargetArea = Request.Form["TargetArea"];



			if (workoutInfo.WorkoutPlanDuration <= 0)
			{
				errorMessage = "All the fields are required";
				return;
			}

			if (workoutInfo.PlanLevel.Length== 0)
			{
				errorMessage = "Invalid ";
				return;
			}
			if (workoutInfo.NumberOfWorkouts <= 0)
			{
				errorMessage = "Invalid ";
				return;
			}

			if (workoutInfo.WorkoutName.Length== 0)
			{
				errorMessage = "Invalid";
				return;
			}
			if (workoutInfo.NumberOfSets <= 0)
			{
				errorMessage = "Invalid ";
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
					String sql = "INSERT INTO Workout" +
						"(WorkoutPlanDuration,PlanLevel,NumberOfWorkouts,WorkoutName,NumberOfSets,NumberOfReps,TargetArea) VALUES" +
						"(@WorkoutPlanDuration,@PlanLevel,@NumberOfWorkouts,@WorkoutName,@NumberOfSets,@NumberOfReps,@TargetArea);";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{

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
			workoutInfo.WorkoutPlanDuration = 0; workoutInfo.PlanLevel = ""; workoutInfo.NumberOfWorkouts = 0; workoutInfo.WorkoutName = ""; workoutInfo.NumberOfSets = 0; workoutInfo.NumberOfReps = 0; workoutInfo.TargetArea = "";
			successMessage = "New Workout Plan Added Correctly";
			Response.Redirect("/Workout");

		}
	}
}

