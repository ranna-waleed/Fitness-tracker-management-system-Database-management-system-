using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
namespace trial.Pages
{
    public class NutritionModel : PageModel
    {
		public List<NutritionPlanInfo> listNutritionsPlan = new List<NutritionPlanInfo>();
		public void OnGet()
		{
			try
			{
				string connectionString = "Data Source=DESKTOP-TVFTFOF;Initial Catalog=trial;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string sql = "SELECT * FROM Nutrition /*n INNER JOIN NutritionPlan np ON n.TrainerID = np.Id*/";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								NutritionPlanInfo nutritionPlanInfo = new NutritionPlanInfo();
								nutritionPlanInfo.TrainerID = reader.GetInt32(0);
								
								nutritionPlanInfo.Nutrition_planID = reader.GetInt32(1);
								nutritionPlanInfo.Nutrition_planDuration = reader.GetInt32(2);
								nutritionPlanInfo.MealName = reader.GetString(3);
								nutritionPlanInfo.MealID = reader.GetInt32(4);
								nutritionPlanInfo.Number_of_meals = reader.GetInt32(5);
								nutritionPlanInfo.Times_per_week = reader.GetInt32(6);
								nutritionPlanInfo.Calories = reader.GetInt32(7);
								nutritionPlanInfo.Protien = reader.GetInt32(8);


								listNutritionsPlan.Add(nutritionPlanInfo);
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

	public class NutritionPlanInfo
	{
		[Required]
		public int TrainerID { get; set; }
	
		[Required]
		public int Nutrition_planID { get; set; }
		[Required]
		public int Nutrition_planDuration { get; set; }
		[Required]
		public string MealName { get; set; }
		[Required]
		public int MealID { get; set; }

		public int Number_of_meals { get; internal set; }

		public int Times_per_week { get; set; }
		[Required]
		public int Calories { get; set; }
		[Required]
		public int Protien { get; set; }
	}

}