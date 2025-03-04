﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ictlab
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            Session["userid"] = "";
            Session["roleid"] = "";
            Session["companyid"] = "";
            Session["entityid"] = "";
            CheckLogin();
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

        void Application_OnPostRequestHandlerExecute()
        {
            if (Context.Handler is IRequiresSessionState || Context.Handler is IReadOnlySessionState)
            {
                CheckLogin();
            }
        }

        void CheckLogin()
        {

            string Url = Request.RawUrl;
            string Urlcontrole = Url.Substring(1);
            string SessieData = Session["userid"].ToString();

            int index = Url.IndexOf("Inloggen.aspx");

            if (SessieData == "" && Urlcontrole != "Inloggen.aspx")
            {
                Response.Redirect("Inloggen.aspx");
            }
        }
    }
}
