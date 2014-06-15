using System;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.UserControls;

namespace DocumRoller
{
	public partial class EditDocumRoller : PortalModuleBase
	{
		// ALT: private int itemId = Null.NullInteger;
		private int? itemId = null;
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
				// parse querystring parameters
				int tmpItemId;
				if (int.TryParse (Request.QueryString ["AggregatorID"], out tmpItemId))
					itemId = tmpItemId;
      
				if (!IsPostBack) {
					// load the data into the control the first time we hit this page

					cmdDelete.Attributes.Add ("onClick", "javascript:return confirm('" + Localization.GetString ("DeleteItem") + "');");

					// check we have an item to lookup
					// ALT: if (!Null.IsNull (itemId) 
					if (itemId.HasValue) {
						// load the item
						var ctrl = new DocumRollerController ();
						var item = ctrl.Get<DocumRollerInfo> (itemId.Value, this.ModuleId);

						if (item != null) {
							// TODO: Fill controls with data
							txtContent.Text = item.Content;
							
							// setup audit control

							ctlAudit.CreatedDate = item.CreatedOnDate.ToLongDateString ();
							ctlAudit.LastModifiedDate = item.LastModifiedOnDate.ToLongDateString ();
						} else
							Response.Redirect (Globals.NavigateURL (), true);
					} else {
						cmdDelete.Visible = false;
						ctlAudit.Visible = false;
					}
				}
			} catch (Exception ex) {
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}
		/// <summary>
		/// Handles Click event for cmdUpdate button
		/// </summary>
		/// <param name='sender'>
		/// Sender.
		/// </param>
		/// <param name='e'>
		/// Event args.
		/// </param>
		protected void cmdUpdate_Click (object sender, EventArgs e)
		{
			try {
				var ctrl = new DocumRollerController ();
				DocumRollerInfo item;

				// determine if we are adding or updating
				// ALT: if (Null.IsNull (itemId))
				if (!itemId.HasValue) {
					// TODO: populate new object properties with data from controls 
					// to add new record
					item = new DocumRollerInfo ();
					item.Content = txtContent.Text;
					item.ModuleID = this.ModuleId;
										

					ctrl.Add<DocumRollerInfo> (item);
				} else {
					// TODO: update properties of existing object with data from controls 
					// to update existing record
					item = ctrl.Get<DocumRollerInfo> (itemId.Value, this.ModuleId);
					item.Content = txtContent.Text;

					ctrl.Update<DocumRollerInfo> (item);
				}

				Response.Redirect (Globals.NavigateURL (), true);
			} catch (Exception ex) {
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}
		/// <summary>
		/// Handles Click event for cmdCancel button
		/// </summary>
		/// <param name='sender'>
		/// Sender.
		/// </param>
		/// <param name='e'>
		/// Event args.
		/// </param>
		protected void cmdCancel_Click (object sender, EventArgs e)
		{
			try {
				Response.Redirect (Globals.NavigateURL (), true);
			} catch (Exception ex) {
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}
		/// <summary>
		/// Handles Click event for cmdDelete button
		/// </summary>
		/// <param name='sender'>
		/// Sender.
		/// </param>
		/// <param name='e'>
		/// Event args.
		/// </param>
		protected void cmdDelete_Click (object sender, EventArgs e)
		{
			try {
				// ALT: if (!Null.IsNull (itemId))
				if (itemId.HasValue) {
					var ctrl = new DocumRollerController ();
					ctrl.Delete<DocumRollerInfo> (itemId.Value);
					Response.Redirect (Globals.NavigateURL (), true);
				}
			} catch (Exception ex) {
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}
		#endregion
	}
}

