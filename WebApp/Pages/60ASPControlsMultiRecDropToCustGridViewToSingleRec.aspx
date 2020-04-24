<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="60ASPControlsMultiRecDropToCustGridViewToSingleRec.aspx.cs" Inherits="WebApp.Pages._60ASPControlsMultiRecDropToCustGridViewToSingleRec" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <h1> Multi Record Query Dropdown to Custom GridView to Single Record via Page Navigation</h1>
    <div class="offset-2">
        <asp:Label ID="Label1" runat="server" Text="Select a Category "></asp:Label>&nbsp;&nbsp;   
        <asp:DropDownList ID="List01" runat="server"></asp:DropDownList>&nbsp;&nbsp;
        <asp:Button ID="Fetch" runat="server" Text="Fetch" 
             CausesValidation="false" OnClick="Fetch_Click"/>
        <br /><br />
        <asp:Label ID="MessageLabel" runat="server" ></asp:Label>
        <br />
        <asp:GridView ID="PlayerList" runat="server"
                 AutoGenerateColumns="False"
                    CssClass="table table-striped" GridLines="Both"
                    BorderStyle="None" AllowPaging="True" OnPageIndexChanging="TeamList_PageIndexChanging" PageSize="5">
                 <Columns>

                     <asp:TemplateField HeaderText="Name">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="PlayerName" runat="server"
                                    Text='<%# Eval("PlayerName") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Age">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="PlayerAge" runat="server"
                                    Text='<%# Eval("Age") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Gender">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="PlayerGender" runat="server"
                                    Text='<%# Eval("Gender") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Med Alert">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="PlayerMedAlert" runat="server"
                                    Text='<%# Eval("MedicalAlertDetails") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            <EmptyDataTemplate>
                no data available at this time.
            </EmptyDataTemplate>
            <PagerSettings FirstPageText="Start" LastPageText="End" Mode="NumericFirstLast" PageButtonCount="3" />
            </asp:GridView>
    </div>
</asp:Content>
