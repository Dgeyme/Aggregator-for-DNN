using System;
using DotNetNuke.Data;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;

namespace DocumRoller
{
	// More attributes for class:
	// Set caching for table: [Cacheable("DocumRoller_DocumRollerInfos", CacheItemPriority.Default, 20)] 
	// Explicit mapping declaration: [DeclareColumns]

	// More attributes for class properties:
	// Custom column name: [ColumnName("DocumRollerID")]
	// Explicit include column: [IncludeColumn]
	// Note: DAL 2 have no AutoJoin analogs from PetaPOCO at this time

	[TableName("DesktopModules")]
	[PrimaryKey("DesktopModuleID", AutoIncrement = true)]
	[Scope("ContentItemID")]
	public class DesktopModulesInfoEx
	{
		#region Fields

		private string createdByUserName = null;
		#endregion

		/// <summary>
		/// Empty default cstor
		/// </summary>
		public DesktopModulesInfoEx()
		{
		}
		#region Properties

		public int DesktopModuleID{ get; set; }

		public string FriendlyName{ get; set; }




		#endregion

		/* // Joins example
     	
     	// foreign key
     	public int AnotherID { get; set; }
     	
     	// private object reference
     	private AnotherInfo _another;
     	
     	// public object reference
     	public AnotherInfo Another 
     	{
     	   	// this getter method hide underlying access to database, 
     	   	// and perform simple caching by storing reference
     	   	// to retrived AnotherInfo object in a private field "_another"
     		get 
     		{
     			if (_other == null)
     			{
     				// load joined object to reference it
     				var ctrl = new DocumRollerController();
     				_another = ctrl.Get<AnotherInfo>(AnotherID);
     			}
     			return _another;	
     		}
     		set 
     		{
     			_another = value;
     		}
     	}      
     	
     	/// <summary>
     	/// Nullifies all private fields with references to joined objects,
     	/// so next access to corresponding object properties 
     	/// results in reloading them from the database  
     	/// </summary>
     	public void ResetJoins ()
     	{
     		_another = null;
     	}
        
        // Now we have ability to use DocumRollerInfo objects
        // to access members of joined AnotherInfo objects 
        
       	// Get DocumRollerInfo object by it's primary key (ID):
       	// var ctrl = new DocumRollerController();
     	// var item = ctrl.Get<DocumRollerInfo>(itemId);
     	
     	// Now simply get data from another table:
     	// Console.WriteLine(item.Another.SomeProperty);
     	
        // True is, that it is not very effective way to retrieve multiple objects, 
        // but it is 1) simple and 2) object-oriented, so then PetaPOCO AutoJoin 
        // attribute will be included in DAL 2, existing business logic code 
        // can be upgraded with almost no efforts.
       
        */
	}
}


