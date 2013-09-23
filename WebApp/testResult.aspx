<%@ Page Title="Login" Language="C#" AutoEventWireup="true" CodeFile="testResult.aspx.cs" Inherits="testResult"  maintainScrollPositionOnPostBack="true" MasterPageFile="~/MasterPage.master"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Test result</h3>
  
    <div>
    
        
    
    </div>
        <p>
            Test Date/Time:
            <asp:Label ID="DateTimeLabel" runat="server"></asp:Label>
        </p>
        <p>
            Module:<asp:Label ID="NodeIdLabel" runat="server"></asp:Label>
        </p>
        <p>
            <asp:Label ID="LblErrMsg" runat="server"></asp:Label>
        </p>
        <p>
            Your total score is :
            <asp:Label ID="ResultLabel" runat="server"></asp:Label>
        </p>
        <p>
            <asp:Label ID="ResultMsg" runat="server" Text="ResultMsg"></asp:Label>
        </p>
        <br />
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Question">
                    <ItemTemplate>
                            <asp:Label ID="Question" runat="server" Text='<%# Bind("Question") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Selected Answer">
                    <ItemTemplate>
                            <asp:Label ID="Answer" runat="server" Text='<%# Bind("Answer") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IsRight">
                    <ItemTemplate>
                            <asp:Label ID="IsRight" runat="server" Text='<%# Bind("IsRight") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Result">
                    <ItemTemplate>
                            <asp:Label ID="AnswerResult" runat="server" Text='<%# Bind("AnswerResult") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Weight">
                    <ItemTemplate>
                            <asp:Label ID="QuestionWeight" runat="server" Text='<%# Bind("QuestionWeight") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="ShowAns" runat="server" CausesValidation="false" CommandName="" Text="Show the right answer" Visible="False" OnClick="GridView1_SelectedIndexChanged"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField>
                    <ItemTemplate>
                            <asp:Label ID="RightAnswer" runat="server" Text='<%# Bind("RightAnswer") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    <br />
    <asp:Button ID="btnBack" runat="server" Text="Back to Course Selection." OnClick="btnBack_Click" />
   </asp:Content>
