using System;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.UI.UserControls;

namespace DocumRoller
{
	public partial class SettingsDocumRoller : ModuleSettingsBase
	{
		/// <summary>
		/// Handles the loading of the module setting for this control
		/// </summary>
		public override void LoadSettings ()
		{
			try {
				if (!IsPostBack) {
					Localize ();	
                		
					var settings = new DocumRollerSettings (this.ModuleId, this.TabModuleId);
										
					if (!string.IsNullOrWhiteSpace (settings.Template)) {
						//txtTemplate.Text = settings.Template;
					}


					txtItemToShow.Text = settings.ItemToShow.ToString ();
					chkSortOrder.Checked = settings.SortOrder;
					Utils.SetIndexByValue (ddlSortField,settings.SortField,0);

				

				}

			} catch (Exception ex) {
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}
		/// <summary>
		/// Localizes native ASP.NET controls. 
		/// DotNetNuke controls have localization of their own.  
		/// </summary>
		private void Localize ()
		{
			// someControl.Value = Localization.GetString ("SomeControl.Text", LocalResourceFile);
		}
		/// <summary>
		/// handles updating the module settings for this control
		/// </summary>
		public override void UpdateSettings ()
		{
			try {
				var settings = new DocumRollerSettings (this.ModuleId, this.TabModuleId);
				//settings.Template = txtTemplate.Text;

				settings.ItemToShow = int.Parse(txtItemToShow.Text);

				settings.SortOrder = chkSortOrder.Checked;

				settings.SortField = ddlSortField.SelectedValue;

			

			} catch (Exception ex) {
				Exceptions.ProcessModuleLoadException (this, ex);
			}
		}
	}
}

