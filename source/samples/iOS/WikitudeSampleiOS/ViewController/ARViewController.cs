using System;
using UIKit;
using Foundation;
using MonoTouch.Dialog;
using Wikitude.Architect;

namespace WikitudeSample
{
	public class ARViewController : UIViewController
	{
		protected WTArchitectView arView;

		public string WorldOrUrl { get; private set; }
		public bool IsUrl { get; private set; }

		public ARViewController (string worldOrUrl, bool isUrl) : base()
		{
			WorldOrUrl = worldOrUrl;
			IsUrl = isUrl;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			if ( UIDevice.CurrentDevice.CheckSystemVersion(7, 0) )
			{
				EdgesForExtendedLayout = UIRectEdge.None;
			}

			if (WTArchitectView.IsDeviceSupportedForAugmentedRealityMode (WTAugmentedRealityMode._Geo))
			{
				arView = new WTArchitectView (UIScreen.MainScreen.Bounds, null, WTAugmentedRealityMode._Geo);			
				arView.SetLicenseKey ("Ug7LmdNQJKN5AX85OMRGt1LIk1xBmwOWi8u+4eEVBpIvKZ8EWh2w1z3f5Cd4YHnWlEIdazY1envd/W7Xy5U4GUlkNmH2l9ddWZr5gIsz0zuD4GZVunmt0o49f4rDv+ssM78CAklidZeMkxqTGGoG6I8UjoegiWEKtzoH3qWpruNTYWx0ZWRfX3mWnNyLFq5Z+rfkc+m7sBeEhO/Urh2wYX/E57J6MdWPrCvmW0Zrt0RfAkUjmmHZ9MdjOyghN4VtSnY6nwc+Xz2Wg8vrCG02TIMw8SbNBRqP4ljrg3BnmjSONHRC69rzLnzCalB+YXAIdh5QdZI8TG8nJNUQCZGmjdrF5SpznSbcLpjDqfI36NaGW3cnH6evloXrcItbrnJDeeZlfB7CZj6PpLaf6q4GpKJRIEiVbeY3UpQ19+5IsydEbo0eVwZQFtE/G/NB7mNM1SjwteJ53EumNT9hd/4fMmk7L3nUj4kpyZ2gttPTS0/1kxtwVJjfRntngMiSN6czJrmrI5IyqN3qjEDextUNJ6zpvj97Vx/k+6RkItCgbMLZzdGgnyvIq9jumKiICOZXcFz4iacFFsYag4w87FoUwJFdp2SAsuW374FdmMB2tE5Zk5CONbQvMCKkMwdT6RnqA0SrzX4NA9qDLv8DwcoOu3jiszRE//8uOGS26I4NIznQniu8gn04sdeDm3P50rVkB7Vq3CDP89LIE8CoqChJ9DVEm2IzwfHFRd6cDalxC9szRKxOI4H5Z6wJY4tPHTeQha3Gp4jFQXLbtwfPLPcr+DyH8GkOf6+aIbzz3AVZZz9v67JN2W5O1xR2BZXAjkE0SC4zXK2g0H6MuDEYLdLHZCnl8ik/Aw3ydyFe5zw9+olNC/72uH5rA5ZLyiACVauyXwwsc6bNzJu3c5Dyb724zg==");

				var absoluteWorldUrl = WorldOrUrl;
				if (!IsUrl)
					absoluteWorldUrl = NSBundle.MainBundle.BundleUrl.AbsoluteString + "ARchitectExamples/" + WorldOrUrl + "/index.html";
				Console.WriteLine (absoluteWorldUrl);
				var u = new NSUrl (absoluteWorldUrl);
				arView.LoadArchitectWorldFromUrl (u);

				View.AddSubview (arView);
			}
			else
			{
				var adErr = new UIAlertView ("Unsupported Device", "This device is not capable of running ARchitect Worlds. Requirements are: iOS 5 or higher, iPhone 3GS or higher, iPad 2 or higher. Note: iPod Touch 4th and 5th generation are only supported in WTARMode_IR.", null, "OK", null);
				adErr.Show ();
			}
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			if (arView != null) {
				arView.Start ();
			}
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			if (arView != null)
				arView.Stop ();
		}


	}
}
