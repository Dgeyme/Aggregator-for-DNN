using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DotNetNuke.UI.UserControls;
using DotNetNuke.UI.WebControls;

namespace DocumRoller
{
	public partial class EditDocumRoller
	{	
		protected RequiredFieldValidator valContent;
		protected LinkButton cmdUpdate;
		protected LinkButton cmdCancel;
		protected LinkButton cmdDelete;
		protected LabelControl lblContent;
		protected TextEditor txtContent;
		protected ModuleAuditControl ctlAudit;
	}
}
