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
    public partial class NewUser : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e) 
        {
                
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["roleid"].ToString() == "2")
            {
                lit.Text = "U bent niet gerechtigd een nieuwe gebruiker toe te voegen. Klik <a href=\"Default.aspx\">hier</a> om terug te keren naar de default pagina.";
                tableNewUser.Visible = false;
            }
        }

        protected void btnSendData_Click(object sender, EventArgs e)
        {
            //cars2cloud.appspot.com/cardata/CreateUser?firstname=thomas&lastname=hendriksen&roleid=2
            string url = "http://cars2cloud.appspot.com/cardata/CreateUser?firstname=" + tbFirstName.Text + "&lastname=" + tbLastName.Text + "&roleid=" + ddlRoleID.SelectedValue+"&companyid="+Session["companyid"]+"&emailadress="+tbEmail.Text; ;

            Uri serviceUri = new Uri(url, UriKind.Absolute);

            // Return the HttpWebRequest.
            HttpWebRequest webRequest = (HttpWebRequest)System.Net.WebRequest.Create(serviceUri);

            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            string jsonResponse = string.Empty;
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                jsonResponse = sr.ReadToEnd();
            }
        }
    }
}