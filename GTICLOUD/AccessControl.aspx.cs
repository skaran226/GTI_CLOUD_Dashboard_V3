using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace GTICLOUD
{
    public partial class AccessControl : System.Web.UI.Page
    {

        int response = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session[Macros.SESSION_KEY].ToString() == "" || Session[Macros.SESSION_KEY].ToString() == null)
                {


                    Response.Redirect("Default.aspx");

                }
                else {

                    if (Request.QueryString.Get("skey") == "" || Request.QueryString.Get("skey") == null)
                    {

                        Response.Redirect("Default.aspx");
                    
                    }
                        
                }
            }
            catch {

                Response.Redirect("Default.aspx");  
            }
          
        }

        private object getRandomString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString;
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            // getSession.Text = Request.QueryString.Get("skey");


            try
            {
                if (Session[Macros.SESSION_KEY].ToString() == "" || Session[Macros.SESSION_KEY].ToString() == null)
                {


                    Response.Redirect("Default.aspx");

                }
                else
                {

                    if (Request.QueryString.Get("skey") != "" || Request.QueryString.Get("skey") != null)
                    {
                        

                        if (choose.SelectedIndex == 0)
                        {
                            Response.Write("<script>alert('Please select your catagory');</script>");
                        }
                        else
                        {
                            int already_added_user = AlreadyAvailable();

                            if (already_added_user == 1)
                            {

                                Response.Write("<script>alert('User Already Available');</script>");
                            }
                            else {
                                AddAccessUser();
                            }
                          
                        }

                    }
                    else
                    {

                        Response.Redirect("Default.aspx");

                    }
                }

            }
            catch (Exception ex)
            {


                Response.Redirect("Default.aspx");

            }
            
        }

        private void AddAccessUser()
        {
            string query = DB_Querys.AddAccessControl();
            DB.CloseConn();
            SqlCommand cmd = DB.ExecuteReader(query);
            cmd.Parameters.AddWithValue("@sitekey", Request.QueryString.Get("skey"));
            cmd.Parameters.AddWithValue("@name", username.Text);
            cmd.Parameters.AddWithValue("@email", email.Text);
            cmd.Parameters.AddWithValue("@category", choose.SelectedValue);
            cmd.Parameters.AddWithValue("@authentication_key", getRandomString());
            cmd.Parameters.AddWithValue("@is_authenticate", 1);
            cmd.Parameters.AddWithValue("@created", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));
            cmd.Parameters.AddWithValue("@updated", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));

            if (choose.SelectedValue.ToLower().Equals(Macros.sADMIN))
            {

                cmd.Parameters.AddWithValue("@permission_level", Macros.iADMIN);
            }
            else if (choose.SelectedValue.ToLower().Equals(Macros.sACCOUNT_MANAGER))
            {

                cmd.Parameters.AddWithValue("@permission_level", Macros.iACCOUNT_MANAGER);
            }
            else if (choose.SelectedValue.ToLower().Equals(Macros.sTECHNICIAN))
            {
                cmd.Parameters.AddWithValue("@permission_level", Macros.iTECHNICIAN);

            }
            else if (choose.SelectedValue.ToLower().Equals(Macros.sMANAGER))
            {
                cmd.Parameters.AddWithValue("@permission_level", Macros.iMANAGER);

            }
            else if (choose.SelectedValue.ToLower().Equals(Macros.sEMPLOYEE))
            {
                cmd.Parameters.AddWithValue("@permission_level", Macros.iEMPLOYEE);

            }
            else if (choose.SelectedValue.ToLower().Equals(Macros.sCLIENT))
            {
                cmd.Parameters.AddWithValue("@permission_level", Macros.iCLIENT);

            }
            else
            {

                cmd.Parameters.AddWithValue("@permission_level", Macros.iNOTACCESS);

            }

            int res = cmd.ExecuteNonQuery();
            cmd.Dispose();
            DB.CloseConn();

            if (res == 1)
            {
                Response.Write("<script>alert('Signup Successfull!');</script>");


               //send mail for access users 

            }
            else
            {
                Response.Write("<script>alert('Somthing Error!');</script>");

            }
        }

        private int AlreadyAvailable()
        {
            
           
            string query = DB_Querys.CheckAlready();
            DB.CloseConn();
            SqlCommand cmd = DB.ExecuteReader(query);
            SqlDataReader dbr = cmd.ExecuteReader();


            if (dbr.HasRows == false)
            {

                response = 0;
            }
            else {

                while (dbr.Read()) {

                    if (dbr["email"].ToString() == email.Text && dbr["sitekey"].ToString() == Request.QueryString.Get("skey"))
                    {

                        response = 1;
                        break;
                        

                    }
                    else {

                        response = 0;
                    }
                        
                }
            }

            cmd.Dispose();
            dbr.Dispose();
            DB.CloseConn();


            return response;
            

            

            
        }


    }
}