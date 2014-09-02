using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Wikitude.Architect;
using Android.Util;
using Java.IO;
using Android.Graphics;

namespace Com.Wikitude.Samples
{
	[Activity (Label = "SamplePoidataFromNativeAndUrlListenerScreenshotActivity")]			
	public class SamplePoidataFromNativeAndUrlListenerScreenshotActivity : SamplePoidataFromNativeActivity,
			ArchitectView.IArchitectUrlListener,
			ArchitectView.ICaptureScreenCallback
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			architectView.RegisterUrlListener (this);
		}

		#region IArchitectUrlListener implementation
		public bool UrlWasInvoked (string uri)
		{
			try
			{
			var parsedUri = Android.Net.Uri.Parse (uri);

			if (parsedUri.Host.Equals ("button", StringComparison.InvariantCultureIgnoreCase)
					&& parsedUri.Query.Equals ("action=captureScreen", StringComparison.InvariantCultureIgnoreCase))
				architectView.CaptureScreen(ArchitectView.CaptureScreenCallback.CaptureModeCamAndWebview, this);
			}
			catch (Exception ex)
			{
				Log.Error (Constants.LOG_TAG, ex.ToString());
			}

			return true;
		}
		#endregion

		#region ICaptureScreenCallback implementation
		public void OnScreenCaptured (Bitmap bmp) {
			if (bmp != null) {
				Toast.MakeText (this, "Screenshot was captured ", ToastLength.Long).Show ();
			} else {
				Toast.MakeText (this, "Screenshot not captured", ToastLength.Long).Show ();
			}
		}
		#endregion
	}
}

