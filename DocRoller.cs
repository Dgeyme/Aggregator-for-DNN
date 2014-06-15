using System;
using DotNetNuke.Data;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;

namespace DocumRoller
{
	// More attributes for class:
	// Set caching for table: [Cacheable("DocumRoller_DocRollers", CacheItemPriority.Default, 20)] 
	// Explicit mapping declaration: [DeclareColumns]
	
	// More attributes for class properties:
	// Custom column name: [ColumnName("DocRollerID")]
	// Explicit include column: [IncludeColumn]
	// Note: DAL 2 have no AutoJoin analogs from PetaPOCO at this time
	
	[TableName("DocumRoller_Documents")]
	[PrimaryKey("ItemID", AutoIncrement = true)]
	//[Scope("ModuleID")]
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

		public int ItemID { get; set; }

		public int ModuleID { get; set; }

		public string URL { get; set; }

		public string Title { get; set; }

		public string Category { get; set; }

		public int CreatedByUserId { get; set; }

		public int OwnedByUserId { get; set; }

		public int ModifiedByUserId { get; set; }

		public int SortOrderIndex { get; set; }

		public string Description { get; set; }

		public int ForceDownload { get; set; }

		[ReadOnlyColumn]
		public DateTime CreatedDate { get; set; }

		[ReadOnlyColumn]
		public DateTime ModifiedDate { get; set; }

		[IgnoreColumn] 
		public string CreatedByUserName {
			get {
				if (createdByUserName == null) {
					var portalId = PortalController.GetCurrentPortalSettings ().PortalId;
					var user = UserController.GetUserById (portalId, CreatedByUser);
					createdByUserName = user.DisplayName;
				}

				return createdByUserName; 
			}
		}
        #endregion
	}
}

