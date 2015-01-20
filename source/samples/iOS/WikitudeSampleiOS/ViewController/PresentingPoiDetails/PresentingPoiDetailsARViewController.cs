using System;
using System.Web;
using System.Collections.Specialized;

using Foundation;
using UIKit;

using Wikitude.Architect;

namespace WikitudeSample
{
	public class PresentingPoiDetailsARViewController : ARViewController
	{
		PresentingPoiDetailsArchitectViewDelegate		architectViewDelegate;

		public PresentingPoiDetailsARViewController (string worldOrUrl) : base(worldOrUrl, false)
		{
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear (animated);

			architectViewDelegate = new PresentingPoiDetailsArchitectViewDelegate (this);

			this.arView.Delegate = architectViewDelegate;
		}

		public void showPoiDetails(string id, string title, string description)
		{
			PoiDetailViewController vc = new PoiDetailViewController ();
			vc.idString = id;
			vc.titleString = title;
			vc.descriptionString = description;

			NavigationController.PushViewController (vc, true);
		}
	}

	public class PresentingPoiDetailsArchitectViewDelegate : WTArchitectViewDelegate
	{
		PresentingPoiDetailsARViewController _presentingVC;

		public PresentingPoiDetailsArchitectViewDelegate (PresentingPoiDetailsARViewController presentingVC)
		{
			_presentingVC = presentingVC;
		}

		public override void InvokedURL (WTArchitectView architectView, NSUrl url)
		{
			string uriString = url.AbsoluteString;
			Uri uri = new Uri (uriString);
			NameValueCollection parameters = HttpUtility.ParseQueryString (uri.Query);

			_presentingVC.showPoiDetails (parameters["id"], parameters["title"], parameters["description"]);
		}
	}
}
