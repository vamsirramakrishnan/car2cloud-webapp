using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;

namespace ictlab
{
    public partial class Inloggen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String url = "http://cars2cloud.appspot.com/cardata/AuthUser?Username=" + Gebruikersnaam.Text + "&Password=" + Wachtwoord.Text + "";
            //String url = "http://localhost:8888/cardata/AuthUser?Username=" + Gebruikersnaam.Text + "&Password=" + Wachtwoord.Text + "";

            Uri serviceUri = new Uri(url, UriKind.Absolute);

            // Return the HttpWebRequest.
            HttpWebRequest webRequest = (HttpWebRequest)System.Net.WebRequest.Create(serviceUri);

            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            string jsonResponse = string.Empty;
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                jsonResponse = sr.ReadToEnd();
            }

            if (jsonResponse != "")
            {
                Session["Toegang"] = jsonResponse;//Response is USER ID
                Response.Redirect("Default.aspx");
            }
            else
            {
                Melding.Text = "Inloggen mislukt. Probeert u alstublieft opnieuw.";
            }
        }
    }
}