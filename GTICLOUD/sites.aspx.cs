using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GTICLOUD
{
    public partial class sites : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int permission_level = 105;
            string postype = Request.QueryString.Get("postype");


            string query = DB_Querys.GetSites(postype);
            SqlCommand cmd = null;
            SqlDataReader dbr = null;
            GTICLOUD.navbar.dropstring = "";
            sitebox.Text = "";
            try
            {

                if ((Session[Macros.SESSION_KEY].ToString() == "" || Session[Macros.SESSION_KEY].ToString() == null))
                {


                    Response.Redirect("Default.aspx");

                }
                else
                {
                    if (permission_level == Macros.iSUPER_ADMIN)
                    {

                        GTICLOUD.navbar.dropstring += "  <li><a href='createsite.aspx'>Create Site</a></li>";
                        GTICLOUD.navbar.dropstring += "  <li><a href='#!'>Settings</a></li>";
                        GTICLOUD.navbar.dropstring += "  <li><a href='#!'>Logout</a></li>";
                    }
                    else {

                        GTICLOUD.navbar.dropstring += "  <li><a href='#!'>Settings</a></li>";
                        GTICLOUD.navbar.dropstring += "  <li><a href='#!'>Logout</a></li>";
                    }


                    try
                    {
                        DB.CloseConn();
                        cmd = DB.ExecuteReader(query);
                        dbr = cmd.ExecuteReader();

                        if (dbr.HasRows == false)
                        {
                            sitebox.Text += "<div><h4 class='center-align red-text'>No Data Available</h4></div>";

                        }
                        else
                        {

                            while (dbr.Read())
                            {





                                sitebox.Text += "<div class='col s12 m4'>";
                                sitebox.Text += "<div class='card white'>";
                                sitebox.Text += "<div class='card-content black-text'>";
                                if (permission_level == Macros.iSUPER_ADMIN)
                                {

                                    sitebox.Text += "<span class='card-title activator grey-text text-darken-4'>" + dbr["sitename"].ToString() + "<i class='material-icons right'>more_vert</i></span>";

                                }
                                else {

                                    sitebox.Text += "<span class='card-title  black-text'>" + dbr["sitename"].ToString() + "</span>";
                                
                                }
                                
                                sitebox.Text += "<p> POS ID : " + dbr["siteid"].ToString() + "</p>";
                                sitebox.Text += "<p> POS Type : " + dbr["postype"].ToString().ToUpper() + "</p>";
                                sitebox.Text += "<p> <span>Updated :</span><span>" + dbr["regitered"].ToString() + "</span> </p>";
                                sitebox.Text += " <p> <span>Created : </span><span>" + dbr["updated"].ToString() + "</span>  </p>";
                                sitebox.Text += "</div>";
                                if (permission_level == Macros.iSUPER_ADMIN) {

                                    sitebox.Text += @"<div class='card-reveal'>
                                  <span class='card-title grey-text text-darken-4'>Access Control<i class='material-icons right'>close</i></span><br/>
                                  <a class='waves-effect waves-light btn' href='AccessControl.aspx?skey=" + dbr["sitekey"].ToString() + "'>authorization</a>";
                                    sitebox.Text += "</div>       ";
                                }

                                sitebox.Text += "<div class='card-action'>";
                                sitebox.Text += "<a href='site.aspx?siteid=" + dbr["siteid"].ToString() + "&sitekey=" + dbr["sitekey"].ToString() + "' class='theme-color'>GO TO SITE</a>";
                                sitebox.Text += @"</div> </div> </div>";








                            }
                        }



                    }
                    catch (Exception ex)
                    {

                        Response.Redirect("Default.aspx");

                    }
                    finally
                    {

                        DB.CloseConn();
                        cmd.Dispose();
                        dbr.Dispose();
                    }



                }

            }
            catch (Exception ex)
            {

                Response.Redirect("Default.aspx");
            }
        }
    }
}