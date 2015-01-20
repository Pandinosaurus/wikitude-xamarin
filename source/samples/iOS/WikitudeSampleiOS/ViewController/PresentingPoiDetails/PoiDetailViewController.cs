
using System;
using CoreGraphics;

using Foundation;
using UIKit;

namespace WikitudeSample
{
	public partial class PoiDetailViewController : UIViewController
	{

		public string idString;
		public string titleString;
		public string descriptionString;


		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public PoiDetailViewController ()
			: base (UserInterfaceIdiomIsPhone ? "PoiDetailViewController_iPhone" : "PoiDetailViewController_iPad", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			if ( UIDevice.CurrentDevice.CheckSystemVersion(7, 0) )
			{
				EdgesForExtendedLayout = UIRectEdge.None;
			}
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear (animated);

			idLabel.Text = idString;
			titleLabel.Text = titleString;
			descriptionLabel.Text = descriptionString;
		}
	}
}

