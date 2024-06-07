using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
namespace trial.Pages
{
    public class GoalModel : PageModel
    
    {
        public List<GoalInfo> listGoals = new List<GoalInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-TVFTFOF;Initial Catalog=trial;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Goal WHERE UserID IN (SELECT UserID FROM SignUp) ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GoalInfo goalInfo = new GoalInfo();
								goalInfo.UserID = reader.GetInt32(0);
								goalInfo.TrainerID = reader.GetInt32(1);
                                goalInfo.GoalID = reader.GetInt32(2);
                                goalInfo.GoalType = reader.GetString(3);
                                goalInfo.TargetValue = reader.GetInt32(4);
                           
                               

                                listGoals.Add(goalInfo);
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

    public class GoalInfo
    {
        [Required]
        public int TrainerID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int GoalID { get; set; }
        [Required]
        public string GoalType { get; set; }
        [Required]
        public int TargetValue { get; set; }

    }

}