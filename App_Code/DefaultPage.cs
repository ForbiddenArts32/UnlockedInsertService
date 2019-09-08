using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UnlockedInsertService
{
    public partial class DefaultPage : System.Web.UI.Page
    {
        protected ulong? _LoggedInUserId = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            _LoggedInUserId = AssetBot.CheckRbxUserId();
        }
    }
}