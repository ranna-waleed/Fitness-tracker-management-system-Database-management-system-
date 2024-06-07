using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using trial.Pages;
//this page is the edit button in the trainer page 
namespace trial.Pages
{
    public class EditModel : PageModel
    
    {
        public NutritionInfo nutritionInfo = new NutritionInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String TrainerID = Request.Query["TrainerID"];

            try
            {
                string connectionString = "Data Source=DESKTOP-TVFTFOF;Initial Catalog=trial;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Nutrition WHERE TrainerID=@TrainerID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TrainerID", TrainerID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nutritionInfo.TrainerID = reader.GetInt32(0);
                               
                                nutritionInfo.Nutrition_planID = reader.GetInt32(1);
                                nutritionInfo.Nutrition_planDuration = reader.GetInt32(2);
                                nutritionInfo.MealName = reader.GetString(3);
                                nutritionInfo.MealID = reader.GetInt32(4);
                                nutritionInfo.Number_of_meals = reader.GetInt32(5);
                                nutritionInfo.Times_per_week = reader.GetInt32(6);
                                nutritionInfo.Calories = reader.GetInt32(7);
                                nutritionInfo.Protien = reader.GetInt32(8);

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
            nutritionInfo.TrainerID = Convert.ToInt32(Request.Form["TrainerID"]);
           
            nutritionInfo.Nutrition_planID = Convert.ToInt32(Request.Form["Nutrition_planID"]);
            nutritionInfo.Nutrition_planDuration = Convert.ToInt32(Request.Form["Nutrition_planDuration"]);
            nutritionInfo.MealName = Request.Form["MealName"];
            nutritionInfo.MealID = Convert.ToInt32(Request.Form["MealID"]);
            nutritionInfo.Number_of_meals = Convert.ToInt32(Request.Form["Number_of_meals"]);
            nutritionInfo.Times_per_week = Convert.ToInt32(Request.Form["Times_per_week"]);
            nutritionInfo.Calories = Convert.ToInt32(Request.Form["Calories"]);
            nutritionInfo.Protien = Convert.ToInt32(Request.Form["Protien"]);




            if (nutritionInfo.MealName.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            if (nutritionInfo.TrainerID <= 0)
            {
                errorMessage = "Invalid ID";
                return;
            }
            if (nutritionInfo.Nutrition_planID <= 0)
            {
                errorMessage = "Invalid ID";
                return;
            }
            if (nutritionInfo.Nutrition_planDuration <= 0)
            {
                errorMessage = "Invalid ";
                return;
            }

            if (nutritionInfo.MealID <= 0)
            {
                errorMessage = "Invalid";
                return;
            }
            if (nutritionInfo.Number_of_meals <= 0)
            {
                errorMessage = "Invalid";
                return;
            }

            if (nutritionInfo.Times_per_week <= 0)
            {
                errorMessage = "Invalid";
                return;
            }
            if (nutritionInfo.Calories <= 0)
            {
                errorMessage = "Invalid ";
                return;
            }

            if (nutritionInfo.Protien <= 0)
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
    UPDATE Nutrition
    SET 

        NutritionPlanID = @NutritionPlanID,
        NutritionPlanDuration = @NutritionPlanDuration,
        MealName = @MealName,
        MealID = @MealID,
        NumberofMeals = @NumberofMeals,
        TimeperWeek = @TimeperWeek,
        Calories = @Calories,
        Protein = @Protein
    WHERE TrainerID = @TrainerID";
					using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                      
                        command.Parameters.AddWithValue("@NutritionPlanID", nutritionInfo.Nutrition_planID);
                        command.Parameters.AddWithValue("@NutritionPlanDuration", nutritionInfo.Nutrition_planDuration);
                        command.Parameters.AddWithValue("@MealName", nutritionInfo.MealName);
                        command.Parameters.AddWithValue("@MealID", nutritionInfo.MealID);
                        command.Parameters.AddWithValue("@NumberofMeals", nutritionInfo.Number_of_meals);
                        command.Parameters.AddWithValue("@TimeperWeek", nutritionInfo.Times_per_week);
                        command.Parameters.AddWithValue("@Calories", nutritionInfo.Calories);
                        command.Parameters.AddWithValue("@Protein", nutritionInfo.Protien);
						command.Parameters.AddWithValue("@TrainerID", nutritionInfo.TrainerID);
						command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Trainer");
        }
    }
}
