// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace WikitudeSample
{
	[Register ("PoiDetailViewController")]
	partial class PoiDetailViewController
	{
		[Outlet]
		UIKit.UILabel descriptionLabel { get; set; }

		[Outlet]
		UIKit.UILabel idLabel { get; set; }

		[Outlet]
		UIKit.UILabel titleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (idLabel != null) {
				idLabel.Dispose ();
				idLabel = null;
			}

			if (titleLabel != null) {
				titleLabel.Dispose ();
				titleLabel = null;
			}

			if (descriptionLabel != null) {
				descriptionLabel.Dispose ();
				descriptionLabel = null;
			}
		}
	}
}
