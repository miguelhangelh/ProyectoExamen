﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using CADCitasUM;

namespace ProyectoAtencion.Account
{
    public partial class Login : System.Web.UI.Page
    {
       public static int userId = 0;
 

        protected void ValidateUser(object sender, EventArgs e)
        {
             
            string constr = ConfigurationManager.ConnectionStrings["DBCITASConnectionString"].ConnectionString;
            
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Validate_User"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", Login1.UserName);
                    cmd.Parameters.AddWithValue("@Password", Login1.Password);
                    cmd.Connection = con;
                    con.Open();
                    userId = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                switch (userId)
                {
                    case -1:
                        Login1.FailureText = "nombre o contraseñas incorrecta.";
                        break;
                    case -2:
                        Login1.FailureText = "Esta cuenta no esta activada.";
                        break;
                    default:
                        FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet);
                        break;
                }
            }
        }
    }
}
