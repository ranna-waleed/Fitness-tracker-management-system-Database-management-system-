using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace trial.Pages
{
    public class UserModel : PageModel
    {
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            return RedirectToPage("/Health");
        }

        public IActionResult OnPostWorkoutPlan()
        {
            return RedirectToPage("/Workout");
        }
        public IActionResult OnPostGoals()
        {
            return RedirectToPage("/Goal");
        }
        public IActionResult OnPostNutritionPlan()
        {
            return RedirectToPage("/Nutrition");
        }
        public IActionResult OnPostSupportRequest()
        {
            return RedirectToPage("/SupportRequest");
        }


    }
}