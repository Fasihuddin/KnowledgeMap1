<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addModuleTest.aspx.cs" Inherits="addModuleTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="CSS/Main.css" />
    <style type="text/css">
        .auto-style2
        {
            height: 23px;
            width: 141px;
        }
        .auto-style3
        {
            width: 141px;
        }
        .auto-style4
        {
            height: 23px;
            width: 380px;
        }
        .auto-style5
        {
            width: 380px;
        }
        .auto-style6
        {
            width: 141px;
            height: 26px;
        }
        .auto-style7
        {
            width: 380px;
            height: 26px;
        }
        .auto-style14
        {
            width: 109px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <h1>Create Tests for a Module</h1>
        <br />
        <table style="width: 56%;">
            <tr>
                <td class="auto-style2">Select Course:</td>
                <td class="auto-style4">
                    <asp:DropDownList ID="ddlCourse" runat="server" Height="31px" Width="352px" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">Select Topic:</td>
                <td class="auto-style7">
                    <asp:DropDownList ID="ddlTopic" OnSelectedIndexChanged="ddlTopic_SelectedIndexChanged" runat="server" Height="25px" Width="352px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Select Module:</td>
                <td class="auto-style5">
                    <asp:DropDownList ID="ddlModule" runat="server" Height="26px" Width="352px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style5">
                    <asp:Button ID="btnTest" runat="server" Text="Create Test" Width="168px" OnClick="btnTest_Click" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Panel ID="pnlTest" runat="server" Height="367px" Visible="False">
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Test Questions"></asp:Label>
            <br />
            <br />
            <asp:GridView ID="gvEG" runat="server" EmptyDataText ="No Data" AutoGenerateColumns="False" CssClass="grid"
            AlternatingRowStyle-CssClass="gridAltRow" RowStyle-CssClass="gridRow" ShowFooter="True"
            EditRowStyle-CssClass="gridEditRow" FooterStyle-CssClass="gridFooterRow"
            OnRowCommand="gvEG_RowCommand" OnRowDeleting="gvEG_RowDeleting" OnRowUpdating="gvEG_RowUpdating"
            OnRowEditing="gvEG_RowEditing" >
                <AlternatingRowStyle CssClass="gridAltRow" />
                <Columns>
                    <asp:TemplateField HeaderText="ID"></asp:TemplateField>
                    <asp:TemplateField HeaderText="Question Text">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtQuestionText" runat="server" Height="16px" Width="420px" Text='<%# Bind("Text") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvQuestionText" ValidationGroup="Insert" runat="server"
                            ControlToValidate="txtQuestionText" ErrorMessage="Please Enter Question Text"
                            ToolTip="Please Enter Question Text" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Text") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:TextBox ID="txtQuestionText" runat="server" Height="16px" Width="420px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvQuestionText" ValidationGroup="Insert" runat="server"
                            ControlToValidate="txtQuestionText" ErrorMessage="Please Enter Question Text"
                            ToolTip="Please Enter Question Text" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </FooterTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Choices">
                        <EditItemTemplate>
                       Option 1:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdbAnswer1" runat="server" Text="Answer" GroupName="Answer" Checked="True" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Update" runat="server"
                            ControlToValidate="txtAnswer1" ErrorMessage="Please Enter Option 1"
                            ToolTip="Please Enter Option 1" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator> <br />
                                <asp:TextBox ID="txtAnswer1" runat="server" Height="16px" Width="420px" Text='<%# Bind("choice1") %>'></asp:TextBox> <br />
                                Option 2: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdbAnswer2" runat="server" Text="Answer" GroupName="Answer"/> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Update" runat="server"
                            ControlToValidate="txtAnswer2" ErrorMessage="Please Enter Option 2"
                            ToolTip="Please Enter Option 2" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="txtAnswer2" runat="server" Height="16px" Width="420px" Text='<%# Bind("choice2") %>'></asp:TextBox> <br />
                                Option 3: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdbAnswer3" runat="server" Text="Answer" GroupName="Answer" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Update" runat="server"
                            ControlToValidate="txtAnswer3" ErrorMessage="Please Enter Option 3"
                            ToolTip="Please Enter Option 3" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="txtAnswer3" runat="server" Height="16px" Width="420px" Text='<%# Bind("choice3") %>'></asp:TextBox> <br />
                                Option 4: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdbAnswer4" runat="server" Text="Answer" GroupName="Answer"/> 
                            <asp:RequiredFieldValidator ID="rfvChoice4" ValidationGroup="Update" runat="server"
                            ControlToValidate="txtAnswer4" ErrorMessage="Please Enter Option 4"
                            ToolTip="Please Enter Option 4" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="txtAnswer4" runat="server" Height="16px" Width="420px" Text='<%# Bind("choice4") %>'></asp:TextBox> <br />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Choice1" runat="server" Text='<%# Bind("choice1") %>'></asp:Label><br />
                            <asp:Label ID="Choice2" runat="server" Text='<%# Bind("choice2") %>'></asp:Label><br />
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("choice3") %>'></asp:Label><br />
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("choice4") %>'></asp:Label><br />
                        </ItemTemplate>
                        <FooterTemplate>
                       Option 1:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdbAnswer1" runat="server" Text="Answer" GroupName="Answer" Checked="True" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="Insert" runat="server"
                            ControlToValidate="txtAnswer1" ErrorMessage="Please Enter Option 1"
                            ToolTip="Please Enter Option 1" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="txtAnswer1" runat="server" Height="16px" Width="420px"></asp:TextBox> <br />
                                Option 2: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdbAnswer2" runat="server" Text="Answer" GroupName="Answer"/> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="Insert" runat="server"
                            ControlToValidate="txtAnswer2" ErrorMessage="Please Enter Option 2"
                            ToolTip="Please Enter Option 2" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="txtAnswer2" runat="server" Height="16px" Width="420px"></asp:TextBox> <br />
                                Option 3: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdbAnswer3" runat="server" Text="Answer" GroupName="Answer" /> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Insert" runat="server"
                            ControlToValidate="txtAnswer3" ErrorMessage="Please Enter Option 3"
                            ToolTip="Please Enter Option 3" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="txtAnswer3" runat="server" Height="16px" Width="420px"></asp:TextBox> <br />
                                Option 4: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdbAnswer4" runat="server" Text="Answer" GroupName="Answer"/> 
                             <asp:RequiredFieldValidator ID="rfvChoice4" ValidationGroup="Insert" runat="server"
                            ControlToValidate="txtAnswer4" ErrorMessage="Please Enter Option 4"
                            ToolTip="Please Enter Option 4" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="txtAnswer4" runat="server" Height="16px" Width="420px"></asp:TextBox> <br />
                    </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Strength">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlStrength" runat="server" SelectedValue='<%# Bind("strength") %>'>
                                    <asp:ListItem Selected="True" Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("strength") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:DropDownList ID="ddlStrength" runat="server">
                                    <asp:ListItem Selected="True" Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                </asp:DropDownList>
                    </FooterTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                        <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                            Text="Update" OnClientClick="return confirm('Update?')" ValidationGroup="Update"></asp:LinkButton>
                        <asp:ValidationSummary ID="vsUpdate" runat="server" ShowMessageBox="true" ShowSummary="false"
                            ValidationGroup="Update" Enabled="true" HeaderText="Validation Summary..." />
                        <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="lnkAdd" runat="server" CausesValidation="True" CommandName="Insert"
                            ValidationGroup="Insert" Text="Insert"></asp:LinkButton>
                        <asp:ValidationSummary ID="vsInsert" runat="server" ShowMessageBox="true" ShowSummary="false"
                            ValidationGroup="Insert" Enabled="true" HeaderText="Validation..." />
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="Edit"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                         <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="Delete" OnClientClick="return confirm('Delete?')"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
       
                <EditRowStyle CssClass="gridEditRow" />
                <EmptyDataTemplate>
                    <table id="gvEG" border="1" cellspacing="0" class="grid" rules="all" style="border-collapse: collapse;">
                        <tr>
                            <th align="left" scope="col">No.</th>
                            <th align="left" scope="col">Question Text</th>
                            <th align="left" scope="col">Answer Options </th>
                            <th align="left" scope="col">Difficulty (Strength) </th>
                            <th align="left" scope="col">Edit</th>
                            <th scope="col">Delete </th>
                        </tr>
                        <tr class="gridFooterRow">
                            <td>
                                <asp:TextBox ID="txtNo" runat="server" MaxLength="6" Width="34px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQuestionText" runat="server" Height="16px" Width="420px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvQuestionText" ValidationGroup="emptyInsert" runat="server"
                            ControlToValidate="txtQuestionText" ErrorMessage="Please Enter Question Text"
                            ToolTip="Please Enter Question Text" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                            <td class="auto-style14">
                                Option 1:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdbAnswer1" runat="server" Text="Answer" GroupName="Answer" Checked="True" /> 
                                <asp:RequiredFieldValidator ID="rfvChoice1" ValidationGroup="emptyInsert" runat="server"
                            ControlToValidate="txtAnswer1" ErrorMessage="Please Enter Option 1"
                            ToolTip="Please Enter Option 1" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator> <br />
                                <asp:TextBox ID="txtAnswer1" runat="server" Height="16px" Width="420px"></asp:TextBox>
                                Option 2: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdbAnswer2" runat="server" Text="Answer" GroupName="Answer"/> 
                                 <asp:RequiredFieldValidator ID="rfvChoice2" ValidationGroup="emptyInsert" runat="server"
                            ControlToValidate="txtAnswer2" ErrorMessage="Please Enter Option 2"
                            ToolTip="Please Enter Option 2" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator> <br />
                                <asp:TextBox ID="txtAnswer2" runat="server" Height="16px" Width="420px"></asp:TextBox>
                                Option 3: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdbAnswer3" runat="server" Text="Answer" GroupName="Answer" />
                                <asp:RequiredFieldValidator ID="rfvChoice3" ValidationGroup="emptyInsert" runat="server"
                            ControlToValidate="txtAnswer3" ErrorMessage="Please Enter Option 3"
                            ToolTip="Please Enter Option 3" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator> <br />
                                <asp:TextBox ID="txtAnswer3" runat="server" Height="16px" Width="420px"></asp:TextBox>
                                Option 4: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdbAnswer4" runat="server" Text="Answer" GroupName="Answer"/> 
                                <asp:RequiredFieldValidator ID="rfvChoice4" ValidationGroup="emptyInsert" runat="server"
                            ControlToValidate="txtAnswer4" ErrorMessage="Please Enter Option 4"
                            ToolTip="Please Enter Option 4" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="txtAnswer4" runat="server" Height="16px" Width="420px"></asp:TextBox>
                                
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStrength" runat="server">
                                    <asp:ListItem Selected="True" Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="justify" colspan="2" valign="middle">
                                <asp:LinkButton ID="lnkAdd" runat="server" CausesValidation="True" ValidationGroup="emptyInsert" CommandName="emptyInsert" Text="emptyInsert"></asp:LinkButton>
                                 <asp:ValidationSummary ID="vsInsert" runat="server" ShowMessageBox="true" ShowSummary="false"
                            ValidationGroup="emptyInsert" Enabled="true" HeaderText="Validation..." />
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <FooterStyle CssClass="gridFooterRow" />
                <RowStyle CssClass="gridRow" />
        </asp:GridView>

            <br />
            <br />

            <br />
            <br />
        </asp:Panel>
    
    </div>
    </form>
</body>
</html>
