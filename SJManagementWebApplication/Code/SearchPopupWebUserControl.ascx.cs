using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SJManagementWebApplication.Code
{
    public partial class SearchPopupWebUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void ShowPopup(bool check)
        {
            toolbarSearchPopup.ShowOnPageLoad = check;
        }
    }
}