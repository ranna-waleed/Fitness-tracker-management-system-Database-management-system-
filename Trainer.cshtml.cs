using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
// this is the trainer page 
namespace trial.Pages
{
    public class TrainerModel : PageModel
    { 
        public List<NutritionInfo> listNutritions = new List<NutritionInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-TVFTFOF;Initial Catalog=trial;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Nutrition ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                NutritionInfo nutritionInfo = new NutritionInfo();
                                nutritionInfo.TrainerID = reader.GetInt32(0);
								
								nutritionInfo.Nutrition_planID = reader.GetInt32(1);
                                nutritionInfo.Nutrition_planDuration = reader.GetInt32(2);
                                nutritionInfo.MealName = reader.GetString(3);
                                nutritionInfo.MealID = reader.GetInt32(4);
                                nutritionInfo.Number_of_meals = reader.GetInt32(5);
                                nutritionInfo.Times_per_week = reader.GetInt32(6);
                                nutritionInfo.Calories = reader.GetInt32(7);
                                nutritionInfo.Protien = reader.GetInt32(8);


                                listNutritions.Add(nutritionInfo);
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

    public class NutritionInfo
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