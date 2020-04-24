using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DBSystem.BLL;
using DBSystem.ENTITIES;

namespace WebApp.Pages
{
    public partial class _60ASPControlsMultiRecDropToCustGridViewToSingleRec : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageLabel.Text = "";
            if (!Page.IsPostBack)
            {
                BindTeamList();
            }
        }
        protected void BindTeamList()
        {
            //standard lookup
            try
            {
                TeamController sysmgr = new TeamController();
                List<Team> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.TeamName.CompareTo(y.TeamName));
                List01.DataSource = info;
                List01.DataTextField = nameof(Team.TeamName);
                List01.DataValueField = nameof(Team.TeamID);
                List01.DataBind();
                List01.Items.Insert(0, "select...");
            }
            catch(Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }
        protected void Fetch_Click(object sender, EventArgs e)
        {
            if (List01.SelectedIndex == 0)
            {
                MessageLabel.Text = "Select a category to view its products";
            }
            else
            {
                try
                {
                    PlayerController sysmgr = new PlayerController();
                    List<Player> info = null;
                    info = sysmgr.Player_GetByTeam(int.Parse(List01.SelectedValue));
                    info.Sort((x, y) => x.LastName.CompareTo(y.LastName));
                    PlayerList.DataSource = info;
                    PlayerList.DataBind();

                    PlayerList.DataSource = info;
                    PlayerList.DataBind();
                }
                catch (Exception ex)
                {
                    MessageLabel.Text = ex.Message;
                }
            }
        }
        protected void TeamList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PlayerList.PageIndex = e.NewPageIndex;
            Fetch_Click(sender, new EventArgs());
        }
        protected void List02_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow agvrow = PlayerList.Rows[PlayerList.SelectedIndex];
            string productid = (agvrow.FindControl("ProductID") as Label).Text;
            Response.Redirect("CRUDPage.aspx?pid=" + productid);
        }
    }
}