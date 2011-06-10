using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using System.Net;
using System.Globalization;
using System.IO;
using System.Data;

namespace ictlab
{
    public partial class _Default : System.Web.UI.Page
    {
        //private readonly static string FindNearbyWeatherUrl =
        //    "http://cars2cloud.appspot.com/cardata/123";

        protected override void OnInit(EventArgs e) 
        {
            Grd1.AllowPaging = true;
            Grd1.PagerStyle.AlwaysVisible = true;
            string formattedUri = "http://3.cars2cloud.appspot.com/cardata/GetAll";
            HttpWebRequest webRequest = GetWebRequest(formattedUri);

            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            string jsonResponse = string.Empty;
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                jsonResponse = sr.ReadToEnd();
            }

            DataTable dtJsonResponse = GetDataTableFromJson(jsonResponse);
            if (Page.IsPostBack != true)
            {
                Session["Data"] = dtJsonResponse;
            }
            Grd1.DataSource = Session["Data"];
            Grd1.DataBind();    
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Grd1.Rebind();
        }       

        private static HttpWebRequest GetWebRequest(string formattedUri)
        {
            // Create the request’s URI.
            Uri serviceUri = new Uri(formattedUri, UriKind.Absolute);

            // Return the HttpWebRequest.
            return (HttpWebRequest)System.Net.WebRequest.Create(serviceUri);
        }

        private DataTable GetDataTableFromJson(String Json)
        {
            DataTable cardata = new DataTable();
            String cleanjson = Json.Replace("[","").Replace("]","").Replace("{","");
            String[] Rows = cleanjson.Split('}');
            String[] tempRows = new String[Rows.Length];
            int rowLenght = Rows.Length;

            if (Rows.Length > 0)
            {
                for (int i = 0; i < Rows.Length; i++)
                {
                    if (Rows[i].StartsWith(","))
                    {
                         tempRows[i] = Rows[i].TrimStart(',');
                    }
                    else
                    {
                        if (Rows[i].Equals(""))
                        {
                            rowLenght--;
                        }
                        tempRows[i] = Rows[i];
                    }
                }
                Rows = new String[rowLenght];

                for (int i = 0; i < rowLenght; i++)
                {
                    Rows[i] = tempRows[i];
                }
            }

            //BUILD DATATABLE
            
                int ColumnCount = Rows[0].Split(',').Length;
                
                foreach (String value in Rows[0].Split(','))
                {
                    cardata.Columns.Add(value.Split(':')[0].Replace("\"",""));
                }

            //Insert Rows Datatable

                for(int rowIndex = 0; rowIndex < Rows.Length; rowIndex++)
                {
                    DataRow dr = cardata.NewRow();
                    int Count = 0;
                    foreach (String value in Rows[rowIndex].Split(','))
                    {
                        dr[Count] = (value.Split(':')[1].Replace("\"", ""));
                        Count++;
                    }
                    cardata.Rows.Add(dr);
                }
            
            return cardata ;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            Page.ClientScript.RegisterForEventValidation(Grd1.UniqueID);
            base.Render(writer);
        }
    }
}
