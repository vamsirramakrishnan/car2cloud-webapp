﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ictlab
{
    public partial class Uitloggen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Toegang"] = "";
            Response.Redirect("Inloggen.aspx");
        }
    }
}