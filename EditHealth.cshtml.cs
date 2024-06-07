using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using trial.Pages;
namespace trial.Pages
{
    public class EditHealthModel : PageModel
    {
		public HealthInfo healthInfo = new HealthInfo();
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
					string sql = "SELECT * FROM HealthMetrics WHERE UserID=@UserID";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@UserID", UserID);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								healthInfo.UserID = reader.GetInt32(0);
								healthInfo.MetricsID = reader.GetInt32(1);
								healthInfo.inbodyScore = reader.GetInt32(2);
								healthInfo.Weight = reader.GetInt32(3);
								healthInfo.Height = reader.GetInt32(4);
								
								
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
			healthInfo.UserID = Convert.ToInt32(Request.Form["UserID"]);
			healthInfo.MetricsID = Convert.ToInt32(Request.Form["MetricsID"]);
			healthInfo.inbodyScore = Convert.ToInt32(Request.Form["inbodyScore"]);
			healthInfo.Weight = Convert.ToInt32(Request.Form["Weight"]);
			healthInfo.Height = Convert.ToInt32(Request.Form["Height"]);




			if (healthInfo.UserID <= 0)
			{
				errorMessage = "All the fields are required";
				return;
			}
			if (healthInfo.MetricsID <= 0)
			{
				errorMessage = "Invalid ID";
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
					String sql = @"
    UPDATE HealthMetrics
    SET 
        Height = @Height,
        MetricsID = @MetricsID,
        inbodyScore = @inbodyScore,
        Weight = @Weight
    WHERE UserID = @UserID";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{

						command.Parameters.AddWithValue("@UserID", healthInfo.UserID);
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
			Response.Redirect("/Health");
		}
	}
}
