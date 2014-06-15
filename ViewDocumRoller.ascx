<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewDocumRoller.ascx.cs" Inherits="DocumRoller.ViewDocumRoller" %>
<asp:DataList ID="lstContent" DataKeyField="ContentItemID" runat="server" CssClass="DocumRoller_ContentList" OnItemDataBound="lstContent_ItemDataBound">
	<ItemTemplate>
		<%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# EditUrl("ItemID",DataBinder.Eval(Container.DataItem,"ItemID").ToString()) %>' Visible="<%# IsEditable %>">
			<asp:Image ID="Image1" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit" Visible="<%#IsEditable%>" ResourceKey="Edit" />
		</asp:HyperLink>
		<%asp:Label ID="lblURL" runat="server" />
		<asp:Label ID="lblTitle" runat="server" />
		<asp:Label ID="lblCategory" runat="server" />
		<asp:Label ID="lblCreatedByUserId" runat="server" />
		<asp:Label ID="lblOwnedByUserId" runat="server" />
		<asp:Label ID="lblModifiedByUserId" runat="server" />
		<asp:Label ID="lblSortOrderIndex" runat="server" />
		<asp:Label ID="lblDescription" runat="server" />
		<asp:Label ID="lblCreatedDate" runat="server" />
		<asp:Label ID="lblModifiedDate" runat="server" /--%>
		<asp:HyperLink id="lnkDocument" runat="server">>
		<asp:Label id="lblContent" runat="server" />
		</asp:HyperLink>
		<asp:Label ID="lblCreatedOnDate" runat="server" />
		<%--<asp:Label ID="lblCreatedByUserID" runat="server" /--%>
		<asp:Label ID="lblLastModifiedOnDate" runat="server" />
		<%--<asp:Label ID="lblLastModifiedByUserID" runat="server" /--%>
		
		
		
		
	</ItemTemplate>
	<ItemStyle CssClass="DocumRoller_ContentListItem" />
</asp:DataList>
<asp:Label id="lblText" runat="server" />
<asp:HyperLink runat="server" id="linkRss" CssClass="dnnPrimaryAction">RSS</asp:HyperLink>
<asp:Image ID="Image1" runat="server" ImageUrl="~/images/rss.gif" AlternateText="RSS" Visible="<%#IsEditable%>" ResourceKey="RSS" />
		