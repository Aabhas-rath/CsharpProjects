using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using BusinessLayer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLayer
{
    public class UserFunctions
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;

        public async Task<Boolean> CheckIfUsernameAvaliable(string username)
        {
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spCheckIfUserNameAvaliable", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter pUsername = new SqlParameter("@username", System.Data.SqlDbType.NVarChar);
                    pUsername.Value = username;
                    cmd.Parameters.Add(pUsername);

                    await con.OpenAsync();
                    if (cmd.ExecuteScalar() != null)
                    {
                        con.Close();
                        return false;
                    }
                    con.Close();
                    return true;
                }
            }
        }

        public async Task<Boolean> CheckIfUsernameHasMatchingPassword(string username, string password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter pUsername = new SqlParameter("@username", System.Data.SqlDbType.NVarChar);
                        pUsername.Value = username;
                        cmd.Parameters.Add(pUsername);

                        SqlParameter pPassword = new SqlParameter("@password", System.Data.SqlDbType.NVarChar);
                        pPassword.Value = password;
                        cmd.Parameters.Add(pPassword);

                        SqlParameter pRET = new SqlParameter("@RET", System.Data.SqlDbType.NVarChar);
                        pRET.Direction = System.Data.ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(pRET);

                        int RETVAL;
                        await con.OpenAsync();
                        if (cmd.ExecuteScalar() != null)
                        {
                            con.Close();
                            RETVAL = Convert.ToInt32(pRET.Value);
                            if (RETVAL==0)
                            {
                                return true;
                            }
                            else if (RETVAL==1)
                            {
                                throw new Exception("Wrong Password");
                            }
                            else
                            {
                                throw new Exception("Username doesn't exist");
                            }
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<User> LoginFunction(string username,string password)
        {
            try
            {
                if (await CheckIfUsernameHasMatchingPassword(username,password))
                    {
                        ReaderFunctions rf = new ReaderFunctions();
                        return await rf.getUserOfUsername(username);
                    }
                else
                    {
                        return null;
                    }
            }
            catch (Exception exx)
            {
                Exception ex = new Exception("Login Unsuccessfull", exx);
                throw ex;
            }
        }
    }
}
