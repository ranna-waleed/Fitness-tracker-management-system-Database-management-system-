using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace trial.Pages
{
    public class AdminsModel : PageModel
    {
        public List<TrainerInfo> listTrainers = new List<TrainerInfo>();
        public List<UsersInfo> listUsers = new List<UsersInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-TVFTFOF;Initial Catalog=trial;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Trainer";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TrainerInfo trainerInfo = new TrainerInfo();
                                trainerInfo.TrainerID = reader.GetInt32(0);
                                trainerInfo.TrainerName = reader.GetString(1);
                                trainerInfo.number = reader.GetString(2);
                                trainerInfo.CountryCode = reader.GetString(3);
                                trainerInfo.Email = reader.GetString(4);
                                trainerInfo.Birthdate = reader.GetDateTime(5);

                                listTrainers.Add(trainerInfo);

                            }
                        }
                    }

                    string sqlUsers = "SELECT * FROM user_";

                    using (SqlCommand commandUsers = new SqlCommand(sqlUsers, connection))
                    {
                        using (SqlDataReader readerUsers = commandUsers.ExecuteReader())
                        {
                            while (readerUsers.Read())
                            {
                                UsersInfo userInfo = new UsersInfo();
                                userInfo.UserID = readerUsers.GetInt32(0);
                                userInfo.UserName = readerUsers.GetString(1);
                                userInfo.number = readerUsers.GetString(2);
                                userInfo.CountryCode = readerUsers.GetString(3);
                                userInfo.Email = readerUsers.GetString(4);
                                userInfo.Birthdate = readerUsers.GetDateTime(5);

                                listUsers.Add(userInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
    public class TrainerInfo
    {
        public int TrainerID;
        public string TrainerName;

        public string number;
        public string CountryCode;

        public string Email;
        public DateTime Birthdate;


    }
    public class UsersInfo
    {
        public int UserID;
        public string UserName;

        public string number;
        public string CountryCode;
        public string Email;
        public DateTime Birthdate;


    }

}
