using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GTICLOUD
{
    public class DB_Querys
    {

        internal static string LocalSqlConn() {

            string query = "Data Source=LAPTOP-EMJVR3G8;Initial Catalog=gti_cloud;Integrated Security=True";
            return query;
        }

        internal static string CreateAccountQuery() {

            string query = "insert into registration (gti_user_name,email,category,password,created,updated) values (@username,@email,@category,@password,@created,@updated)";

            return query;
        }

        internal static string CreateSite() {

            string query = "insert into sites (sitename,siteid,postype,backofficeuserid,backofficepassword,regitered,updated) values (@sitename,@siteid,@postype,@backofficeuserid,@backofficepassword,@regitered,@updated)";

            return query;
        
        }

        internal static string GetRegisteredUsersQuery() {

            string query = "select email,password,category from registration";

            return query;
        
        }

        internal static string GetSideNav() {

            string query = "select *from sidenav";

            return query;
        
        }

        internal static string GetSites(string postype)
        {
            string query = "select sitekey,sitename,siteid,postype,regitered,updated from sites where postype='" + postype + "'";

            return query;
        }

        internal static string SiteServices() {

            return null;
        
        }

        internal static string AddAccessControl()
        {
            string query = "insert into accessControl (sitekey,name,email,category,authentication_key,is_authenticate,created,updated,permission_level) values(@sitekey,@name,@email,@category,@authentication_key,@is_authenticate,@created,@updated,@permission_level)";

            return query;
        }

        internal static string CheckAlready()
        {
            string query = "select email,sitekey from accessControl";

            return query;
        }
    }
}