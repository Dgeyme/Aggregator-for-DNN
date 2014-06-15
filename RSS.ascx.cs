using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.IO;
using System.Globalization;


using DotNetNuke;
using DotNetNuke.Common;
using DotNetNuke.Entities;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Entities.Modules;





namespace DocumRoller
{
	/*
    public class RssChannelInfo { 
       
        
    }
     */

	public partial class RSS : PortalModuleBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			//throw (new Exception("I'm working!"));
			if (!IsPostBack)
			{
				#region RSS OPTIONS
				/*
                if (!String.IsNullOrEmpty(Request.QueryString["records"]))
                {
                    records = int.Parse(Request.QueryString["records"]);
                }*/
				// PortalSettings.PortalAlias;
				string title = new ModuleController().GetTabModule(TabModuleId).ModuleTitle;
				string description = "descr";


				var settings = new DocumRollerSettings (this.ModuleId, this.TabModuleId); 
				int records = settings.ItemToShow;

					
//int.TryParse((string)Settings["rss_records"], out records);

				// определяем язык запроса
				// PortalSettings.CultureCode
				string language = PortalSettings.CultureCode; // язык по-умолчанию - русский
				/*
                if (!String.IsNullOrEmpty(Request.QueryString["language"]))
                    language = Request.QueryString["language"].ToLower();
                */
				#endregion


				if (records > 0)
				{
					// проверяем, существует ли директория с кешем rss
					// если нет, создаем
					//string rss_dname = Path.Combine(PortalSettings.HomeDirectoryMapPath, "Cache");
					//if (!Directory.Exists(rss_dname))
					//	Directory.CreateDirectory(rss_dname);

					//Directory.SetCurrentDirectory(rss_dname);

					// формируем имя файла кеша rss для нашего модуля
					/*string rssfile_fullname = Path.Combine(
						rss_dname,
						String.Format("Feed_{0}.rss", ModuleId));
*/
					// устанавливаем, нужно ли обновлять rss канал?
					// в данном случае время устаревания - cachetime часов
/*					bool
						need_regen = !File.Exists(rssfile_fullname);
					if (!need_regen) need_regen =
						// актуальность канала - cachetime часов
						File.GetLastWriteTime(rssfile_fullname) + new TimeSpan(cachetime, 0, 0) < DateTime.Now;

					// для отладки!!!
					//need_regen = true;

*/					// если нужна перегенерация, формируем xml-файл в кеше rss
					//if (true) //need_regen)
					{
						var ctrl = new DocumRollerController ();
						//var items = ctrl.GetList<DocumRollerInfo> (this.ModuleId);
						var docrollers = ctrl.GetObjects<DocRollerInfo> (System.Data.CommandType.StoredProcedure, 
						                                                 "dbo.Aggregator_GetContent" , settings.ItemToShow, settings.SortOrder);
						// labeltest.Text = Server.HtmlEncode(ToRss (language, title, description));
						Response.Clear ();
						Response.ContentType = "application/rss+xml";
						Response.Write (ToRss(docrollers, language, title, description));
						Response.Flush ();
						Response.Close ();
					}
				}
			}
		}

		protected Label labeltest;

		private string ToRss (
			IEnumerable<DocRollerInfo> lai, 
			string language, 
			string title,
			string description
			)
		{
			//CultureInfo culture = new CultureInfo(language);
			//string channel = string.Empty;

			const string eol = "";
			string channel_tpl = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + eol +
				"<rss version=\"2.0\" xmlns:atom=\"http://www.w3.org/2005/Atom\">" + eol +
					"<channel>" + eol +
					"<title>[TITLE]</title>" + eol +
					"<link>[LINK]</link>" + eol +
					"<atom:link href=\"[LINK]\" rel=\"self\" type=\"application/rss+xml\" />" + eol +
					"<description>[DESCRIPTION]</description>" + eol +
					"<language>[LANGUAGE]</language>" + eol +
					"<pubDate>[PUBDATE]</pubDate>" + eol +
					"<lastBuildDate>[LAST_BUILD_DATE]</lastBuildDate>" + eol +
					// <docs>http://blogs.law.harvard.edu/tech/rss</docs>
					"<generator>[GENERATOR]</generator>" + eol +
					"<managingEditor>[MANAGING_EDITOR]</managingEditor>" + eol +
					"<webMaster>[WEBMASTER]</webMaster>" + eol +
					"[ITEM_LIST]" + eol +
					"</channel>" + eol +
					"</rss>";

			string item_tpl = "<item>" + eol +
				"<title>[TITLE]</title>" + eol +
					"<pubDate>[PUBDATE]</pubDate>" + eol +
					"<link>[LINK]</link>" + eol +
					"<description>[DESCRIPTION]</description>" + eol +
					//<category>url</category>
					"<guid>[GUID]</guid>" + eol +
					"</item>" + eol;

			// временно!!!
			// channel_tpl = channel_tpl.Replace("[TITLE]", this.ModuleConfiguration.ModuleTitle);
			channel_tpl = channel_tpl.Replace ("[TITLE]", title);

			// временно!!!
			channel_tpl = channel_tpl.Replace ("[DESCRIPTION]", description);
			// из description страницы по TabId?

			channel_tpl = channel_tpl.Replace ("[LANGUAGE]", language);

			channel_tpl = channel_tpl.Replace ("[LINK]",
			                                   Server.HtmlEncode (
				//"http://" + PortalSettings.PortalAlias.HTTPAlias +
				EditUrl ("RSS"))
			                                   );


			// CultureInfo ci = new CultureInfo("en-US");
			// Console.WriteLine("{0,-30}{1}\n", msgCulture, ci.DisplayName);


			DateTime.Now.ToString ("R").Replace ("GMT", "+0300");

			channel_tpl = channel_tpl.Replace ("[PUBDATE]", FormatDateTime_RFC822 (DateTime.Now));
			channel_tpl = channel_tpl.Replace ("[LAST_BUILD_DATE]", FormatDateTime_RFC822 (DateTime.Now));

			// Get Admin
			DotNetNuke.Entities.Users.UserController uctrl = new DotNetNuke.Entities.Users.UserController ();
			DotNetNuke.Entities.Users.UserInfo admin = uctrl.GetUser (PortalId, PortalSettings.AdministratorId);

			string adminInfo = string.Format ("{0} ({1} {2})", 
			                                  admin.Email, admin.FirstName, admin.LastName);

			// почта должна дополняться реальным именем foo@bar.com (vasya pupkin)
			channel_tpl = channel_tpl.Replace ("[WEBMASTER]", adminInfo);
			channel_tpl = channel_tpl.Replace ("[MANAGING_EDITOR]", adminInfo);

			channel_tpl = channel_tpl.Replace ("[GENERATOR]", 
			                                   //String.Format("{0} v{1}", ModuleConfiguration.ModuleName, ModuleConfiguration.Version));
			                                   String.Format ("AnnoView v{0}", ModuleConfiguration.DesktopModule.Version));


			string itemListStr = string.Empty;
				// временно отлючено
			foreach (DocRollerInfo item in lai)
			{
				string itemStr = item_tpl;

				itemStr = itemStr.Replace ("[TITLE]", item.Content);
			
				var url = "";
				var descr = "";

				if (item.Tab != null) {
					url = "/default.aspx?TabId=" + item.Tab.TabID;
					var tc = new TabController ();
					descr = tc.GetTab (item.Tab.TabID, PortalId, true).Description;
				}
					if (item.DesktopModule != null)
					url ="/default.aspx?DesktopModuleId="+ item.DesktopModule.DesktopModuleID;
				if (item.TabModule != null)
					url =string.Format ("/default.aspx?tabid={0}&mid={1}", item.TabModule.TabID, item.TabModule.ModuleID);
				if (item.Journal != null)
					url ="/default.aspx?JournalId="+ item.Journal.JournalID;


				itemStr = itemStr.Replace ("[LINK]", Server.HtmlEncode (
					"http://" + PortalSettings.PortalAlias.HTTPAlias +
					url));

				itemStr = itemStr.Replace ("[DESCRIPTION]", descr);

				                           /*); 
				itemStr = itemStr.Replace ("[DESCRIPTION]", 
				                           ai.Description
				                           .Replace ("&amp;nbsp;", " ") // необязательно!
				                           .Replace ("&amp;#160;", " ") //
				                           .Replace ("href=&quot;/Portals/",
				          String.Format ("href=&quot;http://{0}/Portals/", 
				               PortalSettings.PortalAlias.HTTPAlias))   
				                           .Replace ("href=&quot;/LinkClick.aspx",
				          String.Format ("href=&quot;http://{0}/LinkClick.aspx", 
				               PortalSettings.PortalAlias.HTTPAlias))

				                           // TODO: Add Rege.Replace to &quot;htt..&quot; to UrlEncode
				                           // this will replace cyrillic letters in Url to %XX codes

				                           );*/

				itemStr = itemStr.Replace ("[PUBDATE]", FormatDateTime_RFC822 (item.CreatedOnDate));
				itemStr = itemStr.Replace ("[GUID]", // CHECK
				                           Server.HtmlEncode (
					//"http://" + PortalSettings.PortalAlias.HTTPAlias + 
					Globals.NavigateURL () + 
					String.Format ("&Mid={0}&ItemId={1}", this.ModuleId, item.ContentItemID))
				                           );

				itemListStr += itemStr;
			}

			//itemListStr = item_tpl; // for testing only!
			channel_tpl = channel_tpl.Replace ("[ITEM_LIST]", itemListStr);

			return channel_tpl;
		}

		public string FormatDateTime_RFC822(DateTime dt)
		{
			#region NOTES
			/*            
                TimeSpan timezone = TimeZone.CurrentTimeZone.GetUtcOffset(dt);
                string timezone_RFC822 = String.Format("+{0:N2}00", timezone.Hours);
                
                return dt.ToString("R").Replace("GMT", "+0300");
            */
			#endregion

			int offset =
				TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours;

			return dt.ToString("R").Replace("GMT",
			                                String.Format("{1}{0:D2}00", 
			              offset, ((offset >= 0)? "+" : "-")  
			              )
			                                );
		}

		public string FormatURL(string Link, bool TrackClicks)
		{
			return Globals.LinkClick(Link, TabId, ModuleId, TrackClicks);
		}

	}
}
