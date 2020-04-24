using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DBSystem.BLL;
using DBSystem.ENTITIES;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;

namespace WebApp.Pages
{
    public partial class CRUDPage : System.Web.UI.Page
    {

        List<string> errormsgs = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Message.DataSource = null;
            Message.DataBind();
            if (!Page.IsPostBack)
            {
                BindPlayerList();
            }
        }
        protected Exception GetInnerException(Exception ex)
        {
            //drill down to the inner most exception
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }
        protected void LoadMessageDisplay(List<string> errormsglist, string cssclass)
        {
            Message.CssClass = cssclass;
            Message.DataSource = errormsglist;
            Message.DataBind();
        }
        #region Binding of DropDownList
        protected void BindPlayerList()
        {
            //standard lookup
            try
            {
                PlayerController sysmgr = new PlayerController();
                List<Player> info = null;
                info = sysmgr.Player_List();
                info.Sort((x, y) => x.LastName.CompareTo(y.LastName));
                PlayerSearch.DataSource = info;
                PlayerSearch.DataTextField = nameof(Player.PlayerName);
                PlayerSearch.DataValueField = nameof(Player.PlayerID);
                PlayerSearch.DataBind();
                PlayerSearch.Items.Insert(0, "select...");

            }
            catch (Exception ex)
            {
                //using the specialized error handling DataList control
                errormsgs.Add(GetInnerException(ex).ToString());
                LoadMessageDisplay(errormsgs, "alert alert-danger");
            }
        }
        #endregion


        protected void Search_Click(object sender, EventArgs e)
        {
            if (PlayerSearch.SelectedIndex == 0)
            {
                errormsgs.Add("Select a player to maintain");
                LoadMessageDisplay(errormsgs, "alert alert-info");
            }
            else
            {
                try
                {
                    PlayerController sysmgr = new PlayerController();
                    Player info = null;
                    info = sysmgr.Player_Find(int.Parse(PlayerSearch.SelectedValue));
                    if (info == null)
                    {
                        errormsgs.Add("Player no longer on file.");
                        LoadMessageDisplay(errormsgs, "alert alert-info");
                        Clear_Click(sender, e);
                        BindPlayerList();
                    }
                    else
                    {
                        PlayerID.Text = info.PlayerID.ToString();
                        GuardianID.SelectedValue = info.GuardianID.ToString();
                        TeamID.SelectedValue = info.TeamId.ToString();
                        FirstName.Text = info.FirstName;
                        LastName.Text = info.LastName;
                        PlayerAge.Text = info.Age.ToString();
                        MedicalAlerts.Text = info.MedicalAlertDetails;
                        AlbertaHealthCareNumber.Text = info.AlbertaHealthCareNumber;
                        PlayerGender.SelectedValue = info.Gender;
                    }
                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
            }
        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            PlayerID.Text = "";
            FirstName.Text = "";
            LastName.Text = "";
            PlayerAge.Text = "";
            TeamID.SelectedIndex = 0;
            GuardianID.SelectedIndex = 0;
            PlayerGender.ClearSelection();
            AlbertaHealthCareNumber.Text = "";
            MedicalAlerts.Text = "";
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (GuardianID.SelectedIndex == 0)
                {
                    errormsgs.Add("Category is required");
                }
                if (TeamID.SelectedIndex == 0)
                {
                    errormsgs.Add("Category is required");
                }
                if (PlayerGender.SelectedIndex != 0 && PlayerGender.SelectedIndex != 1)
                {
                    errormsgs.Add("A Player Gender is required.");
                }

                //is data still good
                if (errormsgs.Count > 0)
                {
                    LoadMessageDisplay(errormsgs, "alert alert-info");
                }
                else
                {
                    try
                    {
                        PlayerController sysmgr = new PlayerController();
                        Player item = new Player();

                        item.GuardianID = int.Parse(GuardianID.SelectedValue);

                        item.TeamId = int.Parse(TeamID.SelectedValue);

                        item.FirstName = FirstName.Text.Trim();

                        item.LastName = LastName.Text.Trim();

                        item.Age = int.Parse(PlayerAge.Text);

                        item.Gender = PlayerAge.Text.Trim();

                        item.AlbertaHealthCareNumber = AlbertaHealthCareNumber.Text.Trim();

                        item.MedicalAlertDetails =
                            string.IsNullOrEmpty(MedicalAlerts.Text) ? null : MedicalAlerts.Text.Trim();

                        int newPlayerID = sysmgr.Player_Add(item);


                        PlayerID.Text = newPlayerID.ToString();
                        errormsgs.Add("Player has been added");
                        LoadMessageDisplay(errormsgs, "alert alert-success");

                        BindPlayerList();
                        PlayerSearch.SelectedValue = PlayerID.Text;
                    }
                    catch (DbUpdateException ex)
                    {
                        UpdateException updateException = (UpdateException)ex.InnerException;
                        if (updateException.InnerException != null)
                        {
                            errormsgs.Add(updateException.InnerException.Message.ToString());
                        }
                        else
                        {
                            errormsgs.Add(updateException.Message);
                        }
                        LoadMessageDisplay(errormsgs, "alert alert-danger");
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var entityValidationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in entityValidationErrors.ValidationErrors)
                            {
                                errormsgs.Add(validationError.ErrorMessage);
                            }
                        }
                        LoadMessageDisplay(errormsgs, "alert alert-danger");
                    }
                    catch (Exception ex)
                    {
                        errormsgs.Add(GetInnerException(ex).ToString());
                        LoadMessageDisplay(errormsgs, "alert alert-danger");
                    }
                }
            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (GuardianID.SelectedIndex == 0)
                {
                    errormsgs.Add("Category is required");
                }
                if (TeamID.SelectedIndex == 0)
                {
                    errormsgs.Add("Category is required");
                }
                if (PlayerGender.SelectedIndex != 0 && PlayerGender.SelectedIndex != 1)
                {
                    errormsgs.Add("A Player Gender is required.");
                }
                int playerid = 0;
                if (string.IsNullOrEmpty(PlayerID.Text))
                {
                    errormsgs.Add("Search for a player to update");
                }
                else if (!int.TryParse(PlayerID.Text, out playerid))
                {
                    errormsgs.Add("Player id is invalid");
                }
                else if (playerid < 1)
                {
                    errormsgs.Add("Player id is invalid");
                }

                //is data still good
                if (errormsgs.Count > 0)
                {
                    LoadMessageDisplay(errormsgs, "alert alert-info");
                }
                else
                {
                    try
                    {

                        PlayerController sysmgr = new PlayerController();
                        Player item = new Player();
                        item.PlayerID = playerid;
                        item.GuardianID = int.Parse(GuardianID.SelectedValue);

                        item.TeamId = int.Parse(TeamID.SelectedValue);

                        item.FirstName = FirstName.Text.Trim();

                        item.LastName = LastName.Text.Trim();

                        item.Age = int.Parse(PlayerAge.Text);

                        item.Gender = PlayerAge.Text.Trim();

                        item.AlbertaHealthCareNumber = AlbertaHealthCareNumber.Text.Trim();

                        item.MedicalAlertDetails =
                            string.IsNullOrEmpty(MedicalAlerts.Text) ? null : MedicalAlerts.Text.Trim();

                        //issue the BLL call
                        int rowsaffected = sysmgr.Products_Update(item);

                        //give feedback
                        if (rowsaffected > 0)
                        {
                            errormsgs.Add("Product has been updated");
                            LoadMessageDisplay(errormsgs, "alert alert-success");
                            //is there any other controls on the form that
                            //   need to be refreshed??
                            BindPlayerList(); //by default, list will be at index 0
                            PlayerSearch.SelectedValue = PlayerID.Text;
                        }
                        else
                        {
                            errormsgs.Add("Product has not been updated. Product was not found");
                            LoadMessageDisplay(errormsgs, "alert alert-warning");
                            BindPlayerList();

                            //optionally you could clear your fields
                        }

                    }
                    catch (DbUpdateException ex)
                    {
                        UpdateException updateException = (UpdateException)ex.InnerException;
                        if (updateException.InnerException != null)
                        {
                            errormsgs.Add(updateException.InnerException.Message.ToString());
                        }
                        else
                        {
                            errormsgs.Add(updateException.Message);
                        }
                        LoadMessageDisplay(errormsgs, "alert alert-danger");
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var entityValidationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in entityValidationErrors.ValidationErrors)
                            {
                                errormsgs.Add(validationError.ErrorMessage);
                            }
                        }
                        LoadMessageDisplay(errormsgs, "alert alert-danger");
                    }
                    catch (Exception ex)
                    {
                        errormsgs.Add(GetInnerException(ex).ToString());
                        LoadMessageDisplay(errormsgs, "alert alert-danger");
                    }
                }
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            int playerid = 0;
            if (string.IsNullOrEmpty(PlayerID.Text))
            {
                errormsgs.Add("Search for a product to update");
            }
            else if (!int.TryParse(PlayerID.Text, out playerid))
            {
                errormsgs.Add("Product id is invalid");
            }
            else if (playerid < 1)
            {
                errormsgs.Add("Product id is invalid");
            }

            //is data still good
            if (errormsgs.Count > 0)
            {
                LoadMessageDisplay(errormsgs, "alert alert-info");
            }
            else
            {
                try
                {

                    PlayerController sysmgr = new PlayerController();
                    int rowsaffected = sysmgr.Players_Delete(playerid);

                    if (rowsaffected > 0)
                    {
                        errormsgs.Add("Player has been removed");
                        LoadMessageDisplay(errormsgs, "alert alert-success");
                    }
                    else
                    {
                        errormsgs.Add("Player already removed");
                        LoadMessageDisplay(errormsgs, "alert alert-warning");
                    }

                }
                catch (DbUpdateException ex)
                {
                    UpdateException updateException = (UpdateException)ex.InnerException;
                    if (updateException.InnerException != null)
                    {
                        errormsgs.Add(updateException.InnerException.Message.ToString());
                    }
                    else
                    {
                        errormsgs.Add(updateException.Message);
                    }
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            errormsgs.Add(validationError.ErrorMessage);
                        }
                    }
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
            }
        }


    }
}