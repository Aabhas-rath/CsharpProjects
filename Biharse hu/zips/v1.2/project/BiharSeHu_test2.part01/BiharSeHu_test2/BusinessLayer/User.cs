using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class User
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
        public UserInfo userInfo { get; set; }
        public LoginInfo loginInfo { get; private set; }

        public void changeLoginInfo( LoginInfo li)
        {
            loginInfo = li;
        }
        public User()
        {

        }
        public User(LoginInfo li, UserInfo ui)
        {
            this.loginInfo = li;
            this.userInfo = ui;
        }
        public User(int userid,SqlConnection con)
        {
            using (SqlCommand cmd = new SqlCommand("", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter("@userId", System.Data.SqlDbType.Int);
                paramId.Value = userid;
                cmd.Parameters.Add(paramId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    userInfo = new UserInfo()
                    {
                        AdminId = Convert.ToInt32(reader[""]),
                        CreatedOn = Convert.ToDateTime(reader[""]),
                        DateOfBirth = Convert.ToDateTime(reader[""]),
                        DisplayName = reader[""].ToString(),
                        isAdmin = (Convert.ToInt32(reader[""]) == 1) ? true : false,
                        isAuthor = (Convert.ToInt32(reader[""]) == 1) ? true : false,
                        Name = reader[""].ToString(),
                        NoOfPosts = Convert.ToInt32(reader[""]),
                        NoOfTags = Convert.ToInt32(reader[""]),
                        userid = Convert.ToInt32(reader[""])
                    };
                    loginInfo = new LoginInfo()
                    {
                        AdminId = Convert.ToInt32(reader[""]),
                        EmailId = reader[""].ToString(),
                        Password = reader[""].ToString(),
                        Username = reader[""].ToString()
                    };
                }
            }
        }
    }
}
