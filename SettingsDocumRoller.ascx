<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SettingsDocumRoller.ascx.cs" Inherits="DocumRoller.SettingsDocumRoller" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<table cellspacing="0" cellpadding="0" border="0">
	<%-- <tr> 
  <td class="SubHead" width="200" valign="top">
   <dnn:Label ID="lblTemplate" runat="server" ControlName="txtTemplate" Suffix=":"></dnn:Label>
  </td>
     <td valign="top">
         <asp:TextBox ID="txtTemplate" runat="server" CssClass="NormalTextBox"  />
     </td>
 </tr>--%>
	
	<tr>	
		<td class="SubHead" width="200" valign="top">
			<dnn:Label ID="lblItemToShow" runat="server" ControlName="txtItemToShow" Suffix=":"></dnn:Label>
		</td>
    	<td valign="top">
        	<asp:TextBox ID="txtItemToShow" runat="server" CssClass="NormalTextBox"  />
    	</td>
	</tr>
	<tr>	
		<td class="SubHead" width="200" valign="top">
			<dnn:Label ID="lblSortField" runat="server" ControlName="ddlSortField" Suffix=":"></dnn:Label>
		</td>
    	<td valign="top">
        	<asp:DropDownList ID="ddlSortField" runat="server" CssClass="NormalTextBox">
        	<asp:ListItem Value="createddate" Selected = "true"  ResourceKey="ddlSortFieldItem1.Text" />
        	<asp:ListItem Value="title" Selected = "true"  ResourceKey="ddlSortFieldItem2.Text" />
        	</asp:DropDownList>
    	</td>
	</tr>
	
	
	<tr>	
		<td class="SubHead" width="200" valign="top">
			<dnn:Label ID="lblSortOrder" runat="server" ControlName="chkSortOrder" Suffix=":"></dnn:Label>
		</td>
    	<td valign="top">
        	<asp:CheckBox ID="chkSortOrder" runat="server" CssClass="NormalTextBox">
        	</asp:CheckBox>
    	</td>
	</tr>
	
	
	

	
</table>

