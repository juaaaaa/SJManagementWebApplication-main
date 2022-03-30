using System;
using System.Web.UI;
using SJManagementWebApplication.Model;
using DevExpress.Web;
using SJManagementWebApplication.Code;
using SJManagementWebApplication.Db;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace SJManagementWebApplication
{
    public partial class Root : MasterPage
    {
        StringBuilder sbQuery;
        SqlCommand scCmd;

        public bool EnableBackButton { get; set; }
        public static string sMainMenuTxt = null;
        public static bool bMenuChecked = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Page.Header.Title))
                Page.Header.Title += " - ";
            //�۾� ���� 0 : ���α׷� �̸� ����
            Page.Header.Title = Page.Header.Title + "Sjtech �ü� ���� ���α׷�";

            Page.Header.DataBind();
            UpdateUserInfo();

            if (!IsPostBack)
            {
                //�޴� �ʱ�ȭ
                DataTable table = GetDataTable_master(Init_detail_menu(sMainMenuTxt)); //��ü detail �޴� �� ���
                CreateMenuItem(sMainMenuTxt); //master �޴� create
                CreateTreeViewNodesRecursive(table, this.treeView.Nodes, "0"); //detail �޴� create
            }
        }
        protected void RightAreaMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "SignOutItem")
            {
                AuthHelper.SignOut();
                Response.Redirect("~/Page/user/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
        protected void UpdateUserInfo()
        {
            if (AuthHelper.IsAuthenticated())
            {
                var user = AuthHelper.GetLoggedInUserInfo();
                var myAccountItem = RightAreaMenu.Items.FindByName("UserInfoItem");
                var userName = (ASPxLabel)myAccountItem.FindControl("UserNameLabel");

                if (string.IsNullOrEmpty(user.UserId))
                {
                    //Print.debugWrite("userId is null");
                }
                else
                {
                    userName.Text = user.UserId + "�� �ȳ��ϼ���.";
                }
            }
        }
        protected void CreateMenuItem(string selectedMenu)//master menu ����
        {
            String[,] menuList = null;
            try
            {
                if (Session["masterMenu"] != null) //���� ���� ���� Ȯ��
                {
                    menuList = (String[,])Session["masterMenu"];
                }
                else
                {
                    //�۾� ���� 1: �޴� ����
                    //�޴� �ʱ�ȭ �۾�
                    sbQuery = new StringBuilder();
                    sbQuery.Append("select mMenu_name from SY001_MasterMenu ");
                    sbQuery.Append("where mMenu_useChk = 'y' ");
                    scCmd = new SqlCommand(sbQuery.ToString());
                    menuList = DbTask.data_Load_Table(scCmd);

                    Session["masterMenu"] = menuList;
                }
                for (int i = 0; i < menuList.Length; i++)
                {
                    String menuName = menuList[i, 0];
                    Print.debugWrite("�޴�", selectedMenu);

                    DevExpress.Web.MenuItem menu = new DevExpress.Web.MenuItem();


                    if (menuName.Equals(selectedMenu))
                    {
                        menu.ItemStyle.BackColor = (Color)new ColorConverter().ConvertFromString(NumDefine.DEEPBLUE);
                    }
                    //���õ� �޴� �� ����
                    if (i == 0 && string.IsNullOrEmpty(selectedMenu))
                    {
                        menu.ItemStyle.BackColor = (Color)new ColorConverter().ConvertFromString(NumDefine.DEEPBLUE);
                    }
                    //�޴� �ؽ�Ʈ ����
                    menu.Text = menuName;
                    mMenu.Items.Add(menu);

                }
            }
            catch (Exception e)
            {

            }

        }

        private void CreateTreeViewNodesRecursive(DataTable table, TreeViewNodeCollection nodesCollection, string parentID)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["ParentID"].ToString() == parentID)
                {
                    String url = table.Rows[i]["NavigatorUrl"].ToString();
                    TreeViewNode node = new TreeViewNode(table.Rows[i]["Title"].ToString(), table.Rows[i]["ID"].ToString());

                    if (url != "")
                    {
                        node.NavigateUrl = "~/" + url + ".aspx";
                    }
                    if (table.Rows[i]["ParentID"].ToString() == "0")
                    {
                        node.NodeStyle.CssClass = "nodet";
                    }
                    node.Expanded = true;

                    nodesCollection.Add(node);
                    CreateTreeViewNodesRecursive(table, node.Nodes, node.Name);
                }
            }
        }
        private DataTable GetDataTable_master(string mMenu_name)
        {
            DataTable dt = null;
            String[,] entire_menuList = getDetailMenu();

            try
            {
                dt = new DataTable();
                dt.Columns.Add("ID", typeof(System.Int32));
                dt.Columns.Add("Title", typeof(System.String));
                dt.Columns.Add("ParentID", typeof(System.Int32));
                dt.Columns.Add("NavigatorUrl", typeof(System.String));

                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID"] };


                for (int i = 0; i < entire_menuList.Length / dt.Columns.Count + 2; i++)
                {
                    if (entire_menuList[i, 4].Equals(mMenu_name))
                    {
                        Print.debugWrite("entire Data", entire_menuList[i, 0], entire_menuList[i, 1], entire_menuList[i, 2]);
                        dt.Rows.Add(entire_menuList[i, 0], entire_menuList[i, 1], entire_menuList[i, 2], entire_menuList[i, 3]);
                    }
                }
                return dt;
            }
            catch (Exception e)
            {
                return dt;
            }
        }
        protected string Init_detail_menu(string selectedMenu) //�ʱ� ������ �޴� ����
        {
            if (!string.IsNullOrEmpty(selectedMenu))
            {
                return selectedMenu;
            }
            else
            {
                try
                {
                    String[,] entire_menuList = getDetailMenu();

                    for (int i = 0; i < entire_menuList.Length / 6; i++)
                    {
                        if (entire_menuList[i, 0].Equals("1"))
                        {
                            return entire_menuList[i, 4];
                        }
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            return null;
        }
        protected String[,] getDetailMenu() //������ �޴� �ҷ����� (���� Ȥ�� DB)
        {
            String[,] entire_menuList = null;
            try
            {
                if (Session["DetailMenu"] != null)
                {
                    entire_menuList = (String[,])Session["DetailMenu"];
                }
                else
                {
                    sbQuery = new StringBuilder();

                    //�۾� ���� 2: ������ ����(���̺��)
                    sbQuery.Append("select d.sdMenu_seq, d.dMenu_name, d.dMenu_seq, d.dMenu_url, m.mMenu_name from SY002_DetailMenu d ");
                    sbQuery.Append("left join SY001_MasterMenu m on d.mMenu_seq = m.mMenu_seq ");
                    sbQuery.Append("where m.mMenu_useChk = 'y' ");
                    sbQuery.Append("AND d.dMenu_useChk = 'y' ");

                    scCmd = new SqlCommand(sbQuery.ToString());

                    entire_menuList = DbTask.data_Load_Table(scCmd);

                    Session["DetailMenu"] = entire_menuList;
                }
                return entire_menuList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        protected void mMenu_ItemClick(object source, MenuItemEventArgs e)
        {
            sMainMenuTxt = e.Item.Text;

            //selected ���� ������ ����
            for (int i = 0; i < mMenu.Items.GetVisibleItemCount(); i++)
            {
                mMenu.Items[i].ItemStyle.BackColor = (Color)new ColorConverter().ConvertFromString(NumDefine.MAINMENU_BLUE);
            }

            //selected �� ����
            mMenu.Items.FindByText(sMainMenuTxt).ItemStyle.BackColor = (Color)new ColorConverter().ConvertFromString(NumDefine.DEEPBLUE);

            //detail �޴� ������ - ok
            this.treeView.Nodes.Clear();

            DataTable table = GetDataTable_master(sMainMenuTxt);
            CreateTreeViewNodesRecursive(table, this.treeView.Nodes, "0");//detail �޴� create
        }

        protected void menuBtn_Click(object sender, EventArgs e)
        {
            bMenuChecked = !bMenuChecked;
            Print.debugWrite("bMenuChecked2", bMenuChecked.ToString());
            LeftPanel.Visible = bMenuChecked;

            if (bMenuChecked && menuBtn.ImageUrl.ToString().Equals(NumDefine.HAMBURGER_CLEAR))
            {
                menuBtn.ImageUrl = NumDefine.HAMBURGER_CLICK;
                menuBtn.BackColor = (Color)new ColorConverter().ConvertFromString(NumDefine.DEEPBLUE);
            }
            else
            {
                menuBtn.ImageUrl = NumDefine.HAMBURGER_CLEAR;
                menuBtn.BackColor = (Color)new ColorConverter().ConvertFromString(NumDefine.MAINMENU_BLUE);
            }
        }
    }
}