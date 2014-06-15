using System;

namespace DocumRoller
{
	/// <summary>
	/// Provides strong typed access to settings used by module
	/// </summary>
	public partial class DocumRollerSettings
	{
		#region Properties for settings

		/// <summary>
		/// Template used to render the module content
		/// </summary>
		public string Template {
			get { 
				return ReadSetting<string> ("template", 
				                            "<i>[CREATEDONDATE]<i> <b>[CREATEDBYUSERNAME]</b>:<br />[CONTENT]", 
				                            true); 
			}
			set { WriteSetting ("template", value, true);
			}

		}

		public int ItemToShow {
		get {
				return ReadSetting<int> ("itemtoshow", 5, true);
			}
			set { WriteSetting ("itemtoshow", value.ToString (), true);
			}
		}

		public string SortField {
			get {
				return ReadSetting<string> ("sortfield", "createddate", true);
			}
			set { WriteSetting ("sortfield", value, true);
			}
		}

		public bool SortOrder {
			get {
				return ReadSetting<bool> ("sortorder", true, true);
			}
			set { WriteSetting ("sortorder", value.ToString (), true);
			}
		}
        #endregion
	} 
}

