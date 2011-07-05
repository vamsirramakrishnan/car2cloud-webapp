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
using WebChart;
using System.Drawing;
using ictlab.Classes;

namespace ictlab
{
    public partial class Default : System.Web.UI.Page
    {
        int userId;
        DataTable dataSet;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["roleid"].ToString() == "1")
            {
                if (Request.QueryString["entityid"] == null )
                {
                    PnlManager.Visible = true;
                    /* Manager, na het inloggen */

                    /* Medewerkers weergeven */
                    string uri = "http://cars2cloud.appspot.com/cardata/GetUserByCompany?companyid=" + Session["companyid"];

                    HttpWebRequest webRequest = GetWebRequest(uri);

                    HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                    string jsonResponse = string.Empty;
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        jsonResponse = sr.ReadToEnd();
                    }

                    DataTable dtCompanyUsers = GetDatableAllCompanysUser(jsonResponse);
                    if (!IsPostBack)
                    {
                        fillListBoxWithEmployers(dtCompanyUsers);
                    }
                }
                else
                {
                    string entity = Request.QueryString["entityid"];
                    PnlTrips.Visible = true;
                    /* Medewerker, na het inloggen */

                    /* dataSet ophalen uit de app engine (nog veranderen naar data van 1 user) */
                    string formattedUri = "http://cars2cloud.appspot.com/cardata/GetDataByEntity?entityid=" + entity;
                    //string formattedUri = "http://localhost:8888/cardata/GetAll";
                    HttpWebRequest webRequest = GetWebRequest(formattedUri);

                    HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                    string jsonResponse = string.Empty;
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        jsonResponse = sr.ReadToEnd();
                    }

                    /* dataSet ophalen */
                    dataSet = GetDataTableFromJson(jsonResponse);

                    if (!IsPostBack)
                    {
                        /*ListBox vullen */
                        fillListBoxWithTrips(dataSet);
                    }
                }

            }
            else if (Session["roleid"].ToString() == "2") // Medewerker
            {
               

                PnlTrips.Visible = true;
                /* Medewerker, na het inloggen */

                /* dataSet ophalen uit de app engine (nog veranderen naar data van 1 user) */
                string formattedUri = "http://cars2cloud.appspot.com/cardata/GetDataByEntity?entityid=" + Session["entityid"];
                //string formattedUri = "http://localhost:8888/cardata/GetAll";
                HttpWebRequest webRequest = GetWebRequest(formattedUri);

                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                string jsonResponse = string.Empty;
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    jsonResponse = sr.ReadToEnd();
                }

                /* dataSet ophalen */
                dataSet = GetDataTableFromJson(jsonResponse);

                /* Ingelogd met een userId */
                userId = Convert.ToInt32(Session["userid"]);
                if (!IsPostBack)
                {
                    /*ListBox vullen */
                    fillListBoxWithTrips(dataSet);
                }
            }
        }

        private static HttpWebRequest GetWebRequest(string formattedUri)
        {
            // Create the request’s URI.
            Uri serviceUri = new Uri(formattedUri, UriKind.Absolute);

            // Return the HttpWebRequest.
            return (HttpWebRequest)System.Net.WebRequest.Create(serviceUri);
        }

        private DataTable GetDatableAllCompanysUser(string Json)
        {
            DataTable dtCompanyUsers = new DataTable();
            String cleanjson = Json.Replace("[", "").Replace("]", "").Replace("\",\"", ":").Replace("\"", "");
            String[] Rows = cleanjson.Split(':');
            String[] tempRows = new String[Rows.Length];
            int rowLength = Rows.Length;

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
                            rowLength--;
                        }
                        tempRows[i] = Rows[i];
                    }
                }
                Rows = new String[rowLength];

                for (int i = 0; i < rowLength; i++)
                {
                    Rows[i] = tempRows[i];
                }
            }

            //add datacolumns

            DataColumn dc = new DataColumn("UserID");
            dtCompanyUsers.Columns.Add(dc);
            dc = new DataColumn("EntityID");
            dtCompanyUsers.Columns.Add(dc);
            dc = new DataColumn("Firstname");
            dtCompanyUsers.Columns.Add(dc);
            dc = new DataColumn("Lastname");
            dtCompanyUsers.Columns.Add(dc);

            //Insert Rows Datatable

            for (int rowIndex = 0; rowIndex < Rows.Length; rowIndex++)
            {
                DataRow dr = dtCompanyUsers.NewRow();
                int Count = 0;
                foreach (String value in Rows[rowIndex].Split(','))
                {
                    dr[Count] = value;
                    Count++;
                }
                dtCompanyUsers.Rows.Add(dr);
            }

            return dtCompanyUsers;
        }

        private DataTable GetDataTableFromJson(String Json)
        {
            DataTable cardata = new DataTable();
            String cleanjson = Json.Replace("[", "").Replace("]", "").Replace("{", "");
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
                cardata.Columns.Add(value.Split(':')[0].Replace("\"", ""));
            }

            //Insert Rows Datatable

            for (int rowIndex = 0; rowIndex < Rows.Length; rowIndex++)
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

            return cardata;
        }

        /* 
         * ListBox vullen met alle trips van de gebruiker
         */
        private void fillListBoxWithTrips(DataTable dataSet)
        {

            List<string> listBoxItems = new List<string>();

            foreach (DataRow r in dataSet.Rows)
            {
                string tempTripId = r.ItemArray.ElementAt((int)DatasetHelper.CarDataColumn.TripId).ToString();
                if (!listBoxItems.Contains(tempTripId))
                {

                    listBoxItems.Add(tempTripId);
                }
            }

            ListBox1.DataSource = listBoxItems;
            ListBox1.DataBind();
        }

        /* 
         * ListBox vullen met alle medewerkers van de manager
         */
        private void fillListBoxWithEmployers(DataTable dataSet)
        {

            List<string> listBoxItems = new List<string>();
            DataTable Users = new DataTable();
            Users.Columns.Add("Displayname");
            Users.Columns.Add("Value");
            foreach (DataRow r in dataSet.Rows)
            {
                string tempId = r.ItemArray.ElementAt(0).ToString();
                string EntityID = r.ItemArray.ElementAt(1).ToString();
                string tempName = r.ItemArray.ElementAt(2).ToString();
                string tempRearName = r.ItemArray.ElementAt(3).ToString();
                DataRow userRow = Users.NewRow();
                userRow[0] = tempId + " | " + tempName + " " + tempRearName;
                userRow[1] = EntityID;
                Users.Rows.Add(userRow);
            }

            ListBox2.DataSource = Users;
            ListBox2.DataValueField = "Value";
            ListBox2.DataTextField = "Displayname";
            ListBox2.DataBind();
        }

        /**
         * Berekent de gemiddelde snelheid.
         * tripId op 0 geeft gemiddelde snelheid van alle trips.
         * @author: Richard
         */
        private int GetAverageSpeed(DataTable dataSet, int userId, int tripId = -1)
        {
            int averageSpeed = 0;
            if (tripId != -1)
            {

                /* Van 1 trip wordt er de gemiddelde snelheid berekent */
                int count = 0;
                int averageTrip = 0;
                foreach (DataRow r in dataSet.Rows)
                {
                    if (Convert.ToInt32(r.ItemArray.ElementAt((int)DatasetHelper.CarDataColumn.TripId).ToString()) == tripId)
                    {

                        averageTrip = averageTrip + Convert.ToInt32(r.ItemArray.ElementAt((int)DatasetHelper.CarDataColumn.Speed).ToString());
                        count++;
                    }
                }

                if (count == 0)
                {

                    return 0;
                }
                else
                {

                    averageSpeed = (averageTrip / count);
                }
            }
            else
            {
                /* Van alle trips wordt de gemiddelde snelheid gemeten */


                int count = 0;
                int averageTrip = 0;
                foreach (DataRow r in dataSet.Rows)
                {

                    //string temp = r.ItemArray.ElementAt(1).ToString();
                    //int tempUserId = Convert.ToInt32(temp);
                    //if (userId == tempUserId)
                    //{

                    averageTrip = averageTrip + Convert.ToInt32(r.ItemArray.ElementAt((int)DatasetHelper.CarDataColumn.Speed).ToString());
                    count++;
                    //}
                }

                if (count == 0)
                {

                    return 0;
                }
                else
                {

                    averageSpeed = (averageTrip / count);
                }
            }

            return averageSpeed;
        }

        /*
         * De kleuren van de ChartControl1 configureren
         * @author: Richard
         */
        private void ConfigureColors()
        {
            ChartControl1.Background.Color = Color.FromArgb(75, Color.White);
            ChartControl1.Legend.Position = LegendPosition.Bottom;
            ChartControl1.Legend.Width = 40;

            ChartControl1.YAxisFont.ForeColor = Color.SteelBlue;
            ChartControl1.XAxisFont.ForeColor = Color.SteelBlue;

            ChartControl1.ChartTitle.Text = "";
            ChartControl1.ChartTitle.ForeColor = Color.Black;
        }

        /*
         * Een nieuwe lijn aanmaken voor een LineChart.
         * @author: Richard
         */
        private LineChart newLine(DataTable dataSet, string lineName, Color lineColor, float lineWidth, int userId, int tripId)
        {

            LineChart chart = new LineChart();
            chart.Legend = lineName;
            chart.Fill.Color = Color.FromArgb(50, lineColor);
            chart.Line.Color = lineColor;
            chart.Line.Width = lineWidth;

            int count = 0;
            foreach (DataRow r in dataSet.Rows)
            {

                if (Convert.ToInt32(r.ItemArray.ElementAt((int)DatasetHelper.CarDataColumn.TripId).ToString()) == tripId)
                {
                    string number = count.ToString();
                    chart.Data.Add(new ChartPoint("", Convert.ToInt64(r.ItemArray.ElementAt(6).ToString())));
                    count++;
                }
            }

            if (count == 0)
            {

                return null;
            }

            return chart;
        }

        /*
         * GoogleMaps implementatie
         * @author: Joel
         */
        private static String BuildScript(String[] Latitude, String[] Longitude)
        {
            try
            {
                String Locations = "";
                String jScript = "";
                for (int i = 0; i < Latitude.Length; )
                {
                    // JavaScript maken voor de overlay             
                    Locations += Environment.NewLine + @"path.push(new google.maps.LatLng(" + Latitude[i] + ", " + Longitude[i] + @"));";

                    //var marker" + i.ToString() + @" = new google.maps.Marker({
                    //     position: new google.maps.LatLng(" + Latitude[i] + ", " + Longitude[i] + @"),
                    //    title: '#' + path.getLength(),
                    //   map: map
                    // });";
                    i++;
                }

                // Het complete JavaScript maken
                jScript = @"<script type='text/javascript'>

                                var poly;
                                var map;

                                function initialize() {
                                    var latlng = new google.maps.LatLng(51.844552160000006, 4.630125760999988);
                                    var myOptions = {
                                        zoom: 12,
                                        center: latlng,
                                        mapTypeId: google.maps.MapTypeId.ROADMAP
                                    };

                                    map = new google.maps.Map(document.getElementById('map_canvas'), myOptions);

                                    var polyOptions = {
                                        strokeColor: 'blue',
                                        strokeOpacity: 0.5,
                                        strokeWeight: 3
                                    }
                                    poly = new google.maps.Polyline(polyOptions);
                                    poly.setMap(map);

                                    var path = poly.getPath();

                                    " + Locations + @"
                                }
                    </script>";
                return jScript;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        /* 
         * Latitudes uit de dataSet halen en in array terug geven.
         * @author: Richard
         */
        private string[] GetLatitudes(DataTable dataSet, int userId, int tripId)
        {

            int count = 0;
            string[] latitudes = new string[dataSet.Select("tripId = " + tripId).Count()];
            foreach (DataRow r in dataSet.Rows)
            {
                if (Convert.ToInt32(r.ItemArray.ElementAt((int)DatasetHelper.CarDataColumn.TripId).ToString()) == tripId)
                {
                    latitudes[count] = r.ItemArray.ElementAt((int)DatasetHelper.CarDataColumn.Latitude).ToString();
                    count++;
                }
            }

            return latitudes;
        }

        /* 
         * Longitudes uit de dataSet halen en in array terug geven.
         * @author: Richard
         */
        private string[] GetLongitudes(DataTable dataSet, int userId, int tripId)
        {

            int count = 0;
            string[] longitudes = new string[dataSet.Select("tripId = " + tripId).Count()];
            foreach (DataRow r in dataSet.Rows)
            {
                if (Convert.ToInt32(r.ItemArray.ElementAt((int)DatasetHelper.CarDataColumn.TripId).ToString()) == tripId)
                {
                    longitudes[count] = r.ItemArray.ElementAt((int)DatasetHelper.CarDataColumn.Longtitude).ToString();
                    count++;
                }
            }

            return longitudes;
        }

   
        protected void Button2_Click(object sender, EventArgs e)
        {

            string select = ListBox2.SelectedValue;
            Response.Redirect("./default.aspx?entityid=" + select);


            ///* dataSet ophalen uit de app engine (nog veranderen naar data van 1 user) */
            //string formattedUri = "http://cars2cloud.appspot.com/cardata/GetDataByEntity?entityid=" + select;
            ////string formattedUri = "http://localhost:8888/cardata/GetAll";
            //HttpWebRequest webRequest = GetWebRequest(formattedUri);

            //HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            //string jsonResponse = string.Empty;
            //using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            //{
            //    jsonResponse = sr.ReadToEnd();
            //}

            ///* dataSet ophalen */
            //dataSet = GetDataTableFromJson(jsonResponse);

            ///* Ingelogd met een userId */
            //userId = id;
            //int tripId = 1; // default

            ///*ListBox vullen */
            //fillListBoxWithTrips(dataSet, userId);

            ///* Gemiddelde snelheid */
            //int averageSpeed = GetAverageSpeed(dataSet, userId, tripId);
            //Label5.Text = averageSpeed + " km/uur";
            //int averageSpeedAll = GetAverageSpeed(dataSet, userId);
            //Label10.Text = averageSpeedAll + " km/uur";

            ///* linechart */
            //LineChart one = newLine(dataSet, "Trip nummer 1", ColorTranslator.FromHtml("#f67027"), 2, userId, tripId);
            //ConfigureColors();
            //one.ShowLineMarkers = false;
            //ChartControl1.Charts.Add(one);
            //ChartControl1.RedrawChart();

            ///* GoogleMaps */
            //String[] Latitude = GetLatitudes(dataSet, userId, tripId);
            //String[] Longitude = GetLongitudes(dataSet, userId, tripId);
            //js.Text = BuildScript(Latitude, Longitude);
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PnlTripData.Visible = true;
            int tripId = Convert.ToInt32(ListBox1.SelectedItem.Text);


            /* Gemiddelde snelheid */
            int averageSpeed = GetAverageSpeed(dataSet, userId, tripId);
            Label5.Text = averageSpeed + " km/uur";
            int averageSpeedAll = GetAverageSpeed(dataSet, userId);
            Label10.Text = averageSpeedAll + " km/uur";

            /* linechart */
            LineChart one = newLine(dataSet, "Trip nummer " + tripId, ColorTranslator.FromHtml("#f67027"), 2, userId, tripId);
            ConfigureColors();
            one.ShowLineMarkers = false;
            ChartControl1.Charts.Add(one);
            ChartControl1.RedrawChart();

            /* GoogleMaps */
            String[] Latitude = GetLatitudes(dataSet, userId, tripId);
            String[] Longitude = GetLongitudes(dataSet, userId, tripId);
            js.Text = BuildScript(Latitude, Longitude);





            //int selectedIndex = Convert.ToInt32(ListBox1.SelectedItem.Text);
            //int tripId = selectedIndex;

            //ChartControl1.Charts.Clear();

            ///* Gemiddelde snelheid */
            //int averageSpeed = GetAverageSpeed(dataSet, userId, tripId);
            //Label5.Text = averageSpeed + " km/uur";
            //int averageSpeedAll = GetAverageSpeed(dataSet, userId, 0);
            //Label10.Text = averageSpeedAll + " km/uur";

            ///* linechart */
            //LineChart one = newLine(dataSet, "Trip nummer 1", ColorTranslator.FromHtml("#f67027"), 2, userId, tripId);
            //ConfigureColors();
            //one.ShowLineMarkers = false;
            //ChartControl1.Charts.Add(one);
            //ChartControl1.RedrawChart();

            ///* GoogleMaps */
            //String[] Latitude = GetLatitudes(dataSet, userId, tripId);
            //String[] Longitude = GetLongitudes(dataSet, userId, tripId);
            //js.Text = BuildScript(Latitude, Longitude);
        }
    }
}