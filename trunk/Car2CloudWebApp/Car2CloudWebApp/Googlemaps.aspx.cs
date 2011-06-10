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
using System.Collections;

namespace ictlab
{
    public partial class Googlemaps : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string formattedUri = "http://3.cars2cloud.appspot.com/cardata/GetAll";
            HttpWebRequest webRequest = GetWebRequest(formattedUri);
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            string jsonResponse = string.Empty;
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                jsonResponse = sr.ReadToEnd();
            }

            // Met behulp van de datatable 2 gebruiken om het javascript te maken
            DataTable data = GetDataTableFromJson(jsonResponse);
            String[] Latitude = GetLatitudes(data, 1, 1);
            String[] Longitude = GetLongitudes(data, 1, 1);
            js.Text = BuildScript(Latitude, Longitude);
        }

        private static String BuildScript(String[] Latitude, String[] Longitude)
        {
            try
            {
                String Locations = "";
                String jScript = "";
                for (int i = 0; i < Latitude.Length; )
                {
                    // JavaScript maken voor de overlay             
                    Locations += Environment.NewLine + @"
                    path.push(new google.maps.LatLng(" + Latitude[i] + ", " + Longitude[i] + @"));

                    var marker" + i.ToString() + @" = new google.maps.Marker({
                        position: new google.maps.LatLng(" + Latitude[i] + ", " + Longitude[i] + @"),
                        title: '#' + path.getLength(),
                        map: map
                    });";
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
            foreach (DataRow r in dataSet.Rows)
            {

                string temp = r.ItemArray.ElementAt(1).ToString();
                int tempUserId = Convert.ToInt32(temp);
                string temp2 = r.ItemArray.ElementAt(2).ToString();
                int tempTripId = Convert.ToInt32(temp2);
                if (userId == tempUserId && tempTripId == tripId)
                {
                    count++;
                }
            }

            string[] latitudes = new string[count];

            count = 0;
            foreach (DataRow r in dataSet.Rows)
            {

                string temp = r.ItemArray.ElementAt(1).ToString();
                int tempUserId = Convert.ToInt32(temp);
                string temp2 = r.ItemArray.ElementAt(2).ToString();
                int tempTripId = Convert.ToInt32(temp2);
                if (userId == tempUserId && tempTripId == tripId)
                {

                    latitudes[count] = r.ItemArray.ElementAt(3).ToString();
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
            foreach (DataRow r in dataSet.Rows)
            {

                string temp = r.ItemArray.ElementAt(1).ToString();
                int tempUserId = Convert.ToInt32(temp);
                string temp2 = r.ItemArray.ElementAt(2).ToString();
                int tempTripId = Convert.ToInt32(temp2);
                if (userId == tempUserId && tempTripId == tripId)
                {
                    count++;
                }
            }

            string[] longitudes = new string[count];

            count = 0;
            foreach (DataRow r in dataSet.Rows)
            {

                string temp = r.ItemArray.ElementAt(1).ToString();
                int tempUserId = Convert.ToInt32(temp);
                string temp2 = r.ItemArray.ElementAt(2).ToString();
                int tempTripId = Convert.ToInt32(temp2);
                if (userId == tempUserId && tempTripId == tripId)
                {

                    longitudes[count] = r.ItemArray.ElementAt(4).ToString();
                    count++;
                }
            }

            return longitudes;
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

    }
}