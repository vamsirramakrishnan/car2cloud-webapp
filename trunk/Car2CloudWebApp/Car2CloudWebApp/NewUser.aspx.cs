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
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSendData_Click(object sender, EventArgs e)
        {
            //cars2cloud.appspot.com/cardata/CreateUser?firstname=thomas&lastname=hendriksen&roleid=2
            string url = "http://cars2cloud.appspot.com/cardata/CreateUser?firstname=" + tbFirstName.Text + "&lastname=" + tbLastName.Text + "&roleid=" + ddlRoleID.SelectedValue+"&companyid=1&emailadress="+tbEmail.Text; ;
            //string url = "http://localhost:8888/cardata/CreateUser=" + tbFirstName.Text + "" + tbLastName.Text + ","+ddlRoleID.SelectedValue+",";//nog companyID toevoegen


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