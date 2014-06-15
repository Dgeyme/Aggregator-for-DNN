using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Services.FileSystem;

namespace DocumRoller
{
	public partial class ViewDocumRoller : PortalModuleBase, IActionable
	{
    	#region Handlers 
    	
    	/// <summary>
		/// Handles Page_Load event for a control
		/// </summary>
		/// <param name='sender'>
		/// Sender.
		/// </param>
		/// <param name='e'>
		/// Event args.
		/// </param>
		protected void Page_Load (object sender, EventArgs e)
		{
			try {
				if (!IsPostBack) {
				
					linkRss.NavigateUrl = EditUrl("Rss");

					var ctrl = new DocumRollerController ();
					//var items = ctrl.GetList<DocumRollerInfo> (this.ModuleId);
					var settings = new DocumRollerSettings (this.ModuleId, this.TabModuleId); 
					var docrollers = ctrl.GetObjects<DocRollerInfo> (System.Data.CommandType.StoredProcedure, 
					           "dbo.Aggregator_GetContent" , settings.ItemToShow, settings.SortOrder);


					// check if we have some content to display, 
					// otherwise display a sample default content from the resources
					/*if (items.Count == 0) {
						var item = new DocumRollerInfo () {
							ModuleID = this.ModuleId,
							CreatedByUser = this.UserId,
							Content = Localization.GetString ("DefaultContent", LocalResourceFile)
						};

						items.Add (item);
					}*/

					// bind the data
					lstContent.DataSource = docrollers;
					lstContent.DataBind ();
				}
			} catch (Exception ex) {
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}
		#endregion		
			
        #region IActionable implementation
        
		public DotNetNuke.Entities.Modules.Actions.ModuleActionCollection ModuleActions {
			get {
				// create a new action to add an item, this will be added 
				// to the controls dropdown menu
				var actions = new ModuleActionCollection ();

				actions.Add (
					GetNextActionID (), 
					Localization.GetString (ModuleActionType.AddContent, this.LocalResourceFile),
					ModuleActionType.AddContent, 
					"", 
					"", 
					EditUrl (), 
					false, 
					DotNetNuke.Security.SecurityAccessLevel.Edit,
					true, 
					false
				);

				actions.Add (
					GetNextActionID (), 
					"RSS",//Localization.GetString (ModuleActionType.AddContent, this.LocalResourceFile),
					ModuleActionType.AddContent, 
					"", 
					"", 
					EditUrl ("Rss"), 
					false, 
					DotNetNuke.Security.SecurityAccessLevel.View,
					true, 
					false
					);

				return actions;
			}
		}
        #endregion

		/// <summary>
		/// Handles the items being bound to the datalist control. In this method we merge the data with the
		/// template defined for this control to produce the result to display to the user
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void lstContent_ItemDataBound (object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
		{
			// use e.Item.DataItem as object of DocumRollerInfo class,
			// as we really know it is:
			var item = e.Item.DataItem as DocRollerInfo;

			var contentLabel = e.Item.FindControl ("lblContent") as Label;
			var createdondateLabel = e.Item.FindControl ("lblCreatedOnDate") as Label;
			var createdbyuseridLabel = e.Item.FindControl ("lblCreatedByUserID") as Label;
			var lastmodifiedondateLabel = e.Item.FindControl ("lblLastModifiedOnDate") as Label;
			var modifiedbyuseridLabel = e.Item.FindControl ("lblLastModifiedByUserID") as Label;
			var titlelink = e.Item.FindControl ("lnkDocument") as HyperLink;


			// find controls in DataList item template
			//var contentLabel = e.Item.FindControl ("lblContent") as Label;
			/*var urlLabel = e.Item.FindControl ("lblURL") as Label;
			var titleLabel = e.Item.FindControl ("lblTitle") as Label;
			var categoryLabel = e.Item.FindControl ("lblCategory") as Label;
			var createdbyuseridLabel = e.Item.FindControl ("lblCreatedByUserId") as Label;
			var ownedbyuseridLabel = e.Item.FindControl ("lblOwnedByUserId") as Label;
			var modifiedbyuseridLabel = e.Item.FindControl ("lblModifiedByUserId") as Label;
			var sortorderindexLabel = e.Item.FindControl ("lblSortOrderIndex") as Label;
			var descriptionLabel = e.Item.FindControl ("lblDescription") as Label;
			var createddateLabel = e.Item.FindControl ("lblCreatedDate") as Label;
			var modifieddateLabel = e.Item.FindControl ("lblModifiedDate") as Label;
*/
// setting default values
//			var contentValue = string.Empty;
            
			// read module settings
			var settings = new DocumRollerSettings (this.ModuleId, this.TabModuleId);            

			contentLabel.Text = Server.HtmlDecode(item.Content);
			createdondateLabel.Text = item.CreatedOnDate.ToLongDateString();

			lastmodifiedondateLabel.Text = item.LastModifiedOnDate.ToLongDateString();

		
			//titlelink.NavigateUrl = FileManager.Instance.GetUrl (
				//FileManager.Instance.GetFile(int.Parse(item.URL.ToUpper().Replace("FILEID=",""))));
			titlelink.Target = "_blank";
			if (item.Tab != null)
			titlelink.NavigateUrl ="/default.aspx?TabId="+ item.Tab.TabID;
			if (item.DesktopModule != null)
				titlelink.NavigateUrl ="/default.aspx?DesktopModuleId="+ item.DesktopModule.DesktopModuleID;
			if (item.TabModule != null)
				titlelink.NavigateUrl =string.Format ("/default.aspx?tabid={0}&mid={1}", item.TabModule.TabID, item.TabModule.ModuleID);
			if (item.Journal != null)
				titlelink.NavigateUrl ="/default.aspx?JournalId="+ item.Journal.JournalID;
			if (item.Blog != null)
				titlelink.NavigateUrl ="/default.aspx?ContentItemId="+ item.Blog.ContentItemId;

/*			urlLabel.Text = Server.HtmlDecode(item.URL);
			titleLabel.Text = Server.HtmlDecode(item.Title);
			categoryLabel.Text = Server.HtmlDecode(item.Category);
			createdbyuseridLabel.Text = item.CreatedByUserId.ToString();
			ownedbyuseridLabel.Text = item.OwnedByUserId.ToString();
			modifiedbyuseridLabel.Text = item.ModifiedByUserId.ToString();
			sortorderindexLabel.Text = item.SortOrderIndex.ToString();
			descriptionLabel.Text = Server.HtmlDecode(item.Description);
			createddateLabel.Text = item.CreatedDate.ToLongDateString();
			modifieddateLabel.Text = item.ModifiedDate.ToLongDateString();
*/
		}

	}
}

