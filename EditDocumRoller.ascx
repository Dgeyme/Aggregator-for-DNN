<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditDocumRoller.ascx.cs" Inherits="DocumRoller.EditDocumRoller" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx"%>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>

<table width="100%" cellspacing="0" cellpadding="0" border="0">
	<tr valign="top">
		<td class="SubHead" width="33%">
			<dnn:Label ID="lblContent" runat="server" ControlName="lblContent" Suffix=":"></dnn:Label>
		</td>
		<td valign="top">
			<dnn:TextEditor ID="txtContent" runat="server" Height="200" Width="100%" />
			<asp:RequiredFieldValidator ID="valContent" runat="server" resourcekey="valContent.ErrorMessage" ControlToValidate="txtContent"
				CssClass="NormalRed" Display="Dynamic" ErrorMessage="<br />Content is required" />
		</td>
	</tr>
</table>
<p>
    <asp:LinkButton ID="cmdUpdate" runat="server" CssClass="CommandButton" ResourceKey="cmdUpdate" BorderStyle="none" Text="Update" OnClick="cmdUpdate_Click"></asp:LinkButton>&#160;
    <asp:LinkButton ID="cmdCancel" runat="server" CssClass="CommandButton" ResourceKey="cmdCancel" BorderStyle="none" Text="Cancel" CausesValidation="False" OnClick="cmdCancel_Click"></asp:LinkButton>&#160;
    <asp:LinkButton ID="cmdDelete" runat="server" CssClass="CommandButton" ResourceKey="cmdDelete" BorderStyle="none" Text="Delete" CausesValidation="False" OnClick="cmdDelete_Click"></asp:LinkButton>
</p>
<dnn:Audit id="ctlAudit" runat="server" />

