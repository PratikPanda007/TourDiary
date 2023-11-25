using System;
using System.Web;
using System.Linq;
using System.Data;
using TourDiary.Models;
using MySqlConnector;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace TourDiary.Helper
{
    public class DAUtils
    {
        private SqlConnection con;

        private void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["tourDB"].ToString();
            con = new SqlConnection(constr);
        }

        public User GetUserDetailsByUsername(String username)
        {
            Connection();

            // Target Stored Procedure
            SqlCommand com = new SqlCommand("GetUserDetailsByUsername", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Username", username);

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            // SP to Object Conversion
            con.Open();
            da.Fill(ds);
            con.Close();

            // User Object
            User user = new User();
            AesCryptoServiceProvider Aes = new AesCryptoServiceProvider();
            String aesKey = ConfigurationManager.AppSettings["AesKey"].ToString();

            try
            {
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    if (!String.IsNullOrEmpty(dr["UserName"].ToString()))
                    {
                        user = new User();

                        user.UserEmail = Convert.ToString(dr["UserName"]);
                        user.UserName = Convert.ToString(dr["UserName"]);
                        user.UserPassword = TextEncrypeDecrypt.DecryptString(aesKey, Convert.ToString(dr["UserPassword"]));
                        user.UserRole = Convert.ToString(dr["RoleId"]);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Err: "+ex.Message);
            }

            return user;
        }

        public void UpdatePassword(String UserName, String NewPassword)
        {
            Connection();

            String aesKey = ConfigurationManager.AppSettings["AesKey"].ToString();

            SqlCommand com = new SqlCommand("UpdatePassword", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@UserName", UserName);
            com.Parameters.AddWithValue("@NewPassword", TextEncrypeDecrypt.EncryptString(aesKey, NewPassword));

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            con.Open();
            da.Fill(ds);
            con.Close();
        }
    }
}