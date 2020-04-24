<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CRUDPage.aspx.cs" Inherits="WebApp.Pages.CRUDPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Product Maintenance Page</h1>
    
   <asp:RequiredFieldValidator ID="RequiredFirstName" runat="server"
        ErrorMessage="First Name is required." Display="None" SetFocusOnError="true" ForeColor="Firebrick"
         ControlToValidate="FirstName"></asp:RequiredFieldValidator >
    <asp:RequiredFieldValidator ID="RequiredLastName" runat="server"
        ErrorMessage="Last Name is required." Display="None" SetFocusOnError="true" ForeColor="Firebrick"
         ControlToValidate="LastName"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredPlayerAge" runat="server"
        ErrorMessage="Age is required." Display="None" SetFocusOnError="true" ForeColor="Firebrick"
         ControlToValidate="PlayerAge"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangePlayerAge" runat="server"
        ErrorMessage="Age must be between 6 and 14." Display="None" SetFocusOnError="true" ForeColor="Firebrick"
         ControlToValidate="PlayerAge"  MaximumValue="14" MinimumValue="6" Type="Integer"></asp:RangeValidator>
    <asp:RequiredFieldValidator ID="RequiredAlbertaHealthCareNumber" runat="server"
        ErrorMessage="Alberta Health Care Number is required." Display="None" SetFocusOnError="true" ForeColor="Firebrick"
         ControlToValidate="AlbertaHealthCareNumber"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegExAlbertaHealthCareNumber" runat="server"
        ErrorMessage="Invalid Alberta Health Care Number format (11111111111)" Display="None"
         ForeColor="#990000"  SetFocusOnError="true"
         ControlToValidate="AlbertaHealthCareNumber"
         ValidationExpression="[1-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]">
    </asp:RegularExpressionValidator>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
            HeaderText="Address the following concerns about your entered data."/>

     <div class="col-md-12">
         <asp:Label ID="Label1" runat="server" Text="Select a Player"></asp:Label>&nbsp;
         <asp:DropDownList ID="PlayerSearch" runat="server"></asp:DropDownList>&nbsp;
         <asp:LinkButton ID="Search" runat="server" OnClick="Search_Click" CausesValidation="false">Search</asp:LinkButton>&nbsp;&nbsp;
         <asp:LinkButton ID="Clear" runat="server" OnClick="Clear_Click" CausesValidation="false">Clear</asp:LinkButton>&nbsp;&nbsp;
         <asp:LinkButton ID="Add" runat="server" CausesValidation="true" OnClick="Add_Click">Add</asp:LinkButton>&nbsp;&nbsp;
         <asp:LinkButton ID="Update" runat="server" OnClick="Update_Click">Update</asp:LinkButton>&nbsp;&nbsp;
         <asp:LinkButton ID="Delete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this player?')" OnClick="Delete_Click">Delete</asp:LinkButton>

         <br /><br />
             <asp:DataList ID="Message" runat="server">
                <ItemTemplate>
                    <%# Container.DataItem %>
                </ItemTemplate>
             </asp:DataList>
     </div>
>


    <div class="col-md-12">
        <fieldset class="form-horizontal">
            <legend>Player Information</legend>

             <asp:Label ID="Label2" runat="server" Text="Player ID:"
                    AssociatedControlID="PlayerID"></asp:Label>
             <asp:Label ID="PlayerID" runat="server"></asp:Label>
                <br />  
             <asp:Label ID="Label3" runat="server" Text="Guardian"
                     AssociatedControlID="GuardianID"></asp:Label>
            <asp:DropDownList ID="GuardianID" runat="server">
                <asp:ListItem Value ="0">Select...</asp:ListItem>
            </asp:DropDownList>
                <br />  
             <asp:Label ID="Label4" runat="server" Text="Team"
                     AssociatedControlID="TeamID"></asp:Label>
            <asp:DropDownList ID="TeamID" runat="server">
                <asp:ListItem Value ="0">Select...</asp:ListItem>
            </asp:DropDownList>
                <br />  
            <asp:Label ID="Label5" runat="server" Text="First Name"
                     AssociatedControlID="FirstName"></asp:Label>
            <asp:TextBox ID="FirstName" runat="server"></asp:TextBox>
                <br />  
            <asp:Label ID="Label6" runat="server" Text="Last Name"
                     AssociatedControlID="LastName"></asp:Label>
            <asp:TextBox ID="LastName" runat="server"></asp:TextBox>
                <br />  
            <asp:Label ID="Label7" runat="server" Text="Age"
                     AssociatedControlID="PlayerAge"></asp:Label>
            <asp:TextBox ID="PlayerAge" runat="server"></asp:TextBox>
                <br />  
            <asp:Label ID="Label8" runat="server" Text="Gender"
                     AssociatedControlID="PlayerGender"></asp:Label>
            <asp:RadioButtonList ID="PlayerGender" runat="server">
                <asp:ListItem Value="M">M</asp:ListItem>
                <asp:ListItem  Value="F">F</asp:ListItem>
            </asp:RadioButtonList>
                <br />  
            <asp:Label ID="Label9" runat="server" Text="Alberta Health Care Number"
                     AssociatedControlID="AlbertaHealthCareNumber"></asp:Label>
            <asp:TextBox ID="AlbertaHealthCareNumber" runat="server"></asp:TextBox>
                <br />  
            <asp:Label ID="Label10" runat="server" Text="Medical Alert Details"
                     AssociatedControlID="MedicalAlerts"></asp:Label>
            <asp:TextBox ID="MedicalAlerts" runat="server"></asp:TextBox>
        </fieldset>
    </div>
</asp:Content>
