using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using trial.Pages;

namespace trial.Pages
{
    public class CreateHealthModel : PageModel
    { 
        public HealthInfo healthInfo = new HealthInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {

            healthInfo.MetricsID = Convert.ToInt32(Request.Form["MetricsID"]);
            healthInfo.inbodyScore = Convert.ToInt32(Request.Form["inbodyScore"]);
            healthInfo.Weight = Convert.ToInt32(Request.Form["Weight"]);
            healthInfo.Height = Convert.ToInt32(Request.Form["Height"]);
            



            if (healthInfo.MetricsID <= 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            if (healthInfo.inbodyScore <= 0)
            {
                errorMessage = "Invalid ID";
                return;
            }
            if (healthInfo.Weight <= 0)
            {
                errorMessage = "Invalid ";
                return;
            }

            if (healthInfo.Height <= 0)
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
                    String sql = "INSERT INTO HealthMetrics" +
                        "(MetricsID,inbodyScore,Weight,Height) VALUES" +
                        "(@MetricsID,@inbodyScore,@Weight,@Height);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@MetricsID", healthInfo.MetricsID);
                        command.Parameters.AddWithValue("@inbodyScore", healthInfo.inbodyScore);
                        command.Parameters.AddWithValue("@Weight", healthInfo.Weight);
                        command.Parameters.AddWithValue("@Height", healthInfo.Height);
                               

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            healthInfo.MetricsID = 0; healthInfo.inbodyScore = 0; healthInfo.Weight = 0; healthInfo.Height = 0;
            successMessage = "New Health Metrics Plan Added Correctly";
            Response.Redirect("/Health");

        }
    }
}

