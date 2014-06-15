using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DotNetNuke.UI.Modules;
using DotNetNuke.Common;

namespace DocumRoller
{
	public class Utils 
	{
		/// <summary>
		/// Finds the item index by it's value in ListControl-type list.
		/// </summary>
		/// <returns>Item index.</returns>
		/// <param name="list">List control.</param>
		/// <param name="value">A value.</param>
		/// <param name="defaultIndex">Default index (in case item not found).</param>
		public static int FindIndexByValue (ListControl list, object value, int defaultIndex)
		{ 
			var index = 0;
			var strvalue = value.ToString();
			foreach (ListItem item in list.Items)
			{
				if (item.Value == strvalue) return index;
				index++;
			}
			return defaultIndex; 
		}

		/// <summary>
		/// Sets the selected index of ListControl-type list.
		/// </summary>
		/// <param name="list">List control.</param>
		/// <param name="value">A value.</param>
		/// <param name="defaultIndex">Default index (in case item not found).</param>
		public static void SetIndexByValue (ListControl list, object value, int defaultIndex)
		{
			list.SelectedIndex = FindIndexByValue(list, value, defaultIndex);
		}
	}
}
