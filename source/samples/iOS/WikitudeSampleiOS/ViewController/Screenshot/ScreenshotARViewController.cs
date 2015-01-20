using System;

using Foundation;
using UIKit;
using Social;

using Wikitude.Architect;

namespace WikitudeSample
{
	public class ScreenshotARViewController : ARViewController
	{
		ScreenshotArchitectViewDelegate architectViewDelegate;

		public ScreenshotARViewController (string worldOrUrl) : base(worldOrUrl, false)
		{
		}

		override public void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear (animated);

			architectViewDelegate = new ScreenshotArchitectViewDelegate (this);
			this.arView.Delegate = architectViewDelegate;
		}
	}

	public class ScreenshotArchitectViewDelegate : WTArchitectViewDelegate
	{
		ScreenshotARViewController	_presentingVC;

		public ScreenshotArchitectViewDelegate (ScreenshotARViewController presentingVC)
		{
			_presentingVC = presentingVC;
		}

		override public void InvokedURL (WTArchitectView architectView, NSUrl url)
		{
			if ( SLComposeViewController.IsAvailable(SLServiceKind.Facebook) ) {
				NSDictionary info = new NSDictionary ();

				architectView.CaptureScreenWithMode(WTScreenshotCaptureMode._CamAndWebView, WTScreenshotSaveMode._Delegate, WTScreenshotSaveOptions.None, info);
			} else {
				NSDictionary info = new NSDictionary ();
				architectView.CaptureScreenWithMode(WTScreenshotCaptureMode._CamAndWebView, WTScreenshotSaveMode._PhotoLibrary, WTScreenshotSaveOptions.CallDelegateOnSuccess, info);
			}

		}

		override public void DidCaptureScreenWithContext(WTArchitectView architectView, NSDictionary context)
		{

			string intString = context.ObjectForKey(new NSString("kWTScreenshotSaveModeKey")).ToString();
			int resultCode = int.Parse (intString);

			if (WTScreenshotSaveMode._Delegate == (Wikitude.Architect.WTScreenshotSaveMode)resultCode) {
				UIImage image = (UIImage)context[(new NSString("kWTScreenshotImageKey"))];
				postImageOnFacebook (image);
			} else {
				showPhotoLibraryAlert ();
			}
		}

		public void postImageOnFacebook(UIImage image)
		{
			if ( SLComposeViewController.IsAvailable(SLServiceKind.Facebook) ) 
			{
				SLComposeViewController composerVC = SLComposeViewController.FromService (SLServiceKind.Facebook);
				composerVC.SetInitialText ("Wikitude Snapshot");
				composerVC.AddImage (image);

				_presentingVC.PresentViewController (composerVC, true, null);

				composerVC.CompletionHandler += (result) => {
					InvokeOnMainThread (() => {

						NSString resultString = new NSString("");
						switch (result) {
						case SLComposeViewControllerResult.Cancelled:
							resultString = new NSString("Failed to post on Facebook");
							break;

						case SLComposeViewControllerResult.Done:
							resultString = new NSString("Successfully posted on Facebook");
							break;

						default:
							break;
						}

						UIAlertView alert = new UIAlertView ("Screen shot", resultString, null, "OK", null);
						alert.Show ();
					});
				};
			}
		}

		public void showPhotoLibraryAlert()
		{
			UIAlertView alert = new UIAlertView("Success", "Screen shot was saved in your photo library", null, "OK", null);
			alert.Show ();		
		}
	}
}

