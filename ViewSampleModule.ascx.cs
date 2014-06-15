using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using DotNetNuke;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.UI.WebControls;
using DotNetNuke.UI.UserControls;
using DotNetNuke.Security;


//using Redhound.DNN;
using YourCompany.SampleModule.Components;

namespace YourCompany.Modules.SampleModule
{

	public partial class ViewSampleModule : PortalModuleBase, IActionable
	{

		#region IActionable Members

		public DotNetNuke.Entities.Modules.Actions.ModuleActionCollection ModuleActions
		{
			get
			{
				//create a new action to add an item, this will be added to the controls dropdown menu
				ModuleActionCollection actions = new ModuleActionCollection();

				// Действие "Добавить новость"
				actions.Add(GetNextActionID(), Localization.GetString(ModuleActionType.AddContent, this.LocalResourceFile),
				            ModuleActionType.AddContent, "", "", EditUrl(), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
				            true, false);

				if (ModuleConfiguration.DisplaySyndicate)
				{
					actions.Add(GetNextActionID(),
					            //Localization.GetString(ModuleActionType.SyndicateModule, this.LocalResourceFile),
					            Localization.GetString("RssExportAction.Text", LocalResourceFile),
					            ModuleActionType.AddContent, "",

					            //ResolveUrl("~/rss.gif"),
					            //"http://" + PortalAlias.HTTPAlias + "/images/rss.gif",
					            "rss.gif",

					            EditUrl("RSS"), false, DotNetNuke.Security.SecurityAccessLevel.View,
					            true, true);
				}

				// Вместо "RSS" должно быть ModuleActionType.SyndicateModule
				// запрос с параметрами: EditUrl("records", "10", "RSS")

				return actions;
			}
		}

		#endregion


	}
}



