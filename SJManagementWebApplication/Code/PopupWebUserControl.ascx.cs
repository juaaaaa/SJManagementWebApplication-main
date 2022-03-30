using System;
using System.Collections.Generic;
using System.Web;

namespace SJManagementWebApplication.Code
{
    public partial class PopupWebUserControl_2 : System.Web.UI.UserControl
    {
        public event EventHandler OkBtnClick; //버튼 리스너 클릭
        public event EventHandler CancelBtnEvent;

        //event remove 용
        List<EventHandler> delegates = new List<EventHandler>();

        protected void Page_Load(object sender, EventArgs e)

        {

        }
        public void setBtnText(string okBtn, string cancelBtn)
        {
            btnOk.Text = okBtn;
            btnCancel.Text = cancelBtn;
        }
        public void showPopup(bool okBtnState, bool CancelBtnState, string lbText)
        {
            lbPopupText(lbText);
            OkBtnVisible(okBtnState);
            CancelBtnVisible(CancelBtnState);
            pcPopup.ShowOnPageLoad = true;
        }
        public void lbPopupText(string text)
        {
            //pcPopup.HeaderText = text;
            lbPopup.Value = text;

        }
        public void OkBtnVisible(bool isvisible)
        {
            btnOk.Visible = isvisible;
        }
        public void CancelBtnVisible(bool isvisible)
        {
            btnCancel.Visible = isvisible;
        }
        public void add_OkClickEvent(object sender, EventArgs e)
        {
            try
            {
                OkBtnClick(sender, e);
                delegates.Add(OkBtnClick);
                clearTag();
            }
            catch (Exception ed)
            {

            }
        }
        public void add_CancelClickEvent(object sender, EventArgs e)
        {
            try
            {
                CancelBtnEvent(sender, e);
                delegates.Add(CancelBtnEvent);
                clearTag();
            }
            catch (Exception ee)
            {

            }
        }
        public void clearEvent(byte mTag)
        {
            HttpContext.Current.Session["Tag"] = mTag;

            Print.debugWrite("clearEvent", mTag.ToString());
            foreach (EventHandler eh in delegates)
            {
                OkBtnClick -= eh;
                CancelBtnEvent -= eh;
            }
            delegates.Clear();
        }
        public byte getTag()
        {
            byte Tag = 0;
            try
            {
                if (HttpContext.Current.Session["Tag"] != null)
                {
                    Tag = (byte)HttpContext.Current.Session["Tag"];
                }
            }
            catch
            {
            }
            return Tag;
        }
        public void clearTag()
        {
            HttpContext.Current.Session["Tag"] = 0;
        }
    }
}