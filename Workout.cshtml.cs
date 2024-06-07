using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
namespace trial.Pages
{
    public class WorkoutModel : PageModel
    {
    

        public List<WorkoutInfo> listWorkout = new List<WorkoutInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-TVFTFOF;Initial Catalog=trial;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Workout WHERE UserID IN (SELECT UserID FROM SignUp) ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                WorkoutInfo workoutInfo = new WorkoutInfo();
                                workoutInfo.UserID = reader.GetInt32(0);
                                workoutInfo.WorkoutPlanDuration = reader.GetInt32(1);
                                workoutInfo.PlanLevel = reader.GetString(2);
                                workoutInfo.NumberOfWorkouts = reader.GetInt32(3);
                                workoutInfo.WorkoutName = reader.GetString(4);
								workoutInfo.NumberOfSets = reader.GetInt32(5);
								workoutInfo.NumberOfReps = reader.GetInt32(6);
								workoutInfo.TargetArea = reader.GetString(7);



								listWorkout.Add(workoutInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
            }
        }
    }

    public class WorkoutInfo
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public int WorkoutPlanDuration { get; set; }
        [Required]
        public string PlanLevel { get; set; }
        [Required]
        public int NumberOfWorkouts { get; set; }
        [Required]
        public string WorkoutName { get; set; }
        [Required]
        public int NumberOfSets { get; set; }
        [Required]
        public int NumberOfReps { get; set; }
        [Required]
        public string TargetArea { get; set; }

    }

}