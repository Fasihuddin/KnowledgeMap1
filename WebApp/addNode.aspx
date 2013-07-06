<%@ Page Title="Add Module" Language="C#" MasterPageFile="~/InstMasterPage.master" AutoEventWireup="true" CodeFile="addNode.aspx.cs" Inherits="addNode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    
    <style type="text/css">
        .auto-style4
        {
        }
        .auto-style5
        {
            width: 246px;
            height: 23px;
        }
        .auto-style1
        {
            height: 23px;
            width: 378px;
        }
        .auto-style14
        {
            width: 640px;
        }
        .auto-style15
        {
            width: 378px;
        }
        .auto-style16
        {
            width: 246px;
        }
    </style>

    
    <div>
    
        <h3>Add New Modules</h3>
        <p>Add new module in this form. Please note that maximum length of module name is 30 characters.</p>
        <table class="auto-style14">
            <tr>
                <td class="auto-style16">Module Name: </td>
                <td class="auto-style15">
                    <asp:TextBox ID="txtNode" runat="server" Width="343px" MaxLength="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNode" ErrorMessage="* Node name is required" ForeColor="Red" ValidationGroup="NodeVal"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Description: </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtNodeDesc" runat="server" Height="76px" TextMode="MultiLine" Width="348px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Material Links:<br />
                    <em>(One link per line)<br />
                    Format: name;link<br />
                    <br />
                    For Example:<br />
                    Search engine;http://www.google.com</em></td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtLinks" runat="server" Height="146px" TextMode="MultiLine" Width="348px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style16">&nbsp;</td>
                <td class="auto-style15">
                    <asp:Button ID="btnAddNewNode" runat="server" Text="Add Module" OnClick="btnAddNewNode_Click" ValidationGroup="NodeVal" />
                &nbsp;
                    <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Finish Adding Modules" Width="164px" />
                </td>
            </tr>
            <tr>
                <td class="auto-style4" colspan="2">
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </asp:Content>
