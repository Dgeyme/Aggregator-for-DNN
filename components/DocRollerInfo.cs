using System;
using System.Linq;
using DotNetNuke.Data;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Tabs;



namespace DocumRoller
{
	// More attributes for class:
	// Set caching for table: [Cacheable("DocumRoller_DocRollers", CacheItemPriority.Default, 20)] 
	// Explicit mapping declaration: [DeclareColumns]
	
	// More attributes for class properties:
	// Custom column name: [ColumnName("DocRollerID")]
	// Explicit include column: [IncludeColumn]
	// Note: DAL 2 have no AutoJoin analogs from PetaPOCO at this time
	
	[TableName("vw_Aggregator_ContentItems")]
	[PrimaryKey("ContentItemID", AutoIncrement = true)]
	[Scope("ContentTypeID")]
	public class DocRollerInfo
	{
        #region Fields
        
		private string createdByUserName = null;
		#endregion
		
		/// <summary>
		/// Empty default cstor
		/// </summary>
		public DocRollerInfo ()
		{
		}
        #region Properties

		public int ContentItemID { get; set; }

		public string Content { get; set; }

		//public int ContentTypeID { get; set; }

		//public int TabID { get; set; }

		//public int ModuleID { get; set; }

		//public string ContentKey { get; set; }

		//public bool Indexed { get; set; }

		//public int CreatedByUserID { get; set; }

		public DateTime CreatedOnDate { get; set; }

		//public int LastModifiedByUserID { get; set; }

		public DateTime LastModifiedOnDate { get; set; }

		//public int StateID { get; set; }


		[IgnoreColumn]
		public TabInfoEx Tab 
		{
			get 
			{

				//var tc = new TabController (); 
				var ctrl = new DocumRollerController ();
				var tabs = ctrl.GetObjects<TabInfoEx> ("WHERE ContentItemID=@0", ContentItemID);
				if (tabs == null || !tabs.Any())
					return null;
				else
					return tabs.First ();

			}
		
		}
		[IgnoreColumn]
		public DesktopModulesInfoEx DesktopModule 
		{
			get 
			{

				//var tc = new TabController (); 
				var ctrl = new DocumRollerController ();
				var desktopmodules = ctrl.GetObjects<DesktopModulesInfoEx> ("WHERE ContentItemID=@0", ContentItemID);
				if (desktopmodules == null || !desktopmodules.Any())
					return null;
				else
					return desktopmodules.First ();

			}

		}
		[IgnoreColumn]
		public ModuleInfoEx Module 
		{
			get 
			{

				//var tc = new TabController (); 
				var ctrl = new DocumRollerController ();
				var modules = ctrl.GetObjects<ModuleInfoEx> ("WHERE ContentItemID=@0", ContentItemID);
				if (modules == null || !modules.Any())
					return null;
				else
					return modules.First ();

			}

		}
		[IgnoreColumn]
		public TabModuleInfoEx TabModule 
		{
			get 
			{

				//var tc = new TabController (); 
				var ctrl = new DocumRollerController ();
				var modules = ctrl.GetObjects<ModuleInfoEx> ("WHERE ContentItemID=@0", ContentItemID);
				if (modules == null || !modules.Any ())
					return null;
				else {
					var m = modules.First ();
					var tabmodules = ctrl.GetObjects < TabModuleInfoEx> ("WHERE ModuleID=@0", m.ModuleID);
					if (tabmodules == null || !tabmodules.Any())
						return null;
					else
						return tabmodules.First ();

				}

			}

		}
	




		[IgnoreColumn]
		public JournalInfoEx Journal 
		{
			get 
			{

				//var tc = new TabController (); 
				var ctrl = new DocumRollerController ();
				var journal = ctrl.GetObjects<JournalInfoEx> ("WHERE ContentItemId=@0", ContentItemID);
				if (journal == null || !journal.Any())
					return null;
				else
					return journal.First ();

			}

		}



		[IgnoreColumn]
		public BlogInfoEx Blog 
		{
			get 
			{

				//var tc = new TabController (); 
				var ctrl = new DocumRollerController ();
				var blog = ctrl.GetObjects<BlogInfoEx> ("WHERE ContentItemId=@0", ContentItemID);
				if (blog == null || !blog.Any())
					return null;
				else
					return blog.First ();

			}

		}

        #endregion
	}

}

