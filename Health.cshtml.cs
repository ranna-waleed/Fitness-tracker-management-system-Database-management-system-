using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace trial.Pages
{
    public class HealthModel : PageModel
    {

        public List<HealthInfo> listHealth = new List<HealthInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-TVFTFOF;Initial Catalog=trial;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM HealthMetrics WHERE UserID IN (SELECT UserID FROM SignUp) ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
								HealthInfo healthInfo = new HealthInfo();
								healthInfo.UserID =  reader.GetInt32(0);
								healthInfo.MetricsID = reader.GetInt32(1);
								healthInfo.inbodyScore = reader.GetInt32(2);
								healthInfo.Weight = reader.GetInt32(3);
								healthInfo.Height = reader.GetInt32(4);
                           


								listHealth.Add(healthInfo);
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

    public class HealthInfo
	{
        [Required]
        public int UserID { get; set; }
        [Required]
        public int MetricsID { get; set; }
        [Required]
        public int inbodyScore { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public int Height { get; set; }

  
    }

}