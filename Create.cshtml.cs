using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using trial.Pages;
//this page is the create page of the (add user ) in the trainer page 
namespace trial.Pages
{
    public class CreateModel : PageModel
    {
       
        public NutritionInfo nutritionInfo = new NutritionInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            nutritionInfo.Nutrition_planID = Convert.ToInt32(Request.Form["Nutrition_planID"]);
            nutritionInfo.Nutrition_planDuration = Convert.ToInt32(Request.Form["Nutrition_planDuration"]);
            nutritionInfo.MealName = Request.Form["MealName"];
            nutritionInfo.MealID = Convert.ToInt32(Request.Form["MealID"]);
            nutritionInfo.Number_of_meals = Convert.ToInt32(Request.Form["Number_of_meals"]);
            nutritionInfo.Times_per_week = Convert.ToInt32(Request.Form["Times_per_week"]);
            nutritionInfo.Calories = Convert.ToInt32(Request.Form["Calories"]);
            nutritionInfo.Protien = Convert.ToInt32(Request.Form["Protien"]);
            


            if (nutritionInfo.MealName.Length == 0 )
            {
                errorMessage = "All the fields are required";
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
                    String sql = "INSERT INTO Nutrition" +
                        "(NutritionPlanID,NutritionPlanDuration,MealName,MealID,NumberofMeals,TimeperWeek,Calories,Protein) VALUES" +
                        "(@NutritionPlanID,@NutritionPlanDuration,@MealName,@MealID,@NumberofMeals,@TimeperWeek,@Calories,@Protein);";
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

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            nutritionInfo.MealName = ""; nutritionInfo.Nutrition_planID = 0; nutritionInfo.Nutrition_planDuration = 0; nutritionInfo.MealID = 0; nutritionInfo.Number_of_meals = 0; nutritionInfo.Times_per_week = 0; nutritionInfo.Calories = 0; nutritionInfo.Protien = 0;
            successMessage = "New User Added Correctly";
            Response.Redirect("/Trainer");

        }
    }
}

